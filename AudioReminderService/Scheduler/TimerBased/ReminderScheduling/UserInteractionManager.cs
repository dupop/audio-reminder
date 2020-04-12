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
    //TODO: log all state changes

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

            switch (userState)
            {
                case ReminderSchedulerState.NoElapsedReminders:

                    //start ringing
                    GoToRingingState(reminder.Name, now);
                    break;
                case ReminderSchedulerState.WaitingUserResponse:
                    //ignoring next reminder until previous remidner is handled
                    //what to do when another event comes but previous is not yet handled by the user - just show them one by one, when handling previous is over, or update this one to show all?
                    break;
                case ReminderSchedulerState.SnoozeTime:
                    //ignoring next reminder until previous remidner is handled
                    //TODO: force showing of both reminders in the same form window? (forcing showing them one by one would be confusing as we don't respect snooze interval, and user is not aware that behing this reminder is another (potentially with higher priority))
                    break;
            }
        }


        protected void GoToRingingState(string reminderName, DateTime now)
        {
            Log.Logger.Information("ReminderScheduler triggering a ringing");

            //TODO:  should we start snooze timer here, or even before that a timer for turning off the noise of this reminder after e.g. 1 min of no response from user?
            userState = ReminderSchedulerState.WaitingUserResponse;
            LastReminderRinging = now;

            OnRingingNeeded(reminderName);

            Log.Logger.Information("ReminderScheduler triggering a ringing done");
        }

        protected virtual void OnRingingNeeded(string reminderName)
        {
            RingingNeeded?.Invoke(reminderName);
        }

        //todo: all these return statements are risky, they prevent the only chance for the user the make next step and don't give him another chance. e.g. if snooze feautre is disabled while ringing form is open, and than user clicks snooze
        //todo: disable, even better hide snooze button if that feature is disabled, we will probably need to handle closing the form as dismiss, not snooze
        //todo: validation if reminders as paramters to dismiss and snooze, are indeed excepted (or we first expect some other), or just remove them (less safe)
        public void DismissReminder(ReminderEntity reminderEntity)
        {
            DateTime now = DateTime.UtcNow;

            if (userState != ReminderSchedulerState.WaitingUserResponse)
            {
                Log.Logger.Error($"Ignoring attempt to dismiss reminder [name = {reminderEntity.Name}] because current scheduler state is {userState} instead od WaitingUserResponse");
                return;
            }

            // Protection of some kind of double dismis request (maybe by multiple ringing windows or something else) that would cause next occurance of reminder to be skipped
            if (!ValidateReminderShouldBeRinging(reminderEntity, now))
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

            ElapsedActiveReminders.Remove(reminderEntity);

            //TODO: test this - we are here potenitally calling ringer again before response for Dismissing is returned.
            GoToRingingOrIdleState(now);

            //indirectly also triggers UpdateReminderList on this object //TODO: what are effects of that, probably none?
            //TODO: immediate notifying of this change to NextReminderNOtifier is not needed. Should it be prevented or it produces no harm, as it only gives us duplicate reminders here?
            FilePersistenceAdapters.RemiderFilePersistence.OnEntitesChanged();
        }

        protected virtual bool ValidateReminderShouldBeRinging(ReminderEntity reminderEntity, DateTime now)
        {
            return new ElapsedReminderValidator().ValidateReminderShouldBeRinging(reminderEntity, now);
        }

        /// <summary>
        /// Immediately fires next event if there is any reminder in the list, or goes to NoElapsedReminders state otherwise.
        /// </summary>
        protected virtual void GoToRingingOrIdleState(DateTime now)
        {
            if (ElapsedActiveReminders.Any())
            {
                var nextToRing = ElapsedActiveReminders.First();

                GoToRingingState(nextToRing.Name, now);
            }
            else
            {
                userState = ReminderSchedulerState.NoElapsedReminders;
            }
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

            GoToSnoozeState(now);
        }

        protected virtual void GoToSnoozeState(DateTime now, int? customSnoozeIntervalMs = null)
        {
            userState = ReminderSchedulerState.SnoozeTime;
            LastReminderSnoozing = now;

            //determine interval
            int configuredIntervalMs = SnoozeIntervalMinutes.Value * 60 * 1000;
            int snoozeIntervalToUse = customSnoozeIntervalMs ?? configuredIntervalMs;

            //start timer
            SnoozeTimer.Interval = snoozeIntervalToUse;
            Log.Logger.Information($"Starting snooze timer with intveral [Interval = {snoozeIntervalToUse} ms]");
            SnoozeTimer.Start();
        }

        protected virtual void SnoozeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime now = DateTime.UtcNow;
            HandleSnoozeElapsed(now);
        }

        protected virtual void HandleSnoozeElapsed(DateTime now)
        {
            if (userState != ReminderSchedulerState.SnoozeTime)
            {
                //only expected to happen as a result of not well handled concurrency
                //this may put component in a deadlock probably! Will disable+enabled unblock the state?
                Log.Logger.Error($"UserState is {userState} instead of {ReminderSchedulerState.SnoozeTime} after snooze period elapsed. Ignoring timer event.");
                return;
            }

            var nextReminderToRing = ElapsedActiveReminders.FirstOrDefault();
            if (nextReminderToRing == null)
            {
                //only expected to happen as a result of not well handled concurrency
                Log.Logger.Error($"No reminder found after snooze period elapsed. Going to {ReminderSchedulerState.NoElapsedReminders} state.");
                userState = ReminderSchedulerState.NoElapsedReminders;
                return;
            }

            GoToRingingState(nextReminderToRing.Name, now);
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

            //TODO: just a mock alogirhtm, a correct one is neede here! implement this from LastReminderRinging,LastReminderDismissing,
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
            SnoozeTimer.Stop(); //preventing concurrency issues, we will restart it again if state was indeed SnoozeTime

            DateTime now = DateTime.UtcNow;

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

            if (snoozeIntervalChanged && userState == ReminderSchedulerState.SnoozeTime)
            {
                if (SnoozeIntervalMinutes == null)
                {
                    GoToRingingOrIdleState(now);
                }
                else
                {
                    //uses new snoose interval while recalculating the remaining time
                    int remainingSnoozeTimeMs = CalculateRemainingSnoozeTime(now);

                    GoToSnoozeState(now, remainingSnoozeTimeMs);
                }
            }
        }

        protected virtual int CalculateRemainingSnoozeTime(DateTime now)
        {
            TimeSpan alreadyElapsedTime = now - LastReminderSnoozing.Value; //nullcehck for additional robustness?
            int alreadyElapsedTimeMs = (int)alreadyElapsedTime.TotalMilliseconds;

            int snoozePeriodMs = SnoozeIntervalMinutes.Value * 60 * 1000;

            const int minimalTimerIntervalMs = 1; //timer can't handle 0ms or negative value

            int remainingSnoozeTimeMs = Math.Max(snoozePeriodMs - alreadyElapsedTimeMs, minimalTimerIntervalMs);
            return remainingSnoozeTimeMs;
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
