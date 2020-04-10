using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.ReminderScheduler.Utils
{
    public class NextReminderOccurenceCalculator
    {

        //TODO: add more unit tests, and plotting of methods as graph f(x) = y to find edge cases
        //TODO: After e.g. 1 year of not using service shoud we show that all recuring reminders are missed?
        //TODO: Consider option of using sched + new TimeStamp, see datetime aritchmetic rules

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
            bool isReminderInTheFuture = reminder.ScheduledTime > now;
            DateTime? nextReminderOccurence;

            if (isReminderInTheFuture)
            {
                //if scheduled time is in the future, that is the next occurence
                nextReminderOccurence = reminder.ScheduledTime;
                Log.Logger.Information($"Next occurence of non-repeatable reminder [name = {reminder.Name}] scheduled in the future is [old scheduled = {reminder.ScheduledTime}, new scheduled = {nextReminderOccurence}, now = {now}].");
            }
            else
            {
                nextReminderOccurence = null;
                Log.Logger.Information($"Next occurence of non-repeatable reminder [name = {reminder.Name}] scheduled in the past is [old scheduled = {reminder.ScheduledTime}, new scheduled = {nextReminderOccurence}, now = {now}].");
            }

            return nextReminderOccurence;
        }

        
        protected virtual DateTime GetNextOccurenceOfRepeatingReminder(ReminderEntity reminder, DateTime now)
        {
            bool isReminderInTheFuture = reminder.ScheduledTime > now;
            DateTime nextReminderOccurence;

            if (isReminderInTheFuture)
            {
                //if scheduled time is in the future, that is the next occurence
                nextReminderOccurence =  reminder.ScheduledTime;
                Log.Logger.Information($"Next occurence of repeatable reminder [name = {reminder.Name}] scheduled in the future is [old scheduled = {reminder.ScheduledTime}, new scheduled = {nextReminderOccurence}, now = {now}, repeat = {""}, repeatDays = {reminder.GetRepeatWeekDays()}]."); //TODO: fill value when repat is changed to enum
            }
            else
            {
                nextReminderOccurence = GetNextOccurenceOfRepeatingReminderFromThePast(reminder, now);
                Log.Logger.Information($"Next occurence of repeatable reminder [name = {reminder.Name}] scheduled in the past is [old scheduled = {reminder.ScheduledTime}, new scheduled = {nextReminderOccurence}, now = {now}, repeat = {""}, repeatDays = {reminder.GetRepeatWeekDays()}].");
            }

            return nextReminderOccurence;
        }

        //If scheduled time would be EXACTLY NOW we treat that as past also, and find the next occurence
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


        /// <summary>
        /// For a yearly repeatable reminder that is scheduled in the past, finds the moment of its first occurence in the future.
        /// </summary>
        protected virtual DateTime GetNextOccurenceOfYearlyRepeatingReminder(DateTime scheduledTimeInThePast, DateTime now)
        {
            TimeSpan intervalFromYearStartUntilNow = new TimeSpan(now.DayOfYear, 0, 0, 0) + now.TimeOfDay;
            TimeSpan intervalFromYearStartUntilRingingTime = new TimeSpan(scheduledTimeInThePast.DayOfYear, 0, 0, 0) + scheduledTimeInThePast.TimeOfDay;

            bool isNextOccurenceInThisYear = intervalFromYearStartUntilRingingTime > intervalFromYearStartUntilNow;
            int yearOfNextOccurence = isNextOccurenceInThisYear ? now.Year : now.Year + 1;

            DateTime nextYearlyOccurenceDate = new DateTime(yearOfNextOccurence, scheduledTimeInThePast.Month, scheduledTimeInThePast.Day);
            DateTime nextYearlyOccurenceDateTime = nextYearlyOccurenceDate + scheduledTimeInThePast.TimeOfDay;

            return nextYearlyOccurenceDateTime;
        }

        /// <summary>
        /// For a monthly repeatable reminder that is scheduled in the past finds the moment of its first occurence in the future.
        /// </summary>
        protected virtual DateTime GetNextOccurenceOfMonthlyRepeatingReminder(DateTime scheduledTimeInThePast, DateTime now)
        {
            TimeSpan intervalFromMonthStartUntilNow = new TimeSpan(now.Day, 0, 0, 0) + now.TimeOfDay;
            TimeSpan intervalFromMonthStartUntilRingingTime = new TimeSpan(scheduledTimeInThePast.Day, 0, 0, 0) + scheduledTimeInThePast.TimeOfDay;

            bool isNextOccurenceInThisMonth = intervalFromMonthStartUntilRingingTime > intervalFromMonthStartUntilNow;
            bool isNextOccurenceInFollowingYear = !isNextOccurenceInThisMonth && now.Month == 12;

            int yearOfNextOccurence = isNextOccurenceInFollowingYear ? now.Year + 1 : now.Year;
            int monthOfNextOccurence = isNextOccurenceInThisMonth ? now.Month : (now.Month + 1) % 12;

            DateTime nextMonthlyOccurenceDate = new DateTime(yearOfNextOccurence, monthOfNextOccurence, scheduledTimeInThePast.Day);
            
            DateTime nextMonthlyOccurenceDateTime = nextMonthlyOccurenceDate + scheduledTimeInThePast.TimeOfDay;

            return nextMonthlyOccurenceDateTime;
        }

        /// <summary>
        /// For a weekly repeatable reminder that is scheduled in the past, finds the moment of its first occurence in the future.
        /// </summary>
        protected virtual DateTime GetNextOccurenceOfWeeklyRepeatingReminder(DateTime scheduledTimeInThePast, DateTime now, bool[] repeatWeeklyDays)
        {
            TimeSpan oneDayTimeSpan = new TimeSpan(1, 0, 0, 0);
            DateTime dateToday = now.Date;

            //First check if it is ok to run today. 
            //Example: reminder last rang 10 days ago because of service/computer not used. NOW is Monday, 1am, and reminder is set for every working day at 7am. Probably not true, if this is non-dissmised remidner it should maybe ring now (maybe not). If it is dismissed we already scheduled a new time that is not too old.
            DateTime nextRingDate = dateToday;

            //check if TimeOfDay for ringing already passed today 
            bool reminderCantRingTodayAnyMore = now.TimeOfDay >= scheduledTimeInThePast.TimeOfDay;

            //If remidner can't ring today, start checking from tommorow. In worst case it will ring this same day of week, but in 7 days.
            //If it rang today as a late ringing, but scheduledTimeInThePast is long ago, we will then ring once more today if schedueld TimeOfDay is after NOW TimeOfDay
            if (reminderCantRingTodayAnyMore)
            {
                nextRingDate += oneDayTimeSpan;
            }

            for (int i = 0; i < 7; i++)
            {
                int nextRingDayOfWeek = GetDayOfWeekMondayBased(nextRingDate);

                bool isOkToRingOnThatDay = repeatWeeklyDays[nextRingDayOfWeek] == true;

                if (isOkToRingOnThatDay)
                {
                    break;
                }

                nextRingDate += oneDayTimeSpan;
            }

            DateTime nextWeeklyOccurenceDateTime = nextRingDate + scheduledTimeInThePast.TimeOfDay;

            return nextWeeklyOccurenceDateTime;
        }

        protected virtual int GetDayOfWeekMondayBased(DateTime proposedNewTime)
        {
            int dayOfWeekSundayBased = (int)proposedNewTime.DayOfWeek;
            int dayOfWeekMondayBased = (dayOfWeekSundayBased + 1) % 7;
            return dayOfWeekMondayBased;
        }

        protected virtual DateTime GetMomentInTwoMinutes()
        {
            TimeSpan twoMinutes = new TimeSpan(0, 2, 0);
            DateTime mockNextOccurence = DateTime.UtcNow + twoMinutes;
            return mockNextOccurence;
        }

    }
}
