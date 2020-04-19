﻿using AudioReminderCore.Model;
using Quartz;
using Quartz.Impl;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AudioReminderService.Scheduler.QuartzBased
{
    class QuartzReminderScheduler : IReminderScheduler
    {
        public event Action<string> ReminderTimeUp;
        public event Action BeeperTimeUp;

        #region Interface for controling Quartz
        public void Start()
        {
            Log.Logger.Information("Starting QuartzWrapper");

            //Quartz.
            //Quartz.
            //TODO: Check if we should remove Quartz dependency or it has something useful for this project?
            //ISchedulerFactory sf = new StdSchedulerFactory();
            //Task<IScheduler> sched = sf.getScheduler();

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



            Log.Logger.Information("Updating list of reminders in QuartzWrapper done");
        }
        #endregion


        #region Callbacks for Quartz jobs
        protected void OnReminderTimeup(string reminderName)
        {
            Log.Logger.Information("QuartzWrapper triggering a ring");

            ReminderTimeUp?.Invoke(reminderName);

            Log.Logger.Information("QuartzWrapper triggering a ring done");
        }

        protected void OnBeeperTimeUp()
        {
            Log.Logger.Information("QuartzWrapper triggering a beep");

            BeeperTimeUp?.Invoke();

            Log.Logger.Information("QuartzWrapper triggering a beep done");
        }

        public void DismissReminder(ReminderEntity reminder)
        {
            throw new NotImplementedException();
        }

        public void SnoozeReminder(ReminderEntity reminder)
        {
            throw new NotImplementedException();
        }

        public void UpdateSettings(ServiceSettingsEntity serviceSettingsEntity)
        {
            throw new NotImplementedException();
        }

        public bool IsOkToModifyReminder(string reminderName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
