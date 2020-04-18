using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.Scheduler.TimerBased.ReminderScheduling
{
    //TODO: add summary for each state
    public enum ReminderSchedulerState
    {
        NoElapsedReminders,
        WaitingUserResponse,
        SnoozeTime
    }
}
