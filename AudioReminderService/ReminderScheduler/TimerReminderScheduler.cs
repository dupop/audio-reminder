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

namespace AudioReminderService.ReminderScheduler
{
    //TODO: optimize later so that one dismiss does not cause recreating of all timers
    //TODO: extract beeper handling

    class TimerReminderScheduler : IReminderScheduler
    {
        bool IsRunning { get; set; }
        protected Timer nextReminderTimer { get; set; }
        protected List<ReminderEntity> ActiveSortedReminders { get; set; }

        public event Action<string> ReminderTimeUp;
        public event Action BeeperTimeUp;

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


        public TimerReminderScheduler()
        {
            nextReminderTimer = new Timer();
            nextReminderTimer.AutoReset = false;
            nextReminderTimer.Elapsed += NextReminderTimer_Elapsed;

            IsRunning = false;
        }

        private void NextReminderTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //TODO: maybe do sam verification if this reminder is indeed correct, and handle situation if not? probably not needed?
            if(ActiveSortedReminders?.Any() != true)
            {
                Log.Logger.Error("Elapsed Timer from scheduled can't find reminder for which ringin should be done.");
                return;
            }

            OnReminderTimeup(ActiveSortedReminders.First().Name);
        }

        #region Interface for controling TimerScheduler
        /// <summary>
        /// Initial list of reminders must be given to this object before it is started.
        /// </summary>
        public void Start()
        {
            Log.Logger.Information("Starting TimerScheduler");

            if (IsRunning)
            {
                Log.Logger.Warning("TimerScheduler is already running.");
                return;
            }

            if (ActiveSortedReminders == null)
            {
                throw new InvalidOperationException("TimerReminderScheduler must be given initial list of reminders before starting it for the first time.");
            }

            IsRunning = true;

            //Time
            //TODO: implement whole this class

            Log.Logger.Information("Starting TimerScheduler done");
        }

        public void Stop()
        {
            Log.Logger.Information("Stopping TimerScheduler");

            if(!IsRunning)
            {
                Log.Logger.Warning("TimerScheduler is already stopped.");
                return;
            }

            IsRunning = false;
            nextReminderTimer.Stop();

            Log.Logger.Information("Stopping TimerScheduler done");
        }

        public void UpdateReminderList(IList<ReminderEntity> upToDateReminders)
        {
            Log.Logger.Information("Updating list of reminders in TimerScheduler");

            //pause timer util we decide when should it ring again
            if(IsRunning)
            {
                Log.Logger.Information("Pausing timer");
                nextReminderTimer.Stop();
            }


            //TODO: maybe not do this now but when needed, we can just find 1 first; keep list ordered instead of oredering it each time
            ActiveSortedReminders = upToDateReminders
                .Where(r => !r.IsDone())
                .Select(r => (ReminderEntity)r.Clone())
                .OrderBy(r => r.ScheduledTime)
                .ToList();

            //TODO: draw detailed state diagram for all this...

            //conitune timer if there is need for this at all
            if (IsRunning && ActiveSortedReminders.Any())
            {
                int intervalMs = GetTimeInMsUntilNextRinging();
                nextReminderTimer.Interval = intervalMs;
                Log.Logger.Information($"Continuing timer with intveral [Interval = {intervalMs} ms] ");
                nextReminderTimer.Start();
            }

            Log.Logger.Information("Updating list of reminders in TimerScheduler done");
        }

        /// <summary>
        /// Expects that there is at least one active remidner
        /// </summary>
        /// <returns></returns>
        private int GetTimeInMsUntilNextRinging()
        {
            ReminderEntity nextReminder = ActiveSortedReminders.First();

            TimeSpan snoozeIntervalMs = new TimeSpan(0, 1, 0);//TODO: instead of harcoded 1min, use value from settings
            int minTimerLength = 1;//Timer can't handle 0 ms, but that could be result of rounding from ticks to miliseconds

            //good to be constant in a variable during this analysis in method so that it doesn't change during analysis. It could make some kind of timer deadlock where timer would never ring.
            DateTime now = DateTime.UtcNow;

#warning //TODO: just a mock alogirhtm, a correct one is neede here! implement this from LastReminderRinging,LastReminderDismissing,
            //LastReminderSnoozing. We sould probably not ring again at all until we get at least snooze response, but on the other
            //hand some sanity check would be good because ringer maybe got stuck so we should try another ring so that next important events are not missed?
            bool isUserAlreadyAnnoyedTooMuchRecently = LastReminderRinging != null && now - LastReminderRinging < snoozeIntervalMs; 

            bool reminderAlreadyReadyForRinging = nextReminder.ScheduledTime >= now;

            if(!reminderAlreadyReadyForRinging)
            {
                int timeMsUntilEvent = (int)(nextReminder.ScheduledTime - now).TotalMilliseconds;
                int intervalForTimer = Math.Max(timeMsUntilEvent, minTimerLength);

                return intervalForTimer;
            }

            //reminder should ring already, we should just check if we didn't ring too much regarding this event already
            if(isUserAlreadyAnnoyedTooMuchRecently)
            {
                //TODO: handle this scneario properly, adding a stub algorithm
                
                int timeMsUntilNextRinging = (int)(LastReminderRinging.Value /*not null?*/ + snoozeIntervalMs - now).TotalMilliseconds;
                int intervalForTimer = Math.Max(timeMsUntilNextRinging, minTimerLength);

                return intervalForTimer;
            }

            //reminder is ready to ring and program did not annoy him too much already - we can ring immediately
            //not triggering ring now, because I want this scenario to be handled on the sam thread as other scnearios would do, not to make additional risks
            return minTimerLength;
        }
        #endregion



