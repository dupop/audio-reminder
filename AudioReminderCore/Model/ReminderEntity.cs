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
        /// Must always be defined for a reminder.
        /// </summary>
        [DataMember]
        public DateTime ScheduledTime { get; set; }

        //TODO: prevent on UI repeatable by multiple periods
        [DataMember]
        public bool RepeatWeekly { get; set; }

        /// <summary>
        /// Monday based flag list which should on which days of week should reminder ring.
        /// At least one day is selected if RepeatWeekly flag is set.
        /// No day is selected if the flag is not set.
        /// </summary>
        [DataMember]
        public bool[] RepeatWeeklyDays { get; set; }

        [DataMember]
        public bool RepeatMonthly { get; set; }

        [DataMember]
        public bool RepeatYearly { get; set; }

        [DataMember]
        public DateTime? LastDismissedOccurence { get; set; }

        //[DataMember]
        //public DateTime? LastRang { get; set; }

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

        /// <summary>
        /// Returns true if there is nothing more to be done regarding this reminder.
        /// </summary>
        /// <returns></returns>
        public bool IsDone()
        {
            //repetable reminder is never done, it will always ring again
            if (IsRepeatable())
            {
                return false;
            }

            //single-firing event is done if its only occurence is dismissed
            bool dismissed = ScheduledTime == LastDismissedOccurence;

            return dismissed;
        }

        public override string ToString()
        {
            return Name;
        }

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
