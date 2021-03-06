﻿using AudioReminderCore;
using AudioReminderCore.ClientProxies;
using AudioReminderCore.Interfaces;
using AudioReminderCore.Model;
using AudioReminderUI;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioReminderUI
{
    //TODO: Is this proxying of proxy too much redudant? Move validations to webservice, check if we need any of them here. Should we used AudioReminderWebServiceClient directly? Maybe handle this by some response value or code?
    //TODO: Handle no service present. Prevent UI starting without it. Same for ringer.
    public class PersistenceAdapter : IPersistenceAdapter
    {
        AudioReminderWebServiceClient Proxy { get; set; }

        #region Proxy setup
        public PersistenceAdapter()
        {
            SetProxy();
        }

        protected void SetProxy()
        {
            Proxy = new AudioReminderWebServiceClient();
        }
        #endregion

        public virtual void Save(ReminderEntity createdReminder)
        {
            if (createdReminder == null)
            {
                Log.Logger.Error($"Reminder to persist is null");
                throw new ArgumentNullException("Reminder to persist is null");
            }

            Log.Logger.Information($"Persisting reminder '{createdReminder.Name}' ");

            SaveImplementation(createdReminder);

            Log.Logger.Information($"Persisting reminder '{createdReminder.Name}' done");
        }

        public virtual bool Update(string reminderOldName, ReminderEntity reminder)
        {
            if (reminder == null)
            {
                Log.Logger.Error($"Reminder to update is null");
                throw new ArgumentNullException("Reminder to update is null");
            }

            Log.Logger.Information($"Updating reminder '{reminder.Name}' ");

            bool result = UpdateImplementation(reminderOldName, reminder);

            Log.Logger.Information($"Updating reminder '{reminder.Name}' done with result {result}");

            return result;
        }

        public virtual ReminderEntity[] LoadAll()
        {
            Log.Logger.Information($"Loading reminders ");

            ReminderEntity[] reminders = LoadAllImplementation();

            Log.Logger.Information($"Loading reminders done");
            return reminders;
        }

        public virtual bool Delete(string reminderName)
        {
            Log.Logger.Information($"Delting reminder '{reminderName}' ");

            bool result = DeleteImplementation(reminderName);

            Log.Logger.Information($"Delting reminder '{reminderName}' done with result {result}");

            return result;
        }

        public virtual ReminderEntity Load(string reminderName)
        {
            Log.Logger.Information($"Loading reminder with name '{reminderName}'");

            var reminder = LoadImplementation(reminderName);

            return reminder;
        }

        public virtual void UpdateSettings(ServiceSettingsEntity settings)
        {
            if (settings == null)
            {
                Log.Logger.Error($"Settings to update is null");
                throw new ArgumentNullException("Settings to update is null");
            }

            Log.Logger.Information($"Updating settings");

            UpdateSettingsImplementation(settings);

            Log.Logger.Information($"Updating settings done");
        }


        public virtual ServiceSettingsEntity LoadSettings()
        {
            Log.Logger.Information($"Loading settings");

            ServiceSettingsEntity settings = LoadSettingsImplementation();

            Log.Logger.Information($"Loading settings done");
            return settings;
        }

        public virtual void TestRinging()
        {
            Log.Logger.Information($"Starting ringing example");

            Proxy.TestRinging();

            Log.Logger.Information($"Starting ringing example done");
        }

        public virtual void TestBeeper()
        {
            Log.Logger.Information($"Starting beeper example");

            Proxy.TestBeeper();

            Log.Logger.Information($"Starting beeper done");
        }

        protected virtual void SaveImplementation(ReminderEntity createdReminder)
        {
            Proxy.Save(createdReminder);
        }

        protected virtual bool UpdateImplementation(string reminderOldName, ReminderEntity reminder)
        {
            return Proxy.Update(reminderOldName, reminder);
        }

        protected virtual ReminderEntity[] LoadAllImplementation()
        {
            return Proxy.LoadAll();
        }

        protected virtual bool DeleteImplementation(string reminderName)
        {
            return Proxy.Delete(reminderName);
        }

        protected virtual ReminderEntity LoadImplementation(string reminderName)
        {
            return Proxy.Load(reminderName);
        }

        protected virtual ServiceSettingsEntity LoadSettingsImplementation()
        {
            return Proxy.LoadSettings();
        }

        protected virtual void UpdateSettingsImplementation(ServiceSettingsEntity settings)
        {
            Proxy.UpdateSettings(settings);
        }

    }
}
