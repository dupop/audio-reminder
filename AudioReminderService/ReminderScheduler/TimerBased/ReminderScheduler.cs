using AudioReminderCore.Model;
using AudioReminderService.Persistence;
using Quartz;
using Quartz.Impl;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AudioReminderService.ReminderScheduler.TimerBased
{
    //TODO: optimize later so that one dismiss does not cause recreating of all timers
    //TODO: implement whole this class
    //TODO: draw detailed state diagram for all this...

    class ReminderScheduler
    {
        /// <summary>
        /// Is reminder events firing enabled. 
        /// This doesn't necessary mean that timer is running, e.g. there could be no active reminders.
        /// </summary>
        bool IsEnabled { get; set; }

        ReminderSchedulerState SchedulerState;
        protected Timer nextReminderTimer { get; set; }
        
        /// <summary>
        /// Chonologically ordered list of reminders that will trigger ringing.
        /// </summary>
        protected List<ReminderEntity> ActiveSortedReminders { get; set; }
        
        public int? SnoozeIntervalMinutes { get; protected set; }

        public event Action<string> ReminderTimeUp;

        /// <summary>
        /// Last time that program started annoying user.
        /// </summary>
        protected DateTime? LastReminderRinging { get; set; }

        /// <summary>
        /// Last time that user dismissed a reminder, i.e. he confimred that a reminder occurence is done.
        /// </summary>
        protected DateTime? LastReminderDismissing { get; set; }

        /// <summary>
        /// Last time that user snoozed a reminder
        /// </summary>
        protected DateTime? LastReminderSnoozing { get; set; }


        public ReminderScheduler()
        {
            nextReminderTimer = new Timer();
            nextReminderTimer.Enabled = false;
            nextReminderTimer.AutoReset = false;
            nextReminderTimer.Elapsed += NextReminderTimer_Elapsed;

            IsEnabled = false;
            SchedulerState = ReminderSchedulerState.NoActiveReminders;
        }

        private void NextReminderTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ReminderEntity elapsedReminder = ActiveSortedReminders?.FirstOrDefault();
            DateTime now = DateTime.UtcNow;

            if (elapsedReminder == null)
            {
                Log.Logger.Error("Elapsed Timer from scheduled can't find reminder for which ringing should be done.");
                //TODO: handle this flow
                return;
            }

            if (!new ReminderDismissableValidator().ValidateReminderShouldBeRinging(elapsedReminder, now))
            {
                Log.Logger.Error("Elapsed reminder is in invalid state (from the future or already dismissed).");
                //TODO: handle this flow
                return;
            }

            //start ringing
            OnReminderTimeUp(elapsedReminder.Name, now);

            SchedulerState = ReminderSchedulerState.WaitingUserResponse; //we could even already be in this state if this timer was triggered by snooze button

            //TODO: we should start snooze timer here, or even before that a timer for turning of the noise of this reminder after e.g. 1 min?
        }

        #region Interface for controling ReminderScheduler
        /// <summary>
        /// Initial list of reminders must be given to this object before it is started.
        /// </summary>
        public void Start()
        {
            Log.Logger.Information("Starting ReminderScheduler");

            DateTime now = DateTime.UtcNow; //good to be constant in a variable during this analysis in method so that it doesn't change during analysis. It could make some kind of timer deadlock where timer would never ring.

            if (IsEnabled)
            {
                Log.Logger.Warning("ReminderScheduler is already running.");
                return;
            }

            if (ActiveSortedReminders == null)
            {
                Log.Logger.Error("TimerReminderScheduler must be given initial list of reminders (at least an empty one) before starting it for the first time. Ignoring start request to ReminderScheduler");
                return;
            }

            IsEnabled = true;

            TryToScheduleNextReminder(now);

            Log.Logger.Information("Starting ReminderScheduler done");
        }

        public void Stop()
        {
            Log.Logger.Information("Stopping ReminderScheduler");

            if (!IsEnabled)
            {
                Log.Logger.Warning("ReminderScheduler is already stopped.");
                return;
            }

            IsEnabled = false;
            nextReminderTimer.Stop();

            Log.Logger.Information("Stopping ReminderScheduler done");
        }

        public void UpdateReminderList(IList<ReminderEntity> upToDateReminders)
        {
            Log.Logger.Information("Updating list of reminders in ReminderScheduler");
            
            DateTime now = DateTime.UtcNow; //good to be constant in a variable during this analysis in method so that it doesn't change during analysis. It could make some kind of timer deadlock where timer would never ring.

            //pause timer util we decide when should it ring again
            if (IsEnabled)
            {
                Log.Logger.Information("Pausing timer (if it is running at all)");
                nextReminderTimer.Stop();
            }

            //TODO: maybe not do this now but when needed, we can just find 1 first; keep list ordered instead of oredering it each time
            ActiveSortedReminders = upToDateReminders
                .Where(r => !r.IsDone())
                .Select(r => (ReminderEntity)r.Clone())
                .OrderBy(r => r.ScheduledTime)
                .ToList();
            
            //if scheduler is enabled continue timer (if there is need for this at all after change of reminders)
            if (IsEnabled)
            {
                TryToScheduleNextReminder(now);
            }

            Log.Logger.Information("Updating list of reminders in ReminderScheduler done");
        }

        private void TryToScheduleNextReminder(DateTime now)
        {
            if(SchedulerState == ReminderSchedulerState.WaitingUserResponse)
            {
                //TODO: handle this transiton when it not caused by snooze/dimiss but when this is action PARALEL to waiting for user response
            }

            bool activeRemindersExist = ActiveSortedReminders.Any();

            //continue timer (if there is need for this at all after change of reminders)
            if (activeRemindersExist)
            {
                int intervalMs = GetTimeInMsUntilNextRinging(now);
                nextReminderTimer.Interval = intervalMs;
                Log.Logger.Information($"Starting timer with intveral [Interval = {intervalMs} ms] ");
                nextReminderTimer.Start();
                SchedulerState = ReminderSchedulerState.WaitingNextReminder;
            }
            else
            {
                Log.Logger.Information($"Reminder timer will not be started because there are no active reminders.");
                SchedulerState = ReminderSchedulerState.NoActiveReminders;
            }
        }
        #endregion

        /// <summary>
        /// Expects that there is at least one active remidner
        /// </summary>
        /// <returns></returns>
        protected virtual int GetTimeInMsUntilNextRinging(DateTime now)
        {
            ReminderEntity nextReminder = ActiveSortedReminders.First();

            TimeSpan snoozeInterval = new TimeSpan(0, SnoozeIntervalMinutes ?? 0, 0); //TODO: handle null snooze  (no snooze)

            int minTimerLength = 1;//Timer can't handle 0 ms. 0 ms could be result of rounding from ticks to miliseconds
            
#warning //TODO: just a mock alogirhtm, a correct one is neede here! implement this from LastReminderRinging,LastReminderDismissing,
            //LastReminderSnoozing. We sould probably not ring again at all until we get at least snooze response, but on the other
            //hand some sanity check would be good because ringer maybe got stuck so we should try another ring so that next important events are not missed?
            bool isUserAlreadyAnnoyedTooMuchRecently = LastReminderRinging != null && now - LastReminderRinging < snoozeInterval;

            bool reminderAlreadyReadyForRinging = now > nextReminder.ScheduledTime;

            if (!reminderAlreadyReadyForRinging)
            {
                int timeMsUntilEvent = (int)(nextReminder.ScheduledTime - now).TotalMilliseconds;
                int intervalForTimer = Math.Max(timeMsUntilEvent, minTimerLength);

                return intervalForTimer;
            }

            //reminder should ring already, we should just check if we didn't ring too much regarding this event already
            if (isUserAlreadyAnnoyedTooMuchRecently)
            {
                //TODO: handle this scneario properly, adding a stub algorithm

                TimeSpan timeUntilNextSnoozeRinging = LastReminderRinging.Value /*not null?*/ + snoozeInterval - now;
                int timeMsUntilNextRinging = (int)timeUntilNextSnoozeRinging.TotalMilliseconds;
                int intervalForTimer = Math.Max(timeMsUntilNextRinging, minTimerLength);

                return intervalForTimer;
            }

            //reminder is ready to ring and program did not annoy him too much already - we can ring immediately
            //not triggering ring now, because I want this scenario to be handled on the sam thread as other scnearios would do, not to make additional risks
            return minTimerLength;
        }


        protected void OnReminderTimeUp(string reminderName, DateTime now)
        {
            Log.Logger.Information("ReminderScheduler triggering a ring");

            LastReminderRinging = now;
            ReminderTimeUp?.Invoke(reminderName);

            Log.Logger.Information("ReminderScheduler triggering a ring done");
        }

        //TODO: check if quartz or other dependency have time calculation library
        //TODO: add more unit tests, and plotting of methods as graph f(x) = y to find edge cases
        //TODO: After e.g. 1 year of not using service shoud we show that all recuring reminders are missed?
        //TODO: Consider option of using sched + new TimeStamp, see datetime aritchmetic rules
        //TODO: check again if same timestamp is everwhere passed and used in complete algorightm to prevent contradicting situations that some condition is true and few lines later the same condition is false
        //TODO: subsribing to and handlign system clock changes, especially when we go back in time

        public void DismissReminder(ReminderEntity reminderEntity)
        {
            DateTime now = DateTime.UtcNow;

            if(SchedulerState != ReminderSchedulerState.WaitingUserResponse)
            {
                Log.Logger.Error($"Ignoring attempt to dismiss reminder [name = {reminderEntity.Name}] because current scheduler state is {SchedulerState} instead od WaitingUserResponse");
                return;
            }

            if (!new ReminderDismissableValidator().ValidateReminderShouldBeRinging(reminderEntity, now))
            {
                return;
            }

            //dismissing nearest occurence of reminder in the past (not the originaly scheduled time)
            DateTime lastReminderRinging = new LastReminderOccurenceCalculator().GetLastReminderOccurence(reminderEntity, now).Value;
            reminderEntity.LastDismissedOccurence = lastReminderRinging;

            if (reminderEntity.IsRepeatable())
            {
                //setting next ringing of the reminder on its first next occurence by its schedule
                DateTime nextReminderRinging = new NextReminderOccurenceCalculator().GetNextReminderOccurence(reminderEntity, now).Value;
                reminderEntity.ScheduledTime = nextReminderRinging;
            }

            LastReminderDismissing = now;
            //WaitingReminderUserResponse = false; //TODO: state change to be done in UpdateReminderList if it was triggered from this method

            FilePersistenceAdapters.RemiderFilePersistence.OnEntitesChanged(); //indirectly also triggers UpdateReminderList on this object, and that will retrigger timer if there are still active timers
        }

        public void SnoozeReminder(ReminderEntity reminderEntity)
        {
            DateTime now = DateTime.UtcNow;

            if (SchedulerState != ReminderSchedulerState.WaitingUserResponse)
            {
                Log.Logger.Error($"Ignoring attempt to snooze reminder [name = {reminderEntity.Name}] because current scheduler state is {SchedulerState} instead od WaitingUserResponse");
                return;
            }

            if (!new ReminderDismissableValidator().ValidateReminderShouldBeRinging(reminderEntity, now))
            {
                return;
            }

            if(SnoozeIntervalMinutes == null)
            {
                Log.Logger.Error($"Ignoring attempt to snooze reminder [name = {reminderEntity.Name}] because snooze option is not set in the scheduler.");
                return;
            }

            //TODO: do we need any implementation regarding this at all, should we maybe keep status DialogShown, and not show again for some time if there is no (at least) snooze response?
            //AudioReminderService.ReminderScheduler.OnReminderSnoozed(reminderEntity);
            LastReminderSnoozing = now;
            //WaitingReminderUserResponse = false; //TODO: state change to be done in UpdateReminderList if it was triggered from this method

            int intervalMs = SnoozeIntervalMinutes.Value * 60 * 1000;
            nextReminderTimer.Interval = intervalMs;
            Log.Logger.Information($"Starting timer with intveral [Interval = {intervalMs} ms] for snooze purpose");
            nextReminderTimer.Start();

            FilePersistenceAdapters.RemiderFilePersistence.OnEntitesChanged(); //indirectly also  triggers UpdateReminderList on this object
        }


        public virtual void ConfigureSnooze(bool snoozeEnabled, int snoozeIntervalMinutes)
        {
            int? oldSnoozeInterval = SnoozeIntervalMinutes;

            if (snoozeEnabled)
            {
                if(snoozeIntervalMinutes > 0)
                {
                    SnoozeIntervalMinutes = snoozeIntervalMinutes;
                }
                else
                {
                    Log.Logger.Error($"ReminderScheduler ignoring snooze interval less than 1min [value = {snoozeIntervalMinutes}]. Treating this as no snooze.");
                    SnoozeIntervalMinutes = null;
                }
            }
            else
            {
                SnoozeIntervalMinutes = null;
            }

            bool snoozeIntervalChanged = oldSnoozeInterval != SnoozeIntervalMinutes;

            if (snoozeIntervalChanged && SchedulerState == ReminderSchedulerState.WaitingUserResponse)
            {
                //TODO: react on snooze interval change while snooze value is actively bein used
            }

        }
    }
}
