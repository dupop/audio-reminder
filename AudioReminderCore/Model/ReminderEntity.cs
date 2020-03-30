using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore.Model
{
    public class ReminderEntity
    {
        public string Name { get; set; }
        public DateTime ScheduledTime { get; set; }
        public bool RepeatWeekly { get; set; }
        public bool[] RepeatWeeklyDays { get; set; }
        public bool RepeatMonthly { get; set; }
        public bool RepeatYearly { get; set; }

        //TODO: add custom sound path, which can be choosen from configured list which can be managed

        public override string ToString()
        {
            return Name;
        }
    }
}