        //protected DateTime? x()
        //{
        //    IEnumerable<DateTime> allDismissingDateTimes = ActiveSortedReminders?
        //        .Where(r => r.LastDismissedOccurence != null)
        //        .Select(r => r.LastDismissedOccurence.Value);

        //    if(allDismissingDateTimes?.Any() == true)
        //    {
        //        return null;
        //    }

        //    DateTime? lastReminderDismissing = allDismissingDateTimes.Max();

        //    return lastReminderDismissing;
        //} 


        #region Timer Callbacks
        protected void OnReminderTimeup(string reminderName)
        {
            Log.Logger.Information("TimerScheduler triggering a ring");

            LastReminderRinging = DateTime.UtcNow;
            ReminderTimeUp?.Invoke(reminderName);

            Log.Logger.Information("TimerScheduler triggering a ring done");
        }

        protected void OnBeeperTimeUp()
        {
            Log.Logger.Information("TimerScheduler triggering a beep");

            BeeperTimeUp?.Invoke();

            Log.Logger.Information("TimerScheduler triggering a beep done");
        }
        #endregion
        
        //public void OnReminderDismissed(ReminderEntity reminder)
        //{
        //    LastReminderDismissing = DateTime.UtcNow;
        //}

        //public void OnReminderSnoozed(ReminderEntity reminder)
        //{
        //    LastReminderSnoozing = DateTime.UtcNow;
        //}

        public void DismissReminder(ReminderEntity reminderEntity)
        {
            if (!ValidateReminderShouldBeRining(reminderEntity))
            {
                return;
            }

            reminderEntity.LastDismissedOccurence = reminderEntity.ScheduledTime;

            if (reminderEntity.IsRepeatable())
            {
                reminderEntity.ScheduledTime = GetNextReminderOccurence(reminderEntity);
            }

            //AudioReminderService.ReminderScheduler.OnReminderDismissed(reminderEntity);
            LastReminderDismissing = DateTime.UtcNow;

            FilePersistenceAdapters.RemiderFilePersistence.OnEntitesChanged(); //indirectly triggers UpdateReminderList on this object
        }

        public void SnoozeReminder(ReminderEntity reminderEntity)
        {
            //TODO: do we need any implementation regarding this at all, should we maybe keep status DialogShown, and not show again for some time if there is no (at least) snooze response?
            //AudioReminderService.ReminderScheduler.OnReminderSnoozed(reminderEntity);
            LastReminderSnoozing = DateTime.UtcNow;

            FilePersistenceAdapters.RemiderFilePersistence.OnEntitesChanged(); //indirectly triggers UpdateReminderList on this object
        }


#warning //TODO: this is just mock method - implement based on reminder repeat settings
        protected DateTime GetNextReminderOccurence(ReminderEntity reminder)
        {
            TimeSpan twoMinutes = new TimeSpan(0, 2, 0);
            DateTime mockNextOccurence = DateTime.UtcNow + twoMinutes;

            return mockNextOccurence;
        }

        /// <summary>
        /// Validates that this reminder should indeed be ringing now.
        /// Protection of some kind of double dismis request (maybe by multiple ringing windows etc..) that would cause next occurance of reminder to be skipped
        /// </summary>
        protected bool ValidateReminderShouldBeRining(ReminderEntity reminderEntity)
        {
            DateTime now = DateTime.UtcNow;
            bool reminderNotYetReady = reminderEntity.ScheduledTime > now;


            if (reminderNotYetReady)
            {
                Log.Logger.Error($"Reminder to be dismissed [reminderName = {reminderEntity.Name}] is scheduled in the future [UtcNow = {now}, scheduled time = {reminderEntity.ScheduledTime}]. Ignorring dismiss request.");
                return false;
            }

            return true;
        }
    }
}
