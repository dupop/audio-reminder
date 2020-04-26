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
        public bool Delete(string reminderName)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" [reminderName = {reminderName}]");


            if (!AudioReminderService.scheduler.IsOkToModifyReminder(reminderName)) //TODO put nullcheck somwhere
            {
                return false;
            }

            FilePersistenceAdapters.RemiderFilePersistence.Entities.RemoveAll(reminder => reminder.Name == reminderName);
            FilePersistenceAdapters.RemiderFilePersistence.OnEntitesChanged();

            return true;
        }

        public ReminderEntity Load(string reminderName)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" [reminderName = {reminderName}]");

            ReminderEntity reminderWithThisName = FilePersistenceAdapters.RemiderFilePersistence.Entities.FirstOrDefault(r => r.Name == reminderName);

            return reminderWithThisName;
        }

        public ReminderEntity[] LoadAll()
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\"");

            return FilePersistenceAdapters.RemiderFilePersistence.Entities.ToArray();
        }

        public ServiceSettingsEntity LoadSettings()
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\"");

            return FilePersistenceAdapters.SettingsFilePersistence.Entities.First();
        }

        public void Save(ReminderEntity createdReminder)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" [Name = {createdReminder?.Name}]");

            FilePersistenceAdapters.RemiderFilePersistence.Entities.Add(createdReminder);
            FilePersistenceAdapters.RemiderFilePersistence.OnEntitesChanged();
        }

        public bool Update(string reminderOldName, ReminderEntity reminder)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" [reminderOldName = {reminderOldName}]");

            if (!AudioReminderService.scheduler.IsOkToModifyReminder(reminderOldName))
            {
                Log.Logger.Information($"It's NOT OK to change reminder with name [reminderOldName = {reminderOldName}]");
                return false;
            }

            //remove old reminder and add updated one
            FilePersistenceAdapters.RemiderFilePersistence.Entities.RemoveAll(r => r.Name == reminderOldName);
            FilePersistenceAdapters.RemiderFilePersistence.Entities.Add(reminder);

            FilePersistenceAdapters.RemiderFilePersistence.OnEntitesChanged();

            Log.Logger.Information($"It's OK to change reminder with name [reminderOldName = {reminderOldName}]");
            return true;
        }

        public void UpdateSettings(ServiceSettingsEntity settings)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\"");

            FilePersistenceAdapters.SettingsFilePersistence.Entities.Clear();
            FilePersistenceAdapters.SettingsFilePersistence.Entities.Add(settings);

            FilePersistenceAdapters.SettingsFilePersistence.OnEntitesChanged();
        }

        public void DismissReminder(string reminderName)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" [reminderName = {reminderName}]");

            ReminderEntity reminderWithThisName = FilePersistenceAdapters.RemiderFilePersistence.Entities.FirstOrDefault(r => r.Name == reminderName);

            AudioReminderService.scheduler.DismissReminder(reminderWithThisName);
        }

        public void SnoozeReminder(string reminderName)
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\" [reminderName = {reminderName}]");

            ReminderEntity reminderWithThisName = FilePersistenceAdapters.RemiderFilePersistence.Entities.FirstOrDefault(r => r.Name == reminderName);

            AudioReminderService.scheduler.SnoozeReminder(reminderWithThisName);
        }

        public void TestRinging()
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\"");

            RingingCaller.RingReminderTest();
        }

        public void TestBeeper()
        {
            Log.Logger.Information($"Executing webservice operation \"{MethodBase.GetCurrentMethod().Name}\"");

            RingingCaller.RingBeep();
        }
    }
}
