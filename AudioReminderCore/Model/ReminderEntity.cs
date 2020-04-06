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
    public class ReminderEntity: ICloneable
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime ScheduledTime { get; set; }

        [DataMember]
        public bool RepeatWeekly { get; set; }

        /// <summary>
        /// At least one day is selected if RepeatWeekly falg is set.
        /// No day is selected if the flag is not set.
        /// </summary>
        [DataMember]
        public bool[] RepeatWeeklyDays { get; set; }

        [DataMember]
        public bool RepeatMonthly { get; set; }

        [DataMember]
        public bool RepeatYearly { get; set; }

        [DataMember]
        public bool Dismissed { get; set; }

        /// <summary>
        /// Deep copy
        /// </summary>
        public object Clone()
        {
            ReminderEntity clone = (ReminderEntity)this.MemberwiseClone();

            if (RepeatWeeklyDays != null)
            {
                clone.RepeatWeeklyDays = (bool[])RepeatWeeklyDays.Clone();
            }

            return clone;
        }

        public bool IsRepeatable()
        {
            return RepeatWeekly || RepeatMonthly || RepeatYearly;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
