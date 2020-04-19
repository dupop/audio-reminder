using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.Scheduler.TimerBased.ReminderScheduling
{
    /// <summary>
    /// Describes if the scheduler or the user should make the next move.
    /// </summary>
    public enum UserInteractionState
    {
        /// <summary>
        /// User interaction manager is currently not enabled.
        /// All interaction towards the user is postponed until it is enabled again.
        /// </summary>
        Disabled,

        /// <summary>
        /// User interaction manager is enabled and there are no reminders of which the user should be notified.
        /// Waiting for next elapsed reminder.
        /// </summary>
        NoElapsedReminders,

        /// <summary>
        /// User interaction manager is enabled and Ringer already rang, but user didn't yet respond to the ringing.
        /// Waiting for user to respond. //TODO: will should also be tracking maximum no-reaction time to elapse and do something
        /// </summary>
        WaitingUserResponse,

        /// <summary>
        /// User interaction manager is enabled and user snoozed the last reminder.
        /// Waiting snooze interval to elapse.
        /// </summary>
        SnoozeTime
    }
}
