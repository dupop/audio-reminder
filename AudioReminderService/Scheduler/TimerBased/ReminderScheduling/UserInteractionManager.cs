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

        protected bool isEnabled;

        /// <summary>
        /// Is making more ringing enabled     //TODO: more precise defintion   <--- this is realted to actually implementing this feature, we need to preoperly combine this with UserState somehow 
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

        /// <summary>
        /// Reminders that are elapsed but are not yet dismissed.
        /// Never null.
        /// </summary>
        protected List<ReminderEntity> ElapsedActiveReminders { get; set; }

        protected ReminderSchedulerState UserState { get; set; }

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
            Log.Logger.Information($"Creating UserInteractionManager");

            SnoozeTimer = new Timer();
            SnoozeTimer.Enabled = false;
            SnoozeTimer.AutoReset = false;
            SnoozeTimer.Elapsed += SnoozeTimer_Elapsed;

            ElapsedActiveReminders = new List<ReminderEntity>();
            IsEnabled = false;
            UserState = ReminderSchedulerState.NoElapsedReminders;

            Log.Logger.Information($"Creating UserInteractionManager done");

        }

        protected virtual void HandleStatusChange(bool wasEnabledBefore)
        {
            bool startedNow = !wasEnabledBefore && isEnabled;
            bool stoppedNow = wasEnabledBefore && !isEnabled;
            DateTime now = DateTime.UtcNow;

            if (startedNow)
            {
                Log.Logger.Information($"Starting UserInteractionManager");
                //TODO: consider current state before doing this
                GoToRingingOrIdleState(now);

                Log.Logger.Information($"Starting UserInteractionManager done");
            }
            else if (stoppedNow)
            {
                SnoozeTimer.Stop();
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

            if (ElapsedActiveReminders.Any(rem => rem.Name == reminder.Name))
            {
                Log.Logger.Information($"ElapsedActiveReminders list already contains reminder [name = {reminder.Name}]. Ignoring OnReminderElapsed event.");
                return;
            }

            ElapsedActiveReminders.Add(reminder); //TODO: is some sorting needed or it is safe to always add it at the end?

            if (!IsEnabled)
            {
                Log.Logger.Information($"OnReminderElapsed event [name = {reminder.Name}] will be handled just by adding reminder to the list because UserInteractionManager is currently not enabled.");
                return;
            }

            Log.Logger.Information($"Processing OnReminderElapsed event [name = {reminder.Name}, current component state = {UserState}, isEnabled = true].");
            switch (UserState)
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
            //TODO:  should we start snooze timer here, or even before that a timer for turning off the noise of this reminder after e.g. 1 min of no response from user?
            UserState = ReminderSchedulerState.WaitingUserResponse;
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
        //todo: disable, even better hide snooze button if that feature is disabled, we will probably need to handle closing the form as dismiss, not snooze
        //todo: validation if reminders as paramters to dismiss and snooze, are indeed excepted (or we first expect some other), or just remove them (less safe)
        public void DismissReminder(ReminderEntity reminderEntity)
        {
            DateTime now = DateTime.UtcNow;

            //TODO: this is proably not expexted to happen during disabled state, but I assume it should make no harm to react on this bacause user can still have old ringing dialogs

            if (UserState != ReminderSchedulerState.WaitingUserResponse)
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

            //TODO: skip when disabled?
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
                UserState = ReminderSchedulerState.NoElapsedReminders;
                Log.Logger.Information($"No more elapsed reminders in the list. GoToRingingOrIdleState method is setting state to NoElapsedReminders");
            }
        }

        public void SnoozeReminder(ReminderEntity reminderEntity)
        {
            DateTime now = DateTime.UtcNow;

            if (UserState != ReminderSchedulerState.WaitingUserResponse)
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

            //TODO: how to handle this when disabled?
            GoToSnoozeState(now);
        }

        protected virtual void GoToSnoozeState(DateTime now, int? customSnoozeIntervalMs = null)
        {
            UserState = ReminderSchedulerState.SnoozeTime;
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
            Log.Logger.Information($"Handling elapsed snooze timer");
            
            DateTime now = DateTime.UtcNow;
            HandleSnoozeElapsed(now);
            
            Log.Logger.Information($"Handling elapsed snooze timer done");
        }

        protected virtual void HandleSnoozeElapsed(DateTime now)
        {
            if (UserState != ReminderSchedulerState.SnoozeTime)
            {
                //only expected to happen as a result of not well handled concurrency
                //this may put component in a deadlock probably! Will disable+enabled unblock the state?
                Log.Logger.Error($"UserState is {UserState} instead of {ReminderSchedulerState.SnoozeTime} after snooze period elapsed. Ignoring timer event.");
                return;
            }

            var nextReminderToRing = ElapsedActiveReminders.FirstOrDefault();
            if (nextReminderToRing == null)
            {
                //only expected to happen as a result of not well handled concurrency
                Log.Logger.Error($"No reminder found after snooze period elapsed. Going to {ReminderSchedulerState.NoElapsedReminders} state.");
                UserState = ReminderSchedulerState.NoElapsedReminders;
                return;
            }

            GoToRingingState(nextReminderToRing.Name, now);
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
                SnoozeIntervalMinutes = null;
            }

            bool snoozeIntervalChanged = oldSnoozeInterval != SnoozeIntervalMinutes;

            if (snoozeIntervalChanged && UserState == ReminderSchedulerState.SnoozeTime)
            {
                //TODO: how to handle when disabled? should component go out of snoozeTime state when we it becamoes disabled?

                if (SnoozeIntervalMinutes == null)
                {
                    Log.Logger.Information($"Snooze value changed to null and UserState was SnoozeTime. Attemting to run remaning reminders immediately (if there are any).");
                    GoToRingingOrIdleState(now);
                }
                else
                {
                    //uses new snoose interval while recalculating the remaining time
                    int remainingSnoozeTimeMs = CalculateRemainingSnoozeTime(now);
                    Log.Logger.Information($"Snooze value changed to [value = {snoozeIntervalMinutes} min] and UserState was SnoozeTime. Rescheduling snooze timer to [value = {remainingSnoozeTimeMs} ms]  ");

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



        //TODO: providte functionallity to run immediately all snooze reminders, not to bother user later with them. This will also be useful for IsOkToModifyReminder situation

        public virtual bool IsOkToModifyReminder(string reminderName)
        {
            bool reminderIsInList = ElapsedActiveReminders.Any(rem => rem.Name == reminderName);
            DateTime now = DateTime.UtcNow;

            //TODO: instead of running yhem now, it would be more polite to ask user if he wants to run the reminder now. It would be even better to just update the reminders immediately, but that is too complex for now (see UpdateReminderList method)
            if (reminderIsInList)
            {
                Log.Logger.Information("Change of elapsed but not dismissed reminder was attempted. Running snoozed reminders now.");

                //TODO: how to handle this when disabled?
                SnoozeTimer.Stop();
                GoToRingingOrIdleState(now); //TODO: this may be unsafe as maybe we are not in snooze state, but e.g. already waiting user reaction?
            }

            return !reminderIsInList;
        }

        //DEAD CODE FOR NOW
        /// <summary>
        /// We are not introducing new reminders through this method, we are just filtering out timers that are possibly not for ringing any more (and that are deleted) or modifying the existing ones
        /// </summary>
        public virtual void UpdateReminderList(IList<ReminderEntity> upToDateReminders)
        {
            Log.Logger.Information("Updating list of reminders in UserInteractionManger");

            DateTime now = DateTime.UtcNow; //good to be constant in a variable during this analysis in method so that it doesn't change during analysis. It could make some kind of timer deadlock where timer would never ring.

            //pause timer util we decide when should it ring again
            if (IsEnabled)
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
            if(willFirstReminderBeRemoved)
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
            if (IsEnabled)
            {
                //TryToScheduleNextReminder(now);
            }

            Log.Logger.Information("Updating list of reminders in ReminderScheduler done");
        }

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


    }
}
