using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.ReminderScheduler.TimerBased
{
    public enum ReminderSchedulerState
    {
        NoActiveReminders,
        WaitingNextReminder,
        WaitingUserResponse
    }
}
