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

            FilePersistenceAdapters.RemiderFilePersistence.Entities.RemoveAll(reminder => reminder.Name == reminderName);
        }

        public bool IsNameAvailable(string reminderName)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [reminderName = {reminderName}]");

            bool nameTaken = FilePersistenceAdapters.RemiderFilePersistence.Entities.Any(r => r.Name == reminderName);

            return !nameTaken;
        }

        public ReminderEntity[] Load()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            return FilePersistenceAdapters.RemiderFilePersistence.Entities.ToArray();
        }

        public ServiceSettingsDto LoadSettings()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            return FilePersistenceAdapters.SettingsFilePersistence.Entities.First();
        }

        public void RegsiterForReminderCallBack()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            //TODO: implement
        }

        public void Save(ReminderEntity createdReminder)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [Name = {createdReminder?.Name}]");

            FilePersistenceAdapters.RemiderFilePersistence.Entities.Add(createdReminder);
        }

        public void Update(string reminderOldName, ReminderEntity reminder)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [reminderOldName = {reminderOldName}]");

            //remove old reminder and add updated one
            FilePersistenceAdapters.RemiderFilePersistence.Entities.RemoveAll(r => r.Name == reminderOldName);
            FilePersistenceAdapters.RemiderFilePersistence.Entities.Add(reminder);
        }

        public void UpdateSettings(ServiceSettingsDto settings)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            FilePersistenceAdapters.SettingsFilePersistence.Entities.Clear();
            FilePersistenceAdapters.SettingsFilePersistence.Entities.Add(settings);
        }

    }
}
