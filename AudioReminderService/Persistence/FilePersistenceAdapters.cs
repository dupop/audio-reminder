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
        public static FilePersistenceAdapter<ServiceSettingsDto> SettingsFilePersistence { get; set; }

        public static void Start()
        {
            Log.Logger.Information("Starting file persistence");

            RemiderFilePersistence = new FilePersistenceAdapter<ReminderEntity>(GetDefaultReminderList());
            SettingsFilePersistence = new FilePersistenceAdapter<ServiceSettingsDto>(GetDefaultSettings());

            Log.Logger.Information("Starting file persistence done");
        }


        //TODO: blank list to be used after testing?
        private static List<ReminderEntity> GetDefaultReminderList()
        {
            return MockData.MockReminders.ToList();
        }

        //TODO: blank list to be used after testing?
        private static List<ServiceSettingsDto> GetDefaultSettings()
        {
            return new List<ServiceSettingsDto> { MockData.DefaultServiceSettings };
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
