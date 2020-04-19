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
    /// Keeps track of list of already elapsed timers and what reminders are currently shown to user,
    /// how and if he responded and which of the elapsed reminders should be additionaly shown
    /// </summary>
    public class UserInteractionManager
    {
        /// <summary>
        /// Configured snooze interval. Null when snooze is disabled.
        /// </summary>
        public int? SnoozeIntervalMinutes { get; protected set; }

        /// <summary>
        /// Fires an event when current snooze interval elapses.
        /// </summary>
        protected Timer SnoozeTimer { get; set; }

        /// <summary>
        /// Last time that user snoozed a reminder.
        /// </summary>
        protected DateTime? SnoozeTimerStarted { get; set; }

        /// <summary>
        /// Fired when a reminder should start ringing
        /// </summary>
        public event Action<string> RingingNeeded;

        /// <summary>
        /// Is requesting additional interaction from user enabled.
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return UserState != UserInteractionState.Disabled;
            }
            set
            {
                bool wasEnabledBefore = UserState != UserInteractionState.Disabled;
                bool isEnabled = value;

                HandleStatusChangeAndSetIsEnabled(wasEnabledBefore, isEnabled);
            }
        }

        /// <summary>
        /// Reminders that are elapsed but are not yet dismissed.
        /// Never null.
        /// </summary>
        protected List<ReminderEntity> ElapsedActiveReminders { get; set; }

        /// <summary>
        /// Keeps track of interaction between the user and the scheduler and who is repoinsible to make next action.
        /// </summary>
        protected UserInteractionState UserState { get; set; }
        
        //TODO: consider if these are still needed
        /// <summary>
        /// Last time that program started annoying user.
        /// </summary>
        protected DateTime? LastReminderRinging { get; set; }

        /// <summary>
        /// Last time that user dismissed a reminder, i.e. he confimred that a reminder occurence is done.
        /// </summary>
        protected DateTime? LastReminderDismissing { get; set; }



        public UserInteractionManager()
        {
            Log.Logger.Information($"Creating UserInteractionManager");

            SnoozeTimer = new Timer();
            SnoozeTimer.Enabled = false;
            SnoozeTimer.AutoReset = false;
            SnoozeTimer.Elapsed += SnoozeTimer_Elapsed;

            ElapsedActiveReminders = new List<ReminderEntity>();
            UserState = UserInteractionState.Disabled;

            Log.Logger.Information($"Creating UserInteractionManager done");

        }

        protected virtual void HandleStatusChangeAndSetIsEnabled(bool wasEnabledBefore, bool isEnabled)
        {
            bool startedNow = !wasEnabledBefore && isEnabled;
            bool stoppedNow = wasEnabledBefore && !isEnabled;
            DateTime now = DateTime.UtcNow;

            if (startedNow)
            {
                Log.Logger.Information($"Starting UserInteractionManager");
                GoToRingingOrIdleState(now);
                Log.Logger.Information($"Starting UserInteractionManager done");
            }
            else if (stoppedNow)
            {
                SnoozeTimer.Stop();
                UserState = UserInteractionState.Disabled;
                Log.Logger.Information($"Stopped UserInteractionManager");
            }
            else
            {
                Log.Logger.Information($"UserInteractionManager already {(isEnabled ? "started" : "stopped")}");
            }
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

            //TODO: duplicate reminder could be added as a consequence of change to the reminder entity. This would need to be handled, especially when the first reminder is changed.
            if (ElapsedActiveReminders.Any(rem => rem.Name == reminder.Name))
            {
                Log.Logger.Information($"ElapsedActiveReminders list already contains reminder [name = {reminder.Name}]. Ignoring OnReminderElapsed event.");
                return;
            }

            ElapsedActiveReminders.Add(reminder); //TODO: is some sorting needed or it is safe to always add it at the end? Otherwise, we would maybe need to handle change of first reminder.

            Log.Logger.Information($"Processing OnReminderElapsed event [name = {reminder.Name}, current component state = {UserState}].");
            switch (UserState)
            {
                case UserInteractionState.NoElapsedReminders:
                    //start ringing
                    GoToRingingState(reminder.Name, now);
                    break;
                case UserInteractionState.WaitingUserResponse:
                    //ignoring next reminder until previous remidner is handled
                    //what to do when another event comes but previous is not yet handled by the user - just show them one by one, when handling previous is over, or update this one to show all?
                    break;
                case UserInteractionState.SnoozeTime:
                    //ignoring next reminder until previous remidner is handled
                    //TODO: force showing of both reminders in the same form window? (forcing showing them one by one would be confusing as we don't respect snooze interval, and user is not aware that behing this reminder is another (potentially with higher priority))
                    break;
                case UserInteractionState.Disabled:
                    Log.Logger.Information($"OnReminderElapsed event [name = {reminder.Name}] will be handled just by adding reminder to the list because UserInteractionManager is currently not enabled.");
                    break;
            }
        }

        /// <summary>
        /// Set user state and fire ringing event.
        /// </summary>
        /// <param name="reminderName"></param>
        /// <param name="now"></param>
        protected void GoToRingingState(string reminderName, DateTime now)
        {
            //TODO:  should we start snooze timer here, or even before that a timer for turning off the noise of this reminder after e.g. 1 min of no response from user?
            UserState = UserInteractionState.WaitingUserResponse;
            LastReminderRinging = now;

            OnRingingNeeded(reminderName);
        }

        protected virtual void OnRingingNeeded(string reminderName)
        {
            Log.Logger.Information("UserInteractionManager triggering a ringing");
            RingingNeeded?.Invoke(reminderName);
            Log.Logger.Information("UserInteractionManager triggering a ringing done"); //TODO: this seem to behave async? check why?
        }

        //todo: all these return statements are risky, they prevent the only chance for the user the make next step and don't give him another chance. e.g. if snooze feautre is disabled while ringing form is open, and than user clicks snooze
        //todo: when component is disabled we skip all processing, not just going to next state, review this once more to bse sure that this is ok. Same for snooze, snooze elapsed, 
        //todo: disable, even better hide snooze button if that feature is disabled, we will probably need to handle closing the form as dismiss, not snooze
        //todo: validation if reminders as paramters to dismiss and snooze, are indeed excepted (or we first expect some other), or just remove them (less safe)
        public void DismissReminder(ReminderEntity reminderEntity)
        {
            DateTime now = DateTime.UtcNow;

            //TODO: this is proably not expexted to happen during disabled state, but I assume it should make no harm to react on this bacause user can still have old ringing dialogs

            if (UserState != UserInteractionState.WaitingUserResponse)
            {
                Log.Logger.Error($"Ignoring attempt to dismiss reminder [name = {reminderEntity.Name}] because current scheduler state is {UserState} instead od WaitingUserResponse");
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
                UserState = UserInteractionState.NoElapsedReminders;
                Log.Logger.Information($"No more elapsed reminders in the list. GoToRingingOrIdleState method is setting state to NoElapsedReminders");
            }
        }

        public void SnoozeReminder(ReminderEntity reminderEntity)
        {
            DateTime now = DateTime.UtcNow;

            if (UserState != UserInteractionState.WaitingUserResponse)
            {
                Log.Logger.Error($"Ignoring attempt to snooze reminder [name = {reminderEntity.Name}] because current scheduler state is {UserState} instead od WaitingUserResponse");
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

        /// <summary>
        /// Sets SnoozeTime user state and starts the snooze timer countdown.
        /// </summary>
        protected virtual void GoToSnoozeState(DateTime now, int? remainingSnoozeIntervalMs = null)
        {
            UserState = UserInteractionState.SnoozeTime;
            SnoozeTimerStarted = now;

            //determine interval
            int configuredIntervalMs = SnoozeIntervalMinutes.Value * 60 * 1000;
            int snoozeIntervalToUse = remainingSnoozeIntervalMs ?? configuredIntervalMs;

            //start timer
            SnoozeTimer.Interval = snoozeIntervalToUse;
            Log.Logger.Information($"Starting snooze timer with intveral [Interval = {snoozeIntervalToUse} ms]");
            SnoozeTimer.Start();
        }

        protected virtual void SnoozeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Log.Logger.Information($"Handling elapsed snooze timer");

            DateTime now = DateTime.UtcNow;
            HandleSnoozeElapsed(now);

            Log.Logger.Information($"Handling elapsed snooze timer done");
        }

        protected virtual void HandleSnoozeElapsed(DateTime now)
        {
            if (UserState != UserInteractionState.SnoozeTime)
            {
                //only expected to happen as a result of not well handled concurrency
                //this may put component in a deadlock probably! Will disable+enabled unblock the state?
                Log.Logger.Error($"UserState is {UserState} instead of {UserInteractionState.SnoozeTime} after snooze period elapsed. Ignoring timer event.");
                return;
            }

            var nextReminderToRing = ElapsedActiveReminders.FirstOrDefault();
            if (nextReminderToRing == null)
            {
                //only expected to happen as a result of not well handled concurrency
                Log.Logger.Error($"No reminder found after snooze period elapsed. Going to {UserInteractionState.NoElapsedReminders} state.");
                UserState = UserInteractionState.NoElapsedReminders;
                return;
            }

            GoToRingingState(nextReminderToRing.Name, now);
        }


        #region Changing snooze settings
        /// <summary>
        /// Set new snooze settings. Settings will take effect immediately.
        /// </summary>
        public virtual void ConfigureSnooze(bool snoozeEnabled, int snoozeIntervalMinutes)
        {
            DateTime now = DateTime.UtcNow;
            int? oldSnoozeInterval = SnoozeIntervalMinutes;
            SnoozeTimer.Stop(); //preventing concurrency issues, we will restart it again if state was indeed SnoozeTime

            UpdateSnoozeIntervalPropertyValue(snoozeEnabled, snoozeIntervalMinutes);

            bool snoozeIntervalChanged = oldSnoozeInterval != SnoozeIntervalMinutes;
            if (snoozeIntervalChanged && UserState == UserInteractionState.SnoozeTime)
            {
                SetStateAfterSnoozeIntervalChange(now, snoozeIntervalChanged);
            }
        }

        protected virtual void UpdateSnoozeIntervalPropertyValue(bool snoozeEnabled, int snoozeIntervalMinutes)
        {
            if (snoozeEnabled)
            {
                if (snoozeIntervalMinutes > 0)
                {
                    Log.Logger.Information($"Setting snooze value [value = {snoozeIntervalMinutes} min]."); //this log line is not redaunt in some flows
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
                Log.Logger.Information($"Snooze feature disabled.");
                SnoozeIntervalMinutes = null;
            }
        }

        /// <summary>
        /// When snooze interval is changed or even disabled during snooze time, update current state (i.e. update the snooze timer)
        /// </summary>
        protected virtual void SetStateAfterSnoozeIntervalChange(DateTime now, bool snoozeIntervalChanged)
        {
            if (SnoozeIntervalMinutes == null)
            {
                Log.Logger.Information($"Snooze value changed to null and UserState was SnoozeTime. Attemting to run remaning reminders immediately (if there are any).");
                GoToRingingOrIdleState(now);
            }
            else
            {
                int remainingSnoozeTimeMs = CalculateRemainingSnoozeTime(now);
                Log.Logger.Information($"Snooze value changed to [value = {SnoozeIntervalMinutes} min] and UserState was SnoozeTime. Rescheduling snooze timer to [value = {remainingSnoozeTimeMs} ms]  ");

                GoToSnoozeState(now, remainingSnoozeTimeMs);
            }
        }

        /// <summary>
        /// Calculate remaining snooze time from new snooze time interval and already elapsed snooze time.
        /// </summary>
        protected virtual int CalculateRemainingSnoozeTime(DateTime now)
        {
            TimeSpan alreadyElapsedTime = now - SnoozeTimerStarted.Value; //nullcehck for additional robustness?
            int alreadyElapsedTimeMs = (int)alreadyElapsedTime.TotalMilliseconds;

            int snoozePeriodMs = SnoozeIntervalMinutes.Value * 60 * 1000;

            const int minimalTimerIntervalMs = 1; //timer can't handle 0ms or negative value

            int remainingSnoozeTimeMs = Math.Max(snoozePeriodMs - alreadyElapsedTimeMs, minimalTimerIntervalMs);
            return remainingSnoozeTimeMs;
        }
        #endregion


        //TODO: providte functionallity to run immediately all snooze reminders, not to bother user later with them. This will also be useful for IsOkToModifyReminder situation

        public virtual bool IsOkToModifyReminder(string reminderName)
        {
            bool reminderIsInList = ElapsedActiveReminders.Any(rem => rem.Name == reminderName);
            DateTime now = DateTime.UtcNow;

            //TODO: instead of running them now, it would be more polite to ask user if he wants to run the reminder now. It would be even better to just update the reminders immediately, but that is too complex for now (see UpdateReminderList method)
            if (reminderIsInList && UserState != UserInteractionState.Disabled)
            {
                Log.Logger.Information("Change of elapsed but not dismissed reminder was attempted. Running snoozed reminders now.");

                SnoozeTimer.Stop();
                GoToRingingOrIdleState(now); //TODO: this may be unsafe as maybe we are not in snooze state, but e.g. already waiting user reaction?
            }

            return !reminderIsInList;
        }

        #region DEAD CODE
        //DEAD CODE FOR NOW
        /// <summary>
        /// We are not introducing new reminders through this method, we are just filtering out timers that are possibly not for ringing any more (and that are deleted) or modifying the existing ones
        /// </summary>
        public virtual void UpdateReminderList(IList<ReminderEntity> upToDateReminders)
        {
            Log.Logger.Information("Updating list of reminders in UserInteractionManger");

            DateTime now = DateTime.UtcNow; //good to be constant in a variable during this analysis in method so that it doesn't change during analysis. It could make some kind of timer deadlock where timer would never ring.

            //pause timer util we decide when should it ring again
            if (UserState != UserInteractionState.Disabled)
            {
                Log.Logger.Information("Pausing UserInteractionManger timer (if it is running at all)");
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
                    //TODO: handle scenario wehere this is consequence of renaming elapsed remiinder, because would be able to bypass preventing of "deletion" of elapsed timers (whcih is in plan to be added)
                }
            }

            bool willFirstReminderBeRemoved = WillFirstReminderBeRemoved(remindersToBeRemoved);
            if (willFirstReminderBeRemoved)
            {
                //handle state change
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
            if (UserState != UserInteractionState.Disabled)
            {
                //TryToScheduleNextReminder(now);
            }

            Log.Logger.Information("Updating list of reminders in ReminderScheduler done");
        }

        //DEAD CODE FOR NOW
        protected virtual bool WillFirstReminderBeRemoved(List<ReminderEntity> remindersToBeRemoved)
        {
            if (!ElapsedActiveReminders.Any())
            {
                //it will not be removed because it doesn't exist
                return false;
            }

            ReminderEntity firstReminder = ElapsedActiveReminders.First();
            bool firstReminderWillBeRemoved = remindersToBeRemoved.Any(rem => rem.Name == firstReminder.Name);

            return firstReminderWillBeRemoved;
        }
        #endregion

    }
}
