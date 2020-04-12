using AudioReminderCore.Model;
using System;
using System.Collections.Generic;

namespace AudioReminderService.Scheduler
{
    /// <summary>
    /// TODO descirption
    /// </summary>
    public interface IReminderScheduler
    {
        /// <summary>
        /// Starts the service if/when it is enabled in user settings.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the service until Start method is called again, irregardles of user settings.
        /// </summary>
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

        void UpdateSettings(ServiceSettingsEntity serviceSettingsEntity);
    }
}