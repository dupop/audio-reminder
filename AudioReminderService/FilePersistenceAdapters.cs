using AudioReminderCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService
{
    //TODO: maybe make non-static and place somwhere nice? or not?
    static class FilePersistenceAdapters
    {
        public static FilePersistenceAdapter<ReminderEntity> RemiderFilePersistence { get; set; }
        public static FilePersistenceAdapter<ServiceSettingsDto> SettingsFilePersistence { get; set; }

        public static void Start()
        {
            RemiderFilePersistence = new FilePersistenceAdapter<ReminderEntity>(GetDefaultReminderList());
            SettingsFilePersistence = new FilePersistenceAdapter<ServiceSettingsDto>(GetDefaultSettings());
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
            RemiderFilePersistence.SaveRemindersToFile();
            SettingsFilePersistence.SaveRemindersToFile();
        }
    }
}
