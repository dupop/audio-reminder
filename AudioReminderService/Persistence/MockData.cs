using AudioReminderCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.Persistence
{
    public static class MockData
    {

        #region Mock data
        public static readonly ReminderEntity[] MockReminders = new ReminderEntity[]
        {
            new ReminderEntity()
            {
                Name = "Some_event_on_workdays",
                ScheduledTime = DateTime.Now,
                RepeatPeriod = RepeatPeriod.Weekly,
                RepeatWeeklyDays = new bool[]{true, true, true, true, true, false, false }
            },
            new ReminderEntity()
            {
                Name = "Some_non-recuring_once",
                ScheduledTime = DateTime.Now + new TimeSpan(6,0,0),
                RepeatPeriod = RepeatPeriod.NoRepeat,
                RepeatWeeklyDays = new bool[] { false, false, false, false, false, false, false }
            },
            new ReminderEntity()
            {
                Name = "Some_non-recuring_once2",
                ScheduledTime = DateTime.Now + new TimeSpan(7,0,0),
                RepeatPeriod = RepeatPeriod.NoRepeat,
                RepeatWeeklyDays = new bool[] { false, false, false, false, false, false, false }
            }
        };

        public static readonly ServiceSettingsEntity DefaultServiceSettings = new ServiceSettingsEntity
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
