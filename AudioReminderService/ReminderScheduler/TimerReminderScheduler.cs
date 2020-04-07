using AudioReminderCore.Model;
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

    class TimerReminderScheduler : IReminderScheduler
    {
        protected Timer nextReminderTimer { get; set; }
        protected List<ReminderEntity> activeSortedReminders { get; set; }

        public event Action<string> OnReminderTimeup;
        public event Action OnBeeperTimeUp;

        public TimerReminderScheduler()
        {
            nextReminderTimer = new Timer();
        }

        #region Interface for controling TimerScheduler
        public void Start()
        {
            Log.Logger.Information("Starting TimerScheduler");

            if(activeSortedReminders == null)
            {
                throw new InvalidOperationException("TimerReminderScheduler must be given initial list of reminders before starting it for the first time.");
            }

            //Time
            //TODO: implement whole this class

            Log.Logger.Information("Starting TimerScheduler done");
        }

        public void Stop()
        {
            Log.Logger.Information("Stopping TimerScheduler");

            nextReminderTimer.Stop();

            Log.Logger.Information("Stopping TimerScheduler done");
        }

        public void UpdateReminderList(IList<ReminderEntity> upToDateReminders)
        {
            Log.Logger.Information("Updating list of reminders in TimerScheduler");

            nextReminderTimer.Stop();

            //TODO: maybe not do this now but when needed, we can just find 1 first; keep list ordered instead of oredering it each time
            activeSortedReminders = upToDateReminders
                .Where(r => !r.IsDone())
                .Select(r => (ReminderEntity)r.Clone())
                .OrderBy(r => r.ScheduledTime)
                .ToList();

            Log.Logger.Information("Updating list of reminders in TimerScheduler done");
        }
        #endregion


        #region Callbacks
        protected void RingReminder(string reminderName)
        {
            Log.Logger.Information("TimerScheduler triggering a ring");

            OnReminderTimeup?.Invoke(reminderName);

            Log.Logger.Information("TimerScheduler triggering a ring done");
        }

        protected void RingBeep()
        {
            Log.Logger.Information("TimerScheduler triggering a beep");

            OnBeeperTimeUp?.Invoke();

            Log.Logger.Information("TimerScheduler triggering a beep done");
        }
        #endregion
    }
}
