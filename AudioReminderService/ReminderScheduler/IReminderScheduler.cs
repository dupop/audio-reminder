using AudioReminderCore.Model;
using System;
using System.Collections.Generic;

namespace AudioReminderService.ReminderScheduler
{
    /// <summary>
    /// Provides 
    /// </summary>
    interface IReminderScheduler
    {
        void Start();
        void Stop();
        void UpdateReminderList(IList<ReminderEntity> upToDateReminders);

        /// <summary>
        /// Occurs each time when a reminder should ring
        /// </summary>
        event Action<string> OnReminderTimeup;

        /// <summary>
        /// Occurs each time when beep sound should be played
        /// </summary>
        event Action OnBeeperTimeUp;
    }
}