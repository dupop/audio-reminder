using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AudioReminderService.ReminderScheduler.TimerBased
{
    class TimerScheduler : IReminderScheduler
    {
        public event Action BeeperTimeUp;
        public event Action<string> ReminderTimeUp;

        BeeperScheduler BeeperScheduler;
        ReminderScheduler ReminderScheduler;


        public TimerScheduler()
        {
            BeeperScheduler = new BeeperScheduler();
            ReminderScheduler = new ReminderScheduler();
        }


        #region Timer Callbacks
        protected void OnBeeperTimeUp()
        {
            Log.Logger.Information("TimerScheduler triggering a beep");

            BeeperTimeUp?.Invoke();

            Log.Logger.Information("TimerScheduler triggering a beep done");
        }

        protected void OnReminderTimeup(string reminderName)
        {
            Log.Logger.Information("TimerScheduler triggering a ring");

            ReminderTimeUp?.Invoke(reminderName);

            Log.Logger.Information("TimerScheduler triggering a ring done");
        }
        #endregion


        #region Interface for controling TimerScheduler
        /// <summary>
        /// Initial list of reminders must be given to this object before it is started.
        /// </summary>
        public void Start()
        {
            Log.Logger.Information("Starting TimerScheduler");

            BeeperScheduler.ServiceEnabled = true;
            ReminderScheduler.Start();

            Log.Logger.Information("Starting TimerScheduler done");
        }

        public void Stop()
        {
            Log.Logger.Information("Stopping TimerScheduler");

            BeeperScheduler.ServiceEnabled = false;
            ReminderScheduler.Stop();

            Log.Logger.Information("Stopping TimerScheduler done");
        }

        public void DismissReminder(ReminderEntity reminder)
        {
            ReminderScheduler.DismissReminder(reminder);
        }

        public void SnoozeReminder(ReminderEntity reminder)
        {
            ReminderScheduler.SnoozeReminder(reminder);
        }

        public void UpdateReminderList(IList<ReminderEntity> upToDateReminders)
        {
            ReminderScheduler.UpdateReminderList(upToDateReminders);
        }

        public void UpdateSettings(ServiceSettingsDto serviceSettingsDto)
        {
            ReminderScheduler.UpdateSettings(serviceSettingsDto);
            BeeperScheduler.Interval = serviceSettingsDto.BeeperIntervalMinutes;
            BeeperScheduler.BeeperEnabledInSettings = serviceSettingsDto.BeeperEnabled;
        }
        #endregion

    }
}
