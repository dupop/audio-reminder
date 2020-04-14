using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.Persistence
{
    static class FilePersistenceAdapters
    {
        public static FilePersistenceAdapter<ReminderEntity> RemiderFilePersistence { get; set; }
        public static FilePersistenceAdapter<ServiceSettingsEntity> SettingsFilePersistence { get; set; }

        public static void Start()
        {
            Log.Logger.Information("Starting file persistence");

            RemiderFilePersistence = new FilePersistenceAdapter<ReminderEntity>(GetDefaultReminderList());
            SettingsFilePersistence = new FilePersistenceAdapter<ServiceSettingsEntity>(GetDefaultSettings());

            Log.Logger.Information("Starting file persistence done");
        }


        private static List<ReminderEntity> GetDefaultReminderList()
        {
            List<ReminderEntity> defaultReminderList;

#if DEBUG
            defaultReminderList = MockData.MockReminders.ToList();
#else
            defaultReminderList = new List<ReminderEntity>();
#endif

            return defaultReminderList;
        }

        private static List<ServiceSettingsEntity> GetDefaultSettings()
        {
            return new List<ServiceSettingsEntity> { MockData.DefaultServiceSettings };
        }

        public static void Stop()
        {
            Log.Logger.Information("Stopping file persistence");

            RemiderFilePersistence.SaveEntitiesToFile();
            SettingsFilePersistence.SaveEntitiesToFile();

            Log.Logger.Information("Stopping file persistence done");
        }
    }
}
