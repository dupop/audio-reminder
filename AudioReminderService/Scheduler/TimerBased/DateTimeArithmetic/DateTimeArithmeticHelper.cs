using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.Scheduler.TimerBased.DateTimeArithmetic
{
    public static class DateTimeArithmeticHelper
    {

        /// <summary>
        /// Fixes month number with possible overflow or underflow by module arthmetic back to the range [1,12].
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int MonthModulo(int month)
        {
            //change number to zero based range [0,11] to enable module arithmetic
            int monthZeroBased = month - 1;

            int monthByModuloZeroBased = monthZeroBased % 12;

            //return number to range [1,12]
            int monthByModuloHumanBased = monthZeroBased + 1;

            return monthByModuloHumanBased;
        }

        public static int GetDayOfWeekMondayBased(DateTime dateTime)
        {
            int dayOfWeekSundayBased = (int)dateTime.DayOfWeek; //DayOfWeek is already zero based (unlike e.g. Month number)
            int dayOfWeekMondayBased = (dayOfWeekSundayBased + 6) % 7;

            return dayOfWeekMondayBased;
        }

    }
}
