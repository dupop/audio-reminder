using AudioReminderCore.Interfaces;
using AudioReminderCore.Model;
using AudioReminderService.Persistence;
using AudioReminderService.RingerCalling;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.WebService
{
    class AudioReminderWebservice : IAudioReminderService
    {
        public void Delete(string reminderName)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" operation [reminderName = {reminderName}]");

            FilePersistenceAdapters.RemiderFilePersistence.Entities.RemoveAll(reminder => reminder.Name == reminderName);
            FilePersistenceAdapters.RemiderFilePersistence.TriggerEntitesChangedEvent();
        }

        public ReminderEntity Load(string reminderName)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" operation [reminderName = {reminderName}]");

            ReminderEntity reminderWithThisName = FilePersistenceAdapters.RemiderFilePersistence.Entities.FirstOrDefault(r => r.Name == reminderName);

            return reminderWithThisName;
        }

        public ReminderEntity[] LoadAll()
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" operation");

            return FilePersistenceAdapters.RemiderFilePersistence.Entities.ToArray();
        }

        public ServiceSettingsDto LoadSettings()
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" operation");

            return FilePersistenceAdapters.SettingsFilePersistence.Entities.First();
        }

        public void Save(ReminderEntity createdReminder)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" operation [Name = {createdReminder?.Name}]");

            FilePersistenceAdapters.RemiderFilePersistence.Entities.Add(createdReminder);
            FilePersistenceAdapters.RemiderFilePersistence.TriggerEntitesChangedEvent();
        }

        public void Update(string reminderOldName, ReminderEntity reminder)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" operation [reminderOldName = {reminderOldName}]");

            //remove old reminder and add updated one
            FilePersistenceAdapters.RemiderFilePersistence.Entities.RemoveAll(r => r.Name == reminderOldName);
            FilePersistenceAdapters.RemiderFilePersistence.Entities.Add(reminder);

            FilePersistenceAdapters.RemiderFilePersistence.TriggerEntitesChangedEvent();
        }

        public void UpdateSettings(ServiceSettingsDto settings)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" operation");

            FilePersistenceAdapters.SettingsFilePersistence.Entities.Clear();
            FilePersistenceAdapters.SettingsFilePersistence.Entities.Add(settings);
        }

        public void DismissReminder(string reminderName)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" operation [reminderName = {reminderName}]");

            ReminderEntity reminderWithThisName = FilePersistenceAdapters.RemiderFilePersistence.Entities.FirstOrDefault(r => r.Name == reminderName);
            
            ReminderDissmisingHelper.DismissReminder(reminderWithThisName);
        }

        public void SnoozeReminder(string reminderName)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" operation [reminderName = {reminderName}]");

            ReminderEntity reminderWithThisName = FilePersistenceAdapters.RemiderFilePersistence.Entities.FirstOrDefault(r => r.Name == reminderName);

            ReminderDissmisingHelper.SnoozeReminder(reminderWithThisName);
        }

        public void TestRinging()
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" operation");

            RingingCaller.RingReminderTest();
        }

        public void TestBeeper()
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" operation");

            RingingCaller.RingBeep();
        }
    }
}
