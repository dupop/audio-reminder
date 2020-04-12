using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.Scheduler.TimerBased.ReminderScheduling
{
    public enum ReminderSchedulerState
    {
        NoActiveReminders,
        WaitingNextReminder,
        WaitingUserResponse
    }
}
