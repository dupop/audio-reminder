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
    class TimerReminderScheduler : IReminderScheduler
    {
        Timer reminderTimer;
        IList<ReminderEntity> reminders;

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

            if(reminders == null)
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

            //reminders = upToDateReminders.clon

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
