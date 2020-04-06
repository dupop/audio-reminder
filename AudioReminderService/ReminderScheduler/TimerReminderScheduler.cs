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
        protected Timer reminderTimer { get; set; }
        protected List<ReminderEntity> activeSortedReminders { get; set; }

        public event Action<string> OnReminderTimeup;
        public event Action OnBeeperTimeUp;

        public TimerReminderScheduler()
        {
            reminderTimer = new Timer();
        }

        #region Interface for controling TimerScheduler
        public void Start()
        {
            Log.Logger.Information("Starting QuartzWrapper");

            if(activeSortedReminders == null)
            {

            }
            //Time
            //TODO: implement whole this class

            Log.Logger.Information("Starting QuartzWrapper done");
        }

        public void Stop()
        {
            Log.Logger.Information("Stopping QuartzWrapper");



            Log.Logger.Information("Stopping QuartzWrapper done");
        }

        public void UpdateReminderList(IList<ReminderEntity> upToDateReminders)
        {
            Log.Logger.Information("Updating list of reminders in QuartzWrapper");

            //ReminderEntity e;
            //ReminderEntity v = e.Mem;

            //TODO: maybe not do this now but when needed, we can just find 1 first
            activeSortedReminders = upToDateReminders
                .Where(r => !r.Dismissed)
                .Select(r => (ReminderEntity)r.Clone())
                .OrderBy(r => r.ScheduledTime)
                .ToList();

            Log.Logger.Information("Updating list of reminders in QuartzWrapper done");
        }
        #endregion


        #region Callbacks
        protected void RingReminder(string reminderName)
        {
            Log.Logger.Information("QuartzWrapper triggering a ring");

            OnReminderTimeup?.Invoke(reminderName);

            Log.Logger.Information("QuartzWrapper triggering a ring done");
        }

        protected void RingBeep()
        {
            Log.Logger.Information("QuartzWrapper triggering a beep");

            OnBeeperTimeUp?.Invoke();

            Log.Logger.Information("QuartzWrapper triggering a beep done");
        }
        #endregion
    }
}
