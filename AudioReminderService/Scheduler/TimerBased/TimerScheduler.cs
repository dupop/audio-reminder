using AudioReminderCore.Model;
using AudioReminderService.Scheduler.TimerBased.BeeperScheduling;
using AudioReminderService.Scheduler.TimerBased.ReminderScheduling;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AudioReminderService.Scheduler.TimerBased
{
    /// <summary>
    /// Wrapper around Reminder and Beeper schedulers. Fires events for beeper and reminder time up. 
    /// Provides two controlling interfaces (EnabledByService and SchedulerEnabledInSettings) and handles their combinations.
    /// Provides means to update service settings, update reminders list and handle feedback from ringing.
    /// </summary>
    class TimerScheduler : IReminderScheduler
    {
        public event Action BeeperTimeUp;
        public event Action<string> ReminderTimeUp;

        BeeperScheduler BeeperScheduler;
        ReminderScheduler ReminderScheduler;

        protected bool enabledByService;
        protected bool schedulerEnabledInSettings;

        /// <summary>
        /// Is scheduler enabled by the service. This is one of the preconditions for running the scheduler.
        /// </summary>
        public bool EnabledByService
        {
            get
            {
                return enabledByService;
            }
            set
            {
                bool wasEnabledBefore = IsRunning;

                enabledByService = value;

                HandleStatusChange(wasEnabledBefore);
            }
        }

        /// <summary>
        /// Is scheduler enabled in settings. This is one of the preconditions for running scheduler.
        /// </summary>
        public bool SchedulerEnabledInSettings
        {
            get
            {
                return schedulerEnabledInSettings;
            }
            set
            {
                bool wasEnabledBefore = IsRunning;

                schedulerEnabledInSettings = value;

                HandleStatusChange(wasEnabledBefore);
            }
        }

        /// <summary>
        /// Is beeper timer currently running.
        /// </summary>
        public bool IsRunning => enabledByService && schedulerEnabledInSettings;



        public TimerScheduler()
        {
            Log.Logger.Information($"Creating TimerScheduler");

            BeeperScheduler = new BeeperScheduler();
            ReminderScheduler = new ReminderScheduler();
            
            BeeperScheduler.BeeperTimeUp += OnBeeperTimeUp;
            ReminderScheduler.RingingNeeded += ReminderTimeUp;

            Log.Logger.Information($"Creating TimerScheduler done");
        }


        /// <summary>
        /// Starts or stops timer if value of Enabled cahnged.
        /// </summary>
        protected virtual void HandleStatusChange(bool wasEnabledBefore)
        {
            bool startedNow = !wasEnabledBefore && IsRunning;
            bool stoppedNow = wasEnabledBefore && !IsRunning;

            if (startedNow)
            {

                BeeperScheduler.SchedulerEnabled = true;
                ReminderScheduler.Start();

                Log.Logger.Information($"Started TimerScheduler");
            }
            else if (stoppedNow)
            {
                BeeperScheduler.SchedulerEnabled = false;
                ReminderScheduler.Stop();

                Log.Logger.Information($"Stopped TimerScheduler");
            }
            else
            {
                Log.Logger.Information($"TimerScheduler already {(IsRunning ? "started" : "stopped")} [enabledByService = {enabledByService}, schedulerEnabledInSettings = {schedulerEnabledInSettings}]");
            }
        }

        #region Timer Callbacks
        protected void OnBeeperTimeUp()
        {
            BeeperTimeUp?.Invoke();
        }

        protected void OnReminderTimeup(string reminderName)
        {
            ReminderTimeUp?.Invoke(reminderName);
        }
        #endregion


        #region Interface for controling TimerScheduler
        /// <summary>
        /// Initial list of reminders must be given to this object before it is started.
        /// </summary>
        public void Start()
        {
            EnabledByService = true;
        }

        public void Stop()
        {
            EnabledByService = false;
        }

        public void DismissReminder(ReminderEntity reminder)
        {
            Log.Logger.Information("Dismissing reminder in TimerScheduler");

            ReminderScheduler.DismissReminder(reminder);

            Log.Logger.Information("Dismissing reminder in TimerScheduler done");
        }

        public void SnoozeReminder(ReminderEntity reminder)
        {
            Log.Logger.Information("Snoozing reminder in TimerScheduler");

            ReminderScheduler.SnoozeReminder(reminder);

            Log.Logger.Information("Snoozing reminder in TimerScheduler done");
        }

        public void UpdateReminderList(IList<ReminderEntity> upToDateReminders)
        {
            Log.Logger.Information("Updating list of reminders in TimerScheduler");

            ReminderScheduler.UpdateReminderList(upToDateReminders);

            Log.Logger.Information("Updating list of reminders in TimerScheduler done");
        }

        /// <summary>
        /// Returns false when reminder is elapsed but not yet dismissed
        /// </summary>
        public bool IsOkToModifyReminder(string reminderName)
        {
            Log.Logger.Information("Checking if reminder can be modified in TimerScheduler");

            bool result = ReminderScheduler.IsOkToModifyReminder(reminderName);
            
            Log.Logger.Information("Checking if reminder can be modified in TimerScheduler done");

            return result;
        }

        public void UpdateSettings(ServiceSettingsEntity serviceSettingsEntity)
        {
            Log.Logger.Information("TimerScheduler updating settings");

            //TODO: remove autostart from UI. Disabling service autostart would probably be feature for future (for never) as it is too complex and doesn't bring almost any value.

            ReminderScheduler.ConfigureSnooze(serviceSettingsEntity.SnoozeEnabled, serviceSettingsEntity.SnoozeIntervalMinutes);

            BeeperScheduler.Interval = serviceSettingsEntity.BeeperIntervalMinutes;
            BeeperScheduler.BeeperEnabledInSettings = serviceSettingsEntity.BeeperEnabled;

            //we are actually only disabling the this scheduler, not complete service
            SchedulerEnabledInSettings = serviceSettingsEntity.ServiceEnabled;

            Log.Logger.Information("TimerScheduler updating settings done");

        }
        #endregion

    }
}
