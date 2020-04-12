using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AudioReminderService.Scheduler.TimerBased.ReminderScheduling
{
    /// <summary>
    /// Fires an event each time the a reminder elapses accoring to its scheduled time.
    /// All missed (i.e. already elapsed and not dismissed) reminders are fired immediately when they are added to the list or this component is enabled.
    /// There could be multiple redundant event firings after each call to Update method because update method will put back in the list all elapsed reminders that are waiting for using response. UI Manager will need to handle duplicate request to add reminder to elapsed list.
    /// </summary>
    public class NextReminderNotifier
    {
        public event Action<ReminderEntity> ReminderElapsed;

        protected bool isEnabled;

        /// <summary>
        /// Controls if the ReminderElapsed event will be firing.
        /// Setting true will start timer if there are active events.
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                bool wasEnabledBefore = isEnabled;
                isEnabled = value;

                HandleStatusChange(wasEnabledBefore);
            }
        }

        protected Timer NextReminderTimer { get; set; }

        /// <summary>
        /// Chonologically ordered list of reminders that are still active (i.e. not dismissed).
        /// Never null.
        /// </summary>
        protected List<ReminderEntity> ActiveSortedReminders { get; set; }



        public NextReminderNotifier()
        {
            NextReminderTimer = new Timer();
            NextReminderTimer.Enabled = false;
            NextReminderTimer.AutoReset = false;
            NextReminderTimer.Elapsed += NextReminderTimer_Elapsed;

            ActiveSortedReminders = new List<ReminderEntity>();
            isEnabled = false;
        }

        private void HandleStatusChange(bool wasEnabledBefore)
        {
            bool startedNow = !wasEnabledBefore && isEnabled;
            bool stoppedNow = wasEnabledBefore && !isEnabled;

            if (startedNow)
            {
                DateTime now = DateTime.UtcNow;
                TryToScheduleNextReminder(now);
                Log.Logger.Information($"Started NextReminderNotifier");
            }
            else if (stoppedNow)
            {
                NextReminderTimer.Stop();
                Log.Logger.Information($"Stopped NextReminderNotifier");
            }
            else
            {
                Log.Logger.Information($"NextReminderNotifier already {(isEnabled ? "started" : "stopped")}");
            }
        }

        private void NextReminderTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!isEnabled)
            {
                //not expected to happen, but may be the result of some unexpected edge case
                Log.Logger.Error("Timer fired event while NextReminderNotifier was not enabled. Ignoring event.");
                return;
            }

            ReminderEntity elapsedReminder = ActiveSortedReminders?.FirstOrDefault();
            DateTime now = DateTime.UtcNow;

            if (!ValidateReminderElapsed(elapsedReminder, now))
            {
                //not expected to happen, but may be the result of some unexpected edge case
                Log.Logger.Error("Elapsed reminder is in invalid state (it is null, or scheduled in the future, or already dismissed). Attemting Recovery mechanism.");
                Recovery(now);
                return;
            }

            //Add reminder to list of elapsed reminders
            OnReminderElapsed(elapsedReminder, now);

            ActiveSortedReminders.Remove(elapsedReminder);
            TryToScheduleNextReminder(now);

            //Dismissing operation will put repeatable reminder in list again (with new ScheduledTime) by triggering UpdateReminderList via persistence adapter.

            //TODO: should snooze somehow put it in this list or UI manager should handle that itself? Reminder schedule change for snooze purposes would its information regarding real scheduled time. Also adding reminder for snoozing to this list would trigger adding all other reminders to the list.
        }

        protected virtual bool ValidateReminderElapsed(ReminderEntity reminder, DateTime now)
        {
            return new ElapsedReminderValidator().ValidateReminderElapsed(reminder, now);
        }

        /// <summary>
        /// For robustness purposes, handles unexpected scenario if timer elapsed but there are no reminders in the list or the first reminder is not actually elapsed. Those situation may only occure as a result of not well handled conncurency.
        /// </summary>
        protected virtual void Recovery(DateTime now)
        {
            if (!ActiveSortedReminders.Any())
            {
                Log.Logger.Warning("RECOVERY: As there are no reminders in the list. The timer will just not be retriggered.");
                return;
            }

            //Tries to fix reminder ordering. As a collateral benefit it also removes dissmissed non-repeatable reminders which are not epxected to be preset in the list at all.
            ActiveSortedReminders = CloneAndSortOnlyActiveReminders(ActiveSortedReminders);

            //At least one reminder exists, but chronologically first reminder is not actually elapsed when timer is elapsed. Not a problem just reschedule the timer (although this situation should not happen).
            Log.Logger.Warning("RECOVERY: Recovery done on reminder list. Trying to schedule next reminder.");
            TryToScheduleNextReminder(now);
        }


        protected virtual void OnReminderElapsed(ReminderEntity reminder, DateTime now)
        {
            Log.Logger.Information("Reminder elapsed. It can be added to elapsed reminders list.");
            ReminderElapsed?.Invoke(reminder);
        }

        /// <summary>
        /// Updates the list of active reminders, and starts the timer if needed.
        /// </summary>
        /// <param name="upToDateReminders"></param>
        public void UpdateReminderList(IList<ReminderEntity> upToDateReminders)
        {
            Log.Logger.Information("Updating list of reminders in ReminderScheduler");

            DateTime now = DateTime.UtcNow; //good to be constant in a variable during this analysis in method so that it doesn't change during analysis. It could make some kind of timer deadlock where timer would never ring.

            //pause timer util we decide when should it ring again so that it does not make conccurency issues while we are changing the list
            if (isEnabled)
            {
                Log.Logger.Information("Pausing timer (if it is running at all)"); //maybe there were no events
                NextReminderTimer.Stop();
            }

            //TODO: maybe not do this now but when needed, we can just find 1 first; keep list ordered instead of oredering it each time
            ActiveSortedReminders = CloneAndSortOnlyActiveReminders(upToDateReminders);

            //if scheduler is enabled continue timer (if there is need for this at all after change of reminders)
            if (isEnabled)
            {
                TryToScheduleNextReminder(now);
            }

            Log.Logger.Information("Updating list of reminders in ReminderScheduler done");
        }

        protected virtual List<ReminderEntity> CloneAndSortOnlyActiveReminders(IList<ReminderEntity> allReminders)
        {
            List<ReminderEntity> onlyActiveReminders = ActiveSortedReminders = allReminders
                            .Where(r => !r.Dismissed)
                            .Select(r => (ReminderEntity)r.Clone()) //TODO: check later how will this cloning be compatible with reminder ID, will we duplicate it also?
                            .OrderBy(r => r.ScheduledTime)
                            .ToList();

            return onlyActiveReminders;
        }

        /// <summary>
        /// Scheduled next reminder if thare are reminders in the list
        /// </summary>
        protected virtual void TryToScheduleNextReminder(DateTime now)
        {
            bool activeRemindersExist = ActiveSortedReminders.Any();

            //continue timer (if there is need for this at all after change of reminders)
            if (activeRemindersExist)
            {
                ScheduleNextReminder(now);
            }
            else
            {
                Log.Logger.Information($"NextReminderNotifier timer will not be started because there are no active reminders.");
            }
        }

        /// <summary>
        /// Starts timer for the next reminder
        /// </summary>
        /// <param name="now"></param>
        protected virtual void ScheduleNextReminder(DateTime now)
        {
            int intervalMs = GetTimeInMsUntilNextRinging(now);
            NextReminderTimer.Interval = intervalMs;
            Log.Logger.Information($"Starting NextReminderNotifier timer with intveral [Interval = {intervalMs} ms] ");
            NextReminderTimer.Start();
        }

        protected virtual int GetTimeInMsUntilNextRinging(DateTime now)
        {
            ReminderEntity nextReminder = ActiveSortedReminders.First();

            return GetTimeInMsUntilNextRinging(nextReminder, now);
        }

        /// <summary>
        /// Determines interval until an active (i.e. not-dismissed) reimnder will elapse. Minimal value 1ms.
        /// </summary>
        protected virtual int GetTimeInMsUntilNextRinging(ReminderEntity reminder, DateTime now)
        {
            int minTimerLength = 1;//Timer can't handle 0 ms. 0 ms could be result of rounding from ticks to miliseconds

            bool reminderAlreadyElapsed = now > reminder.ScheduledTime;

            if (!reminderAlreadyElapsed)
            {
                int timeMsUntilEvent = (int)(reminder.ScheduledTime - now).TotalMilliseconds;
                int intervalForTimer = Math.Max(timeMsUntilEvent, minTimerLength);

                return intervalForTimer;
            }

            //reminder already elapsed
            return minTimerLength;
        }
    }
}
