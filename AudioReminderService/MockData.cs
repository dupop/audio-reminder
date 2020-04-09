﻿using AudioReminderCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService
{
    public static class MockData
    {

        #region Mock data
        public static readonly ReminderEntity[] MockReminders = new ReminderEntity[]
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

        public static readonly ServiceSettingsDto DefaultServiceSettings = new ServiceSettingsDto
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