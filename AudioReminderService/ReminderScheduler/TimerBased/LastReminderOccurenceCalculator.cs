using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AudioReminderService.ReminderScheduler.TimerBased
{

    public class LastReminderOccurenceCalculator
    {
        /// <summary>
        /// Finds the nearest moment in the past where the reminder would ring accoring to its schedule.
        /// </summary>
        public virtual DateTime? GetLastReminderOccurence(ReminderEntity reminder, DateTime now)
        {
            if (!reminder.IsRepeatable())
            {
                return GetLastOccurenceOfNonRepeatingReminder(reminder, now);
            }
            else
            {
                return GetLastOccurenceOfRepeatingReminder(reminder, now);
            }
        }


        protected virtual DateTime? GetLastOccurenceOfNonRepeatingReminder(ReminderEntity reminder, DateTime now)
        {
            bool isReminderInTheFuture = reminder.ScheduledTime > now;
            DateTime? lastReminderOccurence;

            if (isReminderInTheFuture)
            {
                //if non-repetable reminder is set in the future, there was no point in the past where it was ringing
                lastReminderOccurence = null;
                Log.Logger.Information($"Nearst occurence in the past of non-repeatable reminder [name = {reminder.Name}] scheduled in the future is [scheduled = {reminder.ScheduledTime}, nearest occurence = {lastReminderOccurence}, now = {now}].");
            }
            else
            {
                //if non-repetable reminder is set in the past, there was just one poitn in the past where it was ringing
                lastReminderOccurence = reminder.ScheduledTime;
                Log.Logger.Information($"Nearst occurence in the past of non-repeatable reminder [name = {reminder.Name}] scheduled in the past is [scheduled = {reminder.ScheduledTime}, nearest occurence = {lastReminderOccurence}, now = {now}].");
            }

            return lastReminderOccurence;
        }

        protected virtual DateTime GetLastOccurenceOfRepeatingReminder(ReminderEntity reminder, DateTime now)
        {
            //Finding last occurence in the past of repetable reminder does not depened on the next scheduled occurence and if it is in the past or future
            //If scheduled time would be EXACTLY NOW we treat that as past also, and return that moment

            switch (reminder.RepeatPeriod)
            {
                case RepeatPeriod.Yearly:
                    return GetLastOccurenceOfYearlyRepeatingReminder(reminder.ScheduledTime, now);
                case RepeatPeriod.Monthly:
                    return GetLastOccurenceOfMonthlyRepeatingReminder(reminder.ScheduledTime, now);
                default: //weekly recuring reminder
                    return GetLastOccurenceOfWeeklyRepeatingReminder(reminder.ScheduledTime, now, reminder.RepeatWeeklyDays);
            }
        }

        /// <summary>
        /// For a yearly repeatable reminder, finds the moment of its last occurence in the past
        /// </summary>
        protected virtual DateTime GetLastOccurenceOfYearlyRepeatingReminder(DateTime scheduledTimeInThePast, DateTime now)
        {
            TimeSpan intervalFromYearStartUntilNow = new TimeSpan(now.DayOfYear, 0, 0, 0) + now.TimeOfDay;
            TimeSpan intervalFromYearStartUntilRingingTime = new TimeSpan(scheduledTimeInThePast.DayOfYear, 0, 0, 0) + scheduledTimeInThePast.TimeOfDay;

            bool isLastOccurenceInThisYear = intervalFromYearStartUntilRingingTime <= intervalFromYearStartUntilNow;
            int yearOfLastOccurence = isLastOccurenceInThisYear ? now.Year : now.Year - 1; //TODO: maybe add resonable min and max time limits to UI to prevent overflows?

            DateTime lastYearlyOccurenceDate = new DateTime(yearOfLastOccurence, scheduledTimeInThePast.Month, scheduledTimeInThePast.Day);
            DateTime lastYearlyOccurenceDateTime = lastYearlyOccurenceDate + scheduledTimeInThePast.TimeOfDay;

            return lastYearlyOccurenceDateTime;
        }

        /// <summary>
        /// For a monthly repeatable reminder, finds the moment of its last occurence in the past.
        /// </summary>
        protected virtual DateTime GetLastOccurenceOfMonthlyRepeatingReminder(DateTime scheduledTimeInThePast, DateTime now)
        {
            TimeSpan intervalFromMonthStartUntilNow = new TimeSpan(now.Day, 0, 0, 0) + now.TimeOfDay;
            TimeSpan intervalFromMonthStartUntilRingingTime = new TimeSpan(scheduledTimeInThePast.Day, 0, 0, 0) + scheduledTimeInThePast.TimeOfDay;

            bool isLastOccurenceInThisMonth = intervalFromMonthStartUntilRingingTime <= intervalFromMonthStartUntilNow;
            bool isNextOccurenceInPreviousYear = !isLastOccurenceInThisMonth && now.Month == 1;

            int yearOfLastOccurence = isNextOccurenceInPreviousYear ? now.Year - 1 : now.Year;
            int monthOfLastOccurence = isLastOccurenceInThisMonth ? now.Month : DateTimeArithmeticHelper.MonthModulo(now.Month - 1);

            DateTime lastMonthlyOccurenceDate = new DateTime(yearOfLastOccurence, monthOfLastOccurence, scheduledTimeInThePast.Day);
            
            DateTime lastMonthlyOccurenceDateTime = lastMonthlyOccurenceDate + scheduledTimeInThePast.TimeOfDay;

            return lastMonthlyOccurenceDateTime;
        }

        /// <summary>
        /// For a weekly repeatable reminder, finds the moment of its last occurence in the past.
        /// </summary>
        protected virtual DateTime GetLastOccurenceOfWeeklyRepeatingReminder(DateTime scheduledTimeInThePast, DateTime now, bool[] repeatWeeklyDays)
        {
            TimeSpan oneDayTimeSpan = new TimeSpan(1, 0, 0, 0);
            DateTime dateToday = now.Date;

            DateTime lastRingDate = dateToday;

            //check if TimeOfDay for ringing already passed today 
            bool reminderAlreadyRangToday = scheduledTimeInThePast.TimeOfDay <= now.TimeOfDay;

            //If remidner wasn't ringing today, start checking from yesterday. In worst case it was ringing this same day of week, but 7 days ago.
            if (!reminderAlreadyRangToday)
            {
                lastRingDate -= oneDayTimeSpan;
            }

            for (int i = 0; i < 7; i++)
            {
                int lastRingDayOfWeek = DateTimeArithmeticHelper.GetDayOfWeekMondayBased(lastRingDate);

                bool wasOkToRingOnThatDay = repeatWeeklyDays[lastRingDayOfWeek] == true;

                if (wasOkToRingOnThatDay)
                {
                    break;
                }

                lastRingDate -= oneDayTimeSpan;
            }

            DateTime lastWeeklyOccurenceDateTime = lastRingDate + scheduledTimeInThePast.TimeOfDay;

            return lastWeeklyOccurenceDateTime;
        }

    }
}
