using AudioReminderCore.Model;
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
    }
}
