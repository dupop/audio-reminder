using AudioReminderCore.Interfaces;
using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminder
{
    class AudioReminderWebservice : IAudioReminderService
    {
        
        

        public AudioReminderWebservice()
        {
        }

        

        public void Delete(string reminderName)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [reminderName = {reminderName}]");

            FilePersistanceAdapter.Singleton.ReminderEntities.RemoveAll(reminder => reminder.Name == reminderName);
        }

        public bool IsNameAvailable(string reminderName)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [reminderName = {reminderName}]");

            //TODO: implement
            return true;
        }

        public ReminderEntity[] Load()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            return FilePersistanceAdapter.Singleton.ReminderEntities.ToArray();
        }

        public ServiceSettingsDto LoadSettings()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            //TODO: implement
            return DefaultServiceSettings;
        }

        public void RegsiterForReminderCallBack()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            //TODO: implement
        }

        public void Save(ReminderEntity createdReminder)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [Name = {createdReminder?.Name}]");

            FilePersistanceAdapter.Singleton.ReminderEntities.Add(createdReminder);
        }

        public void Update(string reminderOldName, ReminderEntity reminder)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [reminderOldName = {reminderOldName}]");

            //TODO: implement
        }

        public void UpdateSettings(ServiceSettingsDto settings)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            //TODO: implement
        }


        #region Mock data
        public static readonly ReminderEntity[] MockReminders = new ReminderEntity[]
        {
            new ReminderEntity()
            {
                Name = "Some event on workdays",
                ScheduledTime = DateTime.Now,
                RepeatWeekly = true,
                RepeatWeeklyDays = new bool[]{true, true, true, true, true, false, false }
            },
            new ReminderEntity()
            {
                Name = "Some non-recuring once",
                ScheduledTime = DateTime.Now + new TimeSpan(6,0,0),
                RepeatWeekly = false,
                RepeatWeeklyDays = new bool[] { false, false, false, false, false, false, false }
            },
            new ReminderEntity()
            {
                Name = "Some non-recuring once2",
                ScheduledTime = DateTime.Now + new TimeSpan(7,0,0),
                RepeatWeekly = false,
                RepeatWeeklyDays = new bool[] { false, false, false, false, false, false, false }
            }
        };

        private static readonly ServiceSettingsDto DefaultServiceSettings = new ServiceSettingsDto
        {
            AutoStartService = true,
            ServiceEnabled = true,
            BeeperEnabled = false,
            BeeperIntervalMinutes = 60,
            SnoozeEnabled = true,
            SnoozeIntervalMinutes = 5
        };
        #endregion

    }
}
