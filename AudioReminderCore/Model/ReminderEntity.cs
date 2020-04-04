using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore.Model
{
    //todo: rename to reminder dto
    [DataContract]
    public class ReminderEntity
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime ScheduledTime { get; set; }

        [DataMember]
        public bool RepeatWeekly { get; set; }

        [DataMember]
        public bool[] RepeatWeeklyDays { get; set; }

        [DataMember]
        public bool RepeatMonthly { get; set; }

        [DataMember]
        public bool RepeatYearly { get; set; }

        //TODO: add custom sound path, which can be choosen from configured list which can be managed

        public override string ToString()
        {
            return Name;
        }
    }
}
