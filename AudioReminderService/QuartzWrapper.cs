using AudioReminderCore.Model;
using AudioReminderService.RingingCaller;
using Quartz;
using Quartz.Impl;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService
{
    class QuartzWrapper
    {
        #region Singleton
        private static QuartzWrapper singleton;

        public static QuartzWrapper Singleton => singleton ?? InitializeSingleton();

        /// <summary>
        /// Provides eager singleton initialization
        /// </summary>
        /// <returns></returns>
        public static QuartzWrapper InitializeSingleton()
        {
            singleton = new QuartzWrapper();

            return singleton;
        }
        #endregion


        public void Start()
        {
            Log.Logger.Information("Starting QuartzWrapper");

            //Quartz.
            //TODO: implement whole this class
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

        #region Callbacks for Quartz jobs
        protected void RingReminder(string reminderName)
        {
            Log.Logger.Information("QuartzWrapper triggering a ring");
            
            new RingingClinetPipeHandler().RingReminder(reminderName);
            
            Log.Logger.Information("QuartzWrapper triggering a ring done");
        }

        protected void RingBeep()
        {
            Log.Logger.Information("QuartzWrapper triggering a beep");

            new RingingClinetPipeHandler().RingBeep();

            Log.Logger.Information("QuartzWrapper triggering a beep done");
        }
        #endregion
    }
}
