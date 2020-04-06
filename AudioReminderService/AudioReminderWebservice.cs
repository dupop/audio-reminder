using AudioReminderService.RingingCaller;
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

namespace AudioReminderService
{
    class AudioReminderWebservice : IAudioReminderService
    {
        public void Delete(string reminderName)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [reminderName = {reminderName}]");

            FilePersistenceAdapters.RemiderFilePersistence.Entities.RemoveAll(reminder => reminder.Name == reminderName);
            FilePersistenceAdapters.RemiderFilePersistence.TriggerEntitesChangedEvent();
        }

        public ReminderEntity Load(string reminderName)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [reminderName = {reminderName}]");

            ReminderEntity reminderWithThisName = FilePersistenceAdapters.RemiderFilePersistence.Entities.FirstOrDefault(r => r.Name == reminderName);

            return reminderWithThisName;
        }

        public ReminderEntity[] LoadAll()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            return FilePersistenceAdapters.RemiderFilePersistence.Entities.ToArray();
        }

        public ServiceSettingsDto LoadSettings()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            return FilePersistenceAdapters.SettingsFilePersistence.Entities.First();
        }

        public void SnoozeReminder()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            //TODO: implement
        }

        public void Save(ReminderEntity createdReminder)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [Name = {createdReminder?.Name}]");

            FilePersistenceAdapters.RemiderFilePersistence.Entities.Add(createdReminder);
            FilePersistenceAdapters.RemiderFilePersistence.TriggerEntitesChangedEvent();
        }

        public void Update(string reminderOldName, ReminderEntity reminder)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [reminderOldName = {reminderOldName}]");

            //remove old reminder and add updated one
            FilePersistenceAdapters.RemiderFilePersistence.Entities.RemoveAll(r => r.Name == reminderOldName);
            FilePersistenceAdapters.RemiderFilePersistence.Entities.Add(reminder);

            FilePersistenceAdapters.RemiderFilePersistence.TriggerEntitesChangedEvent();
        }

        public void UpdateSettings(ServiceSettingsDto settings)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            FilePersistenceAdapters.SettingsFilePersistence.Entities.Clear();
            FilePersistenceAdapters.SettingsFilePersistence.Entities.Add(settings);
        }

        public void DismissReminder(string reminderName)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [reminderName = {reminderName}]");

            ReminderEntity reminderWithThisName = FilePersistenceAdapters.RemiderFilePersistence.Entities.FirstOrDefault(r => r.Name == reminderName);

            reminderWithThisName.Dismissed = true;
            FilePersistenceAdapters.RemiderFilePersistence.TriggerEntitesChangedEvent();
        }

        public void SnoozeReminder(string reminderName)
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called [reminderName = {reminderName}]");

            ReminderEntity reminderWithThisName = FilePersistenceAdapters.RemiderFilePersistence.Entities.FirstOrDefault(r => r.Name == reminderName);

            //TODO: do we need any implementation regarding this at all, should we maybe keep status DialogShown, and not show again for some time if there is no (at least) snooze response?
            FilePersistenceAdapters.RemiderFilePersistence.TriggerEntitesChangedEvent();
        }

        public void TestRinging()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            new RingingClinetPipeHandler().TriggerRinging("test3");//TODO: use some special name for ringer testing by user
        }

        public void TestBeeper()
        {
            Log.Logger.Information($"Webservice \"{MethodBase.GetCurrentMethod().Name}\" operation called");

            //TODO: call beeper instead of ringing
            new RingingClinetPipeHandler().TriggerRinging("test3");
        }
    }
}
