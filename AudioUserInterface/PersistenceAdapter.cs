using AudioUserInterface;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioUserInterface
{
    public class PersistenceAdapter : ReminderNameAvailabilityChecker
    {
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

        public virtual void Update(string reminderOldName, ReminderEntity reminder)
        {
            if (reminder == null)
            {
                Log.Logger.Error($"Reminder to update is null");
                throw new ArgumentNullException("Reminder to update is null");
            }

            Log.Logger.Information($"Updating reminder '{reminder.Name}' ");

            UpdateImplementation(reminderOldName, reminder);

            Log.Logger.Information($"Updating reminder '{reminder.Name}' done");
        }

        public virtual ReminderEntity[] Load()
        {
            Log.Logger.Information($"Loading reminders ");

            ReminderEntity[] reminders = LoadImplementation();
            
            Log.Logger.Information($"Loading reminders done");
            return reminders;
        }

        public virtual void Delete(string reminderName)
        {
            Log.Logger.Information($"Delting reminder '{reminderName}' ");

            DeleteImplementation(reminderName);

            Log.Logger.Information($"Delting reminder '{reminderName}' done");
        }

        public virtual bool IsNameAvailable(string reminderName)
        {
            Log.Logger.Information($"Checking if reminder name '{reminderName}' is avialable");

            //TODO: implmwnt
            bool nameAvialable = IsNameAvailableImplementation();

            Log.Logger.Information($"Checking reminder name '{reminderName}' done. Result is {nameAvialable}");
            return nameAvialable;
        }

        public virtual void UpdateSettings(ServiceSettingsDto settings)
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


        public virtual ServiceSettingsDto LoadSettings()
        {
            Log.Logger.Information($"Loading settings");

            ServiceSettingsDto settings = LoadSettingsImplementation();

            Log.Logger.Information($"Loading settings done");
            return settings;
        }

        protected virtual void SaveImplementation(ReminderEntity createdReminder)
        {
            //TODO: implement
        }

        protected virtual void UpdateImplementation(string reminderOldName, ReminderEntity reminder)
        {
            //TODO: implement
        }

        protected virtual ReminderEntity[] LoadImplementation()
        {
            return MockReminders;
            //TODO: implement
        }

        protected virtual void DeleteImplementation(string reminderName)
        {
            //TODO: implement
        }

        protected virtual bool IsNameAvailableImplementation()
        {
            //TODO: implement
            return true;
        }

        protected virtual ServiceSettingsDto LoadSettingsImplementation()
        {
            return DefaultServiceSettings;
            //TODO: implement
        }

        protected virtual void UpdateSettingsImplementation(ServiceSettingsDto settings)
        {
            //TODO: implement
        }

        #region Mock data
        private static readonly ReminderEntity[] MockReminders = new ReminderEntity[]
        {
            new ReminderEntity()
            {
                Name = "Some event on workdays",
                ScheduledTime = DateTime.Now,
                RepeatWeekly = true,
                RepeatWeeklyDays = new bool[]{true, true, true, true, true, false, false }
            },
            new ReminderEntity()
            {
                Name = "Some non-recuring once",
                ScheduledTime = DateTime.Now + new TimeSpan(6,0,0),
                RepeatWeekly = false,
                RepeatWeeklyDays = new bool[] { false, false, false, false, false, false, false }
            },
            new ReminderEntity()
            {
                Name = "Some non-recuring once2",
                ScheduledTime = DateTime.Now + new TimeSpan(7,0,0),
                RepeatWeekly = false,
                RepeatWeeklyDays = new bool[] { false, false, false, false, false, false, false }
            }
        };

        private static readonly ServiceSettingsDto DefaultServiceSettings = new ServiceSettingsDto
        {
            AutoStartService = true,
            ServiceEnabled = true,
            BeeperEnabled = false,
            BeeperIntervalMinutes = 60,
            SnoozeEnabled = true,
            SnoozeIntervalMinutes = 5
        };
        #endregion
    }
}
