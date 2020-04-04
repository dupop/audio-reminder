using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminder
{
    class FilePersistanceAdapter
    {
        private static FilePersistanceAdapter singleton;

        public static FilePersistanceAdapter Singleton => singleton ?? InitializeSingleton();

        /// <summary>
        /// Provides eager singleton initialization
        /// </summary>
        /// <returns></returns>
        public static FilePersistanceAdapter InitializeSingleton()
        {
            singleton = new FilePersistanceAdapter();

            return singleton;
        }

        //TODO: Extract persistency to new class
        private const string storageFilName = "reminders.xml";
        public List<ReminderEntity> ReminderEntities;
        public ServiceSettingsDto[] settingsDtos;

        FilePersistanceAdapter()
        {
            LoadDataFromStorage();
        }
        protected void LoadDataFromStorage()
        {
            if (File.Exists(GetFilePath()))
            {
                LoadRemindersFromFile();
            }
            else
            {
                CreateNewReminderList();
            }
        }

        private void CreateNewReminderList()
        {
            Log.Logger.Information($"Using empty (mock for now instead) list of reminders");
            ReminderEntities = AudioReminderWebservice.MockReminders.ToList();
            //File.WriteAllText(storageFilePath, "");
        }

        protected string GetFilePath()
        {
            string servicePath = AppDomain.CurrentDomain.BaseDirectory; //TODO: extract both occuranecs of this
            string name = storageFilName;

            string fullPath = Path.Combine(servicePath, name);
            string fullPathNonRelative = Path.GetFullPath(fullPath);

            return fullPathNonRelative;
        }
        private void LoadRemindersFromFile()
        {
            string filePath = GetFilePath();

            Log.Logger.Information($"Loading reminders from file [filename = {filePath}]");
            string xmlString = File.ReadAllText(filePath);
            ReminderEntities = UserInterfaceCommunication.FromXmlString<List<ReminderEntity>>(xmlString);
            //reminderEntities = reminderEntitiesArray.ToList();

            Log.Logger.Information($"Loading reminders from file done");
        }

        ////TODO: also move to persistency handler
        //~FilePersistanceAdapter()
        //{
        //    SaveRemindersToFile();
        //}

        public void SaveRemindersToFile()
        {
            string filePath = GetFilePath();

            Log.Logger.Information($"Saving reminders to file [filename = {filePath}]");
            //ReminderEntity[] remindersArray = reminderEntities.ToArray();

            string xmlString = UserInterfaceCommunication.ToXmlString(ReminderEntities);

            File.WriteAllText(filePath, xmlString);

            Log.Logger.Information($"Saving reminders to file done");
        }
    }
}
