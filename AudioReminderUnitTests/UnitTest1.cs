using System;
using AudioReminderCore.Model;
using AudioReminderService.Scheduler.TimerBased.DateTimeArithmetic;
using AudioReminderUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AudioReminderUnitTests
{
    [TestClass]
    public class UnitTests
    {
        /// <summary>
        /// sunday, 1 hour before ringing time, next ringing is on monday
        /// </summary>
        [TestMethod]
        public void Test_WorkdayReminder_OnSunday_NextShouldbeMonday()
        {
            ReminderEntity weekly8am = CreateWeekly8amReminder();
            var calc = new NextReminderOccurenceCalculator();

            
            DateTime now1 = new DateTime(2020, 4, 13, 7, 0, 0);
            DateTime expectedNextOccurence1 = new DateTime(2020, 4, 13, 8, 0, 0);
            
            TestNextReminderOccurenceCalculation(weekly8am, calc, now1, expectedNextOccurence1);

        }

        /// <summary>
        /// monday, 1 hour later than the ringing time, next ringing is on tuesday
        /// </summary>
        [TestMethod]
        public void Test_WorkdayReminder_1hourAfterRing_NextShouldbeTomorrow()
        {
            ReminderEntity weekly8am = CreateWeekly8amReminder();
            var calc = new NextReminderOccurenceCalculator();

            DateTime now2 = new DateTime(2020, 4, 13, 9, 0, 0);
            DateTime expectedNextOccurence2 = new DateTime(2020, 4, 14, 8, 0, 0);
            TestNextReminderOccurenceCalculation(weekly8am, calc, now2, expectedNextOccurence2);
        }

        /// <summary>
        /// monday, 1 hour before ringing time, next ringing should be in an hour
        /// </summary>
        [TestMethod]
        public void Test_WorkdayReminder_1hourBeforeRing_NextShouldbeToday()
        {
            ReminderEntity weekly8am = CreateWeekly8amReminder();
            var calc = new NextReminderOccurenceCalculator();

            DateTime now3 = new DateTime(2020, 4, 13, 7, 0, 0);
            DateTime expectedNextOccurence3 = new DateTime(2020, 4, 13, 8, 0, 0);
            TestNextReminderOccurenceCalculation(weekly8am, calc, now3, expectedNextOccurence3);
        }

        private static ReminderEntity CreateWeekly8amReminder()
        {
            //TODO: put this in a test fixture
            return new ReminderEntity()
            {
                Name = "Some event on workdays",
                ScheduledTime = new DateTime(2020, 4, 10, 8, 0, 0),
                RepeatPeriod = RepeatPeriod.Weekly,
                RepeatWeeklyDays = new bool[] { true, true, true, true, true, false, false }
            };
        }

        private static void TestNextReminderOccurenceCalculation(ReminderEntity weekly8am, NextReminderOccurenceCalculator calc, DateTime now, DateTime expectedNextOccurence)
        {
            var actualnextOccurence = calc.GetNextReminderOccurence(weekly8am, now).Value;
            Assert.AreEqual(expectedNextOccurence, actualnextOccurence);
        }

        [TestMethod]
        public void TestAnyChecked_WhenAllAreFalse()
        {
            //arange
            bool[] repeatWeeklyDays = { false, false, false, false, false, false, false };
            bool expectedResult = false;

            //act
            bool actualResult = CreateAndUpdateReminderForm.AnyChecked(repeatWeeklyDays);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestAnyChecked_WhenOneIsTrue()
        {
            //arange
            bool[] repeatWeeklyDays = { true, false, false, false, false, false, false };
            bool expectedResult = true;

            //act
            bool actualResult = CreateAndUpdateReminderForm.AnyChecked(repeatWeeklyDays);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestMethod]
        public void TestAnyChecked_WhenOneIsTrueOnAnotherPosition()
        {
            //arange
            bool[] repeatWeeklyDays = { false, false, false, false, false, true, false };
            bool expectedResult = true;

            //act
            bool actualResult = CreateAndUpdateReminderForm.AnyChecked(repeatWeeklyDays);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
