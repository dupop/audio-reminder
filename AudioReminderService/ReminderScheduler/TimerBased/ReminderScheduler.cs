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

    class ReminderScheduler
    {
        bool IsRunning { get; set; }
        protected Timer nextReminderTimer { get; set; }
        protected List<ReminderEntity> ActiveSortedReminders { get; set; }
        protected ServiceSettingsDto ServiceSettings { get; set; }

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
            nextReminderTimer.AutoReset = false;
            nextReminderTimer.Elapsed += NextReminderTimer_Elapsed;

            IsRunning = false;
        }

        private void NextReminderTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //TODO: maybe do sam verification if this reminder is indeed correct, and handle situation if not? probably not needed?
            if (ActiveSortedReminders?.Any() != true)
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

            Log.Logger.Information("Starting TimerScheduler done");
        }

        public void Stop()
        {
            Log.Logger.Information("Stopping TimerScheduler");

            if (!IsRunning)
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
            if (IsRunning)
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
        protected virtual int GetTimeInMsUntilNextRinging()
        {
            ReminderEntity nextReminder = ActiveSortedReminders.First();

            TimeSpan snoozeInterval = new TimeSpan(0, ServiceSettings.SnoozeIntervalMinutes, 0);
            int minTimerLength = 1;//Timer can't handle 0 ms. 0 ms could be result of rounding from ticks to miliseconds

            //good to be constant in a variable during this analysis in method so that it doesn't change during analysis. It could make some kind of timer deadlock where timer would never ring.
            DateTime now = DateTime.UtcNow;

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
        #endregion


        #region Timer Callbacks
        protected void OnReminderTimeup(string reminderName)
        {
            Log.Logger.Information("TimerScheduler triggering a ring");

            LastReminderRinging = DateTime.UtcNow;
            ReminderTimeUp?.Invoke(reminderName);

            Log.Logger.Information("TimerScheduler triggering a ring done");
        }

        #endregion

        //TODO: check if quartz or other dependency have time calculation library
        //TODO: add more unit tests, and plotting of methods as graph f(x) = y to find edge cases
        //TODO: After e.g. 1 year of not using service shoud we show that all recuring reminders are missed?
        //TODO: Consider option of using sched + new TimeStamp, see datetime aritchmetic rules
        //TODO: check again if same timestamp is everwhere passed and used in complete algorightm to prevent contradicting situations that some condition is true and few lines later the same condition is false
        public void DismissReminder(ReminderEntity reminderEntity)
        {
            if (!new ReminderDismissableValidator().ValidateReminderShouldBeRinging(reminderEntity))
            {
                return;
            }

            reminderEntity.LastDismissedOccurence = reminderEntity.ScheduledTime;//TODO: instead of this, last passed occureance of event should be dismissed, maybe 3 more times event occured until now

            if (reminderEntity.IsRepeatable())
            {
                DateTime now = DateTime.UtcNow;

                reminderEntity.ScheduledTime = new NextReminderOccurenceCalculator().GetNextReminderOccurence(reminderEntity, now).Value;
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


        public virtual void UpdateSettings(ServiceSettingsDto serviceSettingsDto)
        {
            ServiceSettings = serviceSettingsDto;

            //TODO: react on this change
        }
    }
}
