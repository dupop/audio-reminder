using AudioReminderCore.Model;
using AudioReminderService.Persistence;
using AudioReminderService.Scheduler.TimerBased.DateTimeArithmetic;
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
    /// Keeps track of list of already elapsed timers and what reminders are currently shown to user, how and if he responded and which of the elapsed reminders should be additionaly shown
    /// </summary>
    class UserInteractionManager
    {
        public int? SnoozeIntervalMinutes { get; protected set; }

        public event Action<string> RingingNeeded;

        /// <summary>
        /// Is making more ringing enabled 
        /// </summary>
        public bool IsEnabled { get; set; } //TODO: more precise defintion + use this value

        /// <summary>
        /// Reminders that are elapsed but are not yet dismissed.
        /// Never null.
        /// </summary>
        protected List<ReminderEntity> ElapsedActiveReminders { get; set; }

        ReminderSchedulerState userState;

        protected Timer SnoozeTimer { get; set; }


        //TODO: consider if these are still needed
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



        public UserInteractionManager()
        {
            SnoozeTimer = new Timer();
            SnoozeTimer.Enabled = false;
            SnoozeTimer.AutoReset = false;
            SnoozeTimer.Elapsed += SnoozeTimer_Elapsed; ;

            ElapsedActiveReminders = new List<ReminderEntity>();
            IsEnabled = false;
            userState = ReminderSchedulerState.NoElapsedReminders;

        }

        /// <summary>
        /// Call this method when a reminder elapses. Robust to adding again elapsed reminders that are not yet dismissed.
        /// </summary>
        /// <param name="reminder"></param>
        public virtual void OnReminderElapsed(ReminderEntity reminder)
        {
            DateTime now = DateTime.UtcNow;

            if (reminder == null)
            {
                //just a redundant check so that this component has robust interface
                Log.Logger.Error($"OnReminderElapsed event fired with null reminder. Ignoring OnReminderElapsed event");
                return;
            }

            if (ElapsedActiveReminders.Any(rem => rem.Name == reminder.Name))
            {
                Log.Logger.Information($"ElapsedActiveReminders list already contains reminder [name = {reminder.Name}]. Ignoring OnReminderElapsed event.");
                return;
            }

            ElapsedActiveReminders.Add(reminder); //TODO: is some sorting needed or it is safe to always add it at the end?

            if (!IsEnabled)
            {
                return;
            }

            if (userState == ReminderSchedulerState.NoElapsedReminders)
            {
                //start ringing
                OnRingingNeeded(reminder.Name, now);
                //TODO: we should start snooze timer here, or even before that a timer for turning off the noise of this reminder after e.g. 1 min?

                userState = ReminderSchedulerState.WaitingUserResponse;
            }
            else //WaitingUserResponse
            {
                //TODO: what to do when another event comes but previous is not yet handled by the user?
            }
        }


        protected void OnRingingNeeded(string reminderName, DateTime now)
        {
            Log.Logger.Information("ReminderScheduler triggering a ring");

            LastReminderRinging = now;
            RingingNeeded?.Invoke(reminderName);

            Log.Logger.Information("ReminderScheduler triggering a ring done");
        }

        public void DismissReminder(ReminderEntity reminderEntity)
        {
            DateTime now = DateTime.UtcNow;

            if (userState != ReminderSchedulerState.WaitingUserResponse)
            {
                Log.Logger.Error($"Ignoring attempt to dismiss reminder [name = {reminderEntity.Name}] because current scheduler state is {userState} instead od WaitingUserResponse");
                return;
            }

            // Protection of some kind of double dismis request (maybe by multiple ringing windows or something else) that would cause next occurance of reminder to be skipped
            if (!new ElapsedReminderValidator().ValidateReminderShouldBeRinging(reminderEntity, now))
            {
                return;
            }

            if (reminderEntity.IsRepeatable())
            {
                //setting next ringing of the reminder on its first next occurence by its schedule
                DateTime nextReminderRinging = new NextReminderOccurenceCalculator().GetNextReminderOccurence(reminderEntity, now).Value;
                reminderEntity.ScheduledTime = nextReminderRinging;
            }
            else
            {
                reminderEntity.Dismissed = true;
            }

            LastReminderDismissing = now;

            //TODO: check if there are more reminder in the list to be started immediately?

            //WaitingReminderUserResponse = false;//userState != ReminderSchedulerState.NoElapsedReminders //TODO: state change to be done in UpdateReminderList if it was triggered from this method

            FilePersistenceAdapters.RemiderFilePersistence.OnEntitesChanged(); //indirectly also triggers UpdateReminderList on this object, and that will retrigger timer if there are still active timers
        }

        public void SnoozeReminder(ReminderEntity reminderEntity)
        {
            DateTime now = DateTime.UtcNow;

            if (userState != ReminderSchedulerState.WaitingUserResponse)
            {
                Log.Logger.Error($"Ignoring attempt to snooze reminder [name = {reminderEntity.Name}] because current scheduler state is {userState} instead od WaitingUserResponse");
                return;
            }

            // Protection of some kind of double dismis request (maybe by multiple ringing windows etc..) that would cause next occurance of reminder to be skipped
            if (!new ElapsedReminderValidator().ValidateReminderShouldBeRinging(reminderEntity, now))
            {
                return;
            }

            if (SnoozeIntervalMinutes == null)
            {
                Log.Logger.Error($"Ignoring attempt to snooze reminder [name = {reminderEntity.Name}] because snooze option is not set in the scheduler.");
                return;
            }

            //TODO: do we need any implementation regarding this at all, should we maybe keep status DialogShown, and not show again for some time if there is no (at least) snooze response?
            //AudioReminderService.ReminderScheduler.OnReminderSnoozed(reminderEntity);
            LastReminderSnoozing = now;
            //WaitingReminderUserResponse = false; //TODO: state change to be done in UpdateReminderList if it was triggered from this method

            int intervalMs = SnoozeIntervalMinutes.Value * 60 * 1000;
            SnoozeTimer.Interval = intervalMs;
            Log.Logger.Information($"Starting timer with intveral [Interval = {intervalMs} ms] for snooze purpose");
            SnoozeTimer.Start();

            FilePersistenceAdapters.RemiderFilePersistence.OnEntitesChanged(); //indirectly also  triggers UpdateReminderList on this object
        }

        protected virtual void SnoozeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //TODO: handle
            throw new NotImplementedException();
        }

        /// <summary>
        /// Expects that there is at least one active remidner
        /// </summary>
        /// <returns></returns>
        protected virtual int GetTimeInMsUntilNextRinging(DateTime now)
        {
            ReminderEntity nextReminder = ElapsedActiveReminders.First();

            TimeSpan snoozeInterval = new TimeSpan(0, SnoozeIntervalMinutes ?? 0, 0); //TODO: handle null snooze  (no snooze)

            int minTimerLength = 1;//Timer can't handle 0 ms. 0 ms could be result of rounding from ticks to miliseconds

#warning //TODO: just a mock alogirhtm, a correct one is neede here! implement this from LastReminderRinging,LastReminderDismissing,
            //LastReminderSnoozing. We sould probably not ring again at all until we get at least snooze response, but on the other
            //hand some sanity check would be good because ringer maybe got stuck so we should try another ring so that next important events are not missed?
            bool isUserAlreadyAnnoyedTooMuchRecently = LastReminderRinging != null && now - LastReminderRinging < snoozeInterval;


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

        public virtual void ConfigureSnooze(bool snoozeEnabled, int snoozeIntervalMinutes)
        {
            int? oldSnoozeInterval = SnoozeIntervalMinutes;

            if (snoozeEnabled)
            {
                if (snoozeIntervalMinutes > 0)
                {
                    SnoozeIntervalMinutes = snoozeIntervalMinutes;
                }
                else
                {
                    Log.Logger.Error($"ReminderScheduler ignoring snooze interval less than 1 minute [value = {snoozeIntervalMinutes} min]. Treating this as no snooze.");
                    SnoozeIntervalMinutes = null;
                }
            }
            else
            {
                SnoozeIntervalMinutes = null;
            }

            bool snoozeIntervalChanged = oldSnoozeInterval != SnoozeIntervalMinutes;

            if (snoozeIntervalChanged && userState == ReminderSchedulerState.WaitingUserResponse)
            {
                //TODO: react on snooze interval change while snooze value is actively bein used
            }
        }

        //TODO: providte functionallity to run immediately all snooze reminders, not to bother user later with them

        /// <summary>
        /// We are not introducing new reminders through this method, we are just filtering out timers that are possibly not for ringing any more (and that are deleted) or modifying the existing ones
        /// </summary>
        public void UpdateReminderList(IList<ReminderEntity> upToDateReminders)
        {
            Log.Logger.Information("Updating list of reminders in UserInteractionManger");

            DateTime now = DateTime.UtcNow; //good to be constant in a variable during this analysis in method so that it doesn't change during analysis. It could make some kind of timer deadlock where timer would never ring.

            //pause timer util we decide when should it ring again
            if (IsEnabled)
            {
                Log.Logger.Information("Pausing timer (if it is running at all)");
                SnoozeTimer.Stop();
            }

            List<ReminderEntity> remindersToBeRemoved = new List<ReminderEntity>();
            //finding elapsed reminders that don't exist anymore
            foreach (ReminderEntity elapsedReminder in ElapsedActiveReminders)
            {
                ReminderEntity upToDateVersionOfElapsedReminder = upToDateReminders.FirstOrDefault(rem => rem.Name == elapsedReminder.Name);
                bool elapsedReminderStillExists = upToDateVersionOfElapsedReminder == null;

                if (elapsedReminderStillExists)
                {
                    //TODO: elapsed reminder still exists but it could be changed. Maybe it is not for ringing anymore or some other its parameter was modified? Should we event prevent this kind of changes during snooze period?
                }
                else
                {
                    remindersToBeRemoved.Add(elapsedReminder);
                    //TODO: handle elapsed reminder deleted (or renamed, but that is the same as creating a new) //except that a rename of an elapsed timer could maybe bypass validation of reminder already elapsed? 
                }
            }

            foreach (ReminderEntity reminderToBeRemoved in remindersToBeRemoved)
            {
                ElapsedActiveReminders.Remove(reminderToBeRemoved);
            }


            //foreach (ReminderEntity upToDateReminder in upToDateReminders)
            //{
            //    ReminderEntity elapsedVersionOfUpToDateReminder = ElapsedActiveReminders.FirstOrDefault(rem => rem.Name == upToDateReminder.Name);

            //    if (elapsedVersionOfUpToDateReminder == null)
            //    {
            //        //new reminder is detected, but we can ignore this because it will first be processed by NextReminderNotifier, and if it is elapsed it will fire an event for that reminder
            //    }
            //}


            //if scheduler is enabled continue timer (if there is need for this at all after change of reminders)
            if (IsEnabled)
            {
                //TryToScheduleNextReminder(now);
            }

            Log.Logger.Information("Updating list of reminders in ReminderScheduler done");
        }

    }
}
