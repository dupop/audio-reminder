using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore.Model
{
    [DataContract]
    public class ReminderEntity: ICloneable
    {
        /// <summary>
        /// Must always be defined for a reminder.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// UTC Time and date of next reminder ringing. 
        /// Must always be defined for a reminder.
        /// </summary>
        [DataMember]
        public DateTime ScheduledTime { get; set; }

        [DataMember]
        public RepeatPeriod RepeatPeriod { get; set; }

        /// <summary>
        /// Monday based flag list which should on which days of week should reminder ring.
        /// At least one day is selected if RepeatWeekly flag is set.
        /// No day is selected if the flag is not set.
        /// </summary>
        [DataMember]
        public bool[] RepeatWeeklyDays { get; set; }

        //[DataMember]
        //public DateTime? LastDismissedOccurence { get; set; }

        //[DataMember]
        //public DateTime? LastRang { get; set; }

        /// <summary>
        /// Always false for repeatable reminders, because their time is just rescheduled when user dismisses them.
        /// </summary>
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
            return RepeatPeriod != RepeatPeriod.NoRepeat;
        }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Gets RepeatWeeklyDays as string, eg. "MoTuThWdFr".
        /// </summary>
        /// <returns></returns>
        public string GetRepeatWeekDays()
        {
            string[] dayOfWeekInitials = new string[] { "Mo", "Tu", "Th", "Wd", "Fr", "St", "Sn" };

            string daysOfWeekToRing = "";
            for(int i = 0; i< 7; i++)
            {
                if(RepeatWeeklyDays[i])
                {
                    daysOfWeekToRing += dayOfWeekInitials[i];
                }
            }

            return daysOfWeekToRing;
        }
    }
}
