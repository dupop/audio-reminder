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

            AudioReminderService.RemiderFilePersistence.Entities.RemoveAll(reminder => reminder.Name == reminderName);
        }

        public bool IsNameAvailable(string reminderName)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [reminderName = {reminderName}]");

            bool nameTaken = AudioReminderService.RemiderFilePersistence.Entities.Any(r => r.Name == reminderName);

            return !nameTaken;
        }

        public ReminderEntity[] Load()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            return AudioReminderService.RemiderFilePersistence.Entities.ToArray();
        }

        public ServiceSettingsDto LoadSettings()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            return AudioReminderService.SettingsFilePersistence.Entities.First();
        }

        public void RegsiterForReminderCallBack()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            //TODO: implement
        }

        public void Save(ReminderEntity createdReminder)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [Name = {createdReminder?.Name}]");

            AudioReminderService.RemiderFilePersistence.Entities.Add(createdReminder);
        }

        public void Update(string reminderOldName, ReminderEntity reminder)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [reminderOldName = {reminderOldName}]");

            //remove old reminder and add updated one
            AudioReminderService.RemiderFilePersistence.Entities.RemoveAll(r => r.Name == reminderOldName);
            AudioReminderService.RemiderFilePersistence.Entities.Add(reminder);
        }

        public void UpdateSettings(ServiceSettingsDto settings)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            AudioReminderService.SettingsFilePersistence.Entities.Clear();
            AudioReminderService.SettingsFilePersistence.Entities.Add(settings);
        }

    }
}
