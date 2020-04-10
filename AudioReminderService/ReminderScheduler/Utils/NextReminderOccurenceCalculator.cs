using AudioReminderCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.ReminderScheduler.Utils
{
    class NextReminderOccurenceCalculator
    {

        //TODO: always log results of theses caluclations, only final result of this class is enough
        /// <summary>
        /// Finds the moment of the first occurence of a reminder in the future. 
        /// Null only for non-repeating reminders from the past.
        /// </summary>
        public virtual DateTime? GetNextReminderOccurence(ReminderEntity reminder, DateTime now)
        {
            if (!reminder.IsRepeatable())
            {
                return GetNextOccurenceOfNonRepeatingReminder(reminder, now);
            }
            else
            {
                return GetNextOccurenceOfRepeatingReminder(reminder, now);
            }
        }

        protected virtual DateTime? GetNextOccurenceOfNonRepeatingReminder(ReminderEntity reminder, DateTime now)
        {
            //if scheduled time is in the future, that is the next occurence
            bool isReminderInTheFuture = reminder.ScheduledTime > now;

            if (isReminderInTheFuture)
            {
                return reminder.ScheduledTime;
            }
            else
            {
                return null;
            }
        }

        protected virtual DateTime GetNextOccurenceOfRepeatingReminder(ReminderEntity reminder, DateTime now)
        {
            //if scheduled time is in the future, that is the next occurence
            bool isReminderInTheFuture = reminder.ScheduledTime > now;

            if (isReminderInTheFuture)
            {
                return reminder.ScheduledTime;
            }

            return GetNextOccurenceOfRepeatingReminderFromThePast(reminder, now);
        }

        protected virtual DateTime GetNextOccurenceOfRepeatingReminderFromThePast(ReminderEntity reminder, DateTime now)
        {
            if (reminder.RepeatYearly)
            {
                return GetNextOccurenceOfYearlyRepeatingReminder(reminder.ScheduledTime, now);
            }

            if (reminder.RepeatMonthly)
            {
                return GetNextOccurenceOfMonthlyRepeatingReminder(reminder.ScheduledTime, now);
            }

            //else weekly recuring reminder
            return GetNextOccurenceOfWeeklyRepeatingReminder(reminder.ScheduledTime, now, reminder.RepeatWeeklyDays);
        }


        //TODO: include current date in isNextOccurenceInThisYear; also in other 3 methods, also fix missing time component of dateim in these 3, and its impact on overflows
        /// <summary>
        /// For a yearlt repeatable reminder that is scheduled in the past, finds the moment of its first occurence in the future.
        /// </summary>
        protected virtual DateTime GetNextOccurenceOfYearlyRepeatingReminder(DateTime scheduledTimeInThePast, DateTime now)
        {
            bool isNextOccurenceInThisYear = scheduledTimeInThePast.DayOfYear > now.DayOfYear;
            int yearOfNextOccurence = isNextOccurenceInThisYear ? now.Year : now.Year + 1;

            DateTime nextYearlyOccurence = new DateTime(yearOfNextOccurence, scheduledTimeInThePast.Month, scheduledTimeInThePast.Day);

            return nextYearlyOccurence;
        }

        /// <summary>
        /// For a monthly repeatable reminder that is scheduled in the past finds the moment of its first occurence in the future.
        /// </summary>
        protected virtual DateTime GetNextOccurenceOfMonthlyRepeatingReminder(DateTime scheduledTimeInThePast, DateTime now)
        {
            bool isNextOccurenceInThisMonth = scheduledTimeInThePast.Day > now.Day;
            bool isNextOccurenceInFollowingYear = !isNextOccurenceInThisMonth && now.Month == 12;

            int yearOfNextOccurence = isNextOccurenceInFollowingYear ? now.Year + 1 : now.Year;
            int monthOfNextOccurence = isNextOccurenceInThisMonth ? now.Month : (now.Month + 1) % 12;

            DateTime nextMonthlyOccurence = new DateTime(yearOfNextOccurence, scheduledTimeInThePast.Month, scheduledTimeInThePast.Day);

            return nextMonthlyOccurence;
        }

        /// <summary>
        /// For a weekly repeatable reminder that is scheduled in the past, finds the moment of its first occurence in the future.
        /// </summary>
        protected virtual DateTime GetNextOccurenceOfWeeklyRepeatingReminder(DateTime scheduledTimeInThePast, DateTime now, bool[] repeatWeeklyDays)
        {
            int daysToAdd = AfterHowManyDaysToRepeatWeeklyReminder(now, repeatWeeklyDays);

            int oldScheduledMonthLength = DateTime.DaysInMonth(scheduledTimeInThePast.Year, scheduledTimeInThePast.Month);
            bool isNextOccurenceInSameMonth = scheduledTimeInThePast.Day + daysToAdd > oldScheduledMonthLength;
            bool isNextOccurenceInFollowingYear = !isNextOccurenceInSameMonth && now.Month == 12;

            int dayOfNextOccurence = (scheduledTimeInThePast.Day + daysToAdd) % oldScheduledMonthLength;
            int monthOfNextOccurence = isNextOccurenceInSameMonth ? now.Month : (now.Month + 1) % 12;
            int yearOfNextOccurence = isNextOccurenceInFollowingYear ? now.Year + 1 : now.Year;

            DateTime nextWeeklyOccurence = new DateTime(yearOfNextOccurence, monthOfNextOccurence, dayOfNextOccurence);

            return nextWeeklyOccurence;
        }

        protected virtual int AfterHowManyDaysToRepeatWeeklyReminder(DateTime now, bool[] repeatWeeklyDays)
        {
            int currentDayOfWeekSundayBased = (int)now.DayOfWeek;
            int currentDayOfWeekMondayBased = (currentDayOfWeekSundayBased + 6) % 7;
            int tommorowDayOfWeekMondayBased = (currentDayOfWeekMondayBased + 1) % 7;

            //TODO: the algorithm starting point is wrong, it should start from NOW. scheduled time in past is irelevant!
            //That is, calculation of days to add may be ok, but it should be added to today, so this is absurd...

            #warning//TODO: this alogorithm can't work if scheduledTime is e.g. 10 days ago. Then, the new event would still be in the past; and even when event is just few days from now, it could return the next day which is still in the past
            //handle that. Other methods maybe have same issue. Consider option of using sched + new TimeStamp, see datetime aritchmetic rulesS

            int dayOfWeekMondayBased = tommorowDayOfWeekMondayBased; //finding next day of week to ring, starting from tomorrow, as event already passed
            int i;
            for (i = 0; i < 7; i++)
            {
                if (repeatWeeklyDays[dayOfWeekMondayBased] == true)
                {
                    break;
                }

                dayOfWeekMondayBased++;
            }

            int afterHowManyDaysToRepeatWeeklyReminder = 1 + 1;// +1 is because we are starting to count from tommmorow

            return afterHowManyDaysToRepeatWeeklyReminder;
        }

        protected virtual DateTime GetMomentInTwoMinutes()
        {
            TimeSpan twoMinutes = new TimeSpan(0, 2, 0);
            DateTime mockNextOccurence = DateTime.UtcNow + twoMinutes;
            return mockNextOccurence;
        }

    }
}
