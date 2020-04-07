using AudioReminderCore.Model;
using System;
using System.Collections.Generic;

namespace AudioReminderService.ReminderScheduler
{
    /// <summary>
    /// TODO descirption
    /// </summary>
    public interface IReminderScheduler
    {
        void Start();

        void Stop();

        /// <summary>
        /// Should be called before UpdateReminderList when user dismisses a reminder.
        /// </summary>
        void DismissReminder(ReminderEntity reminder);

        /// <summary>
        /// Should be called before UpdateReminderList when user snoozes  a reminder.
        /// </summary>
        void SnoozeReminder(ReminderEntity reminder);
        
        void UpdateReminderList(IList<ReminderEntity> upToDateReminders);

        /// <summary>
        /// Occurs each time when a reminder should ring
        /// </summary>
        event Action<string> ReminderTimeUp;

        /// <summary>
        /// Occurs each time when beep sound should be played
        /// </summary>
        event Action BeeperTimeUp;

    }
}