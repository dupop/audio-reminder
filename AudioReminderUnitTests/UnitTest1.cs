using System;
using AudioReminderCore.Model;
using AudioReminderService.ReminderScheduler.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AudioReminderUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //TODO: put this in a test fixture
            ReminderEntity weekly8am = new ReminderEntity()
            {
                Name = "Some event on workdays",
                ScheduledTime = new DateTime(2020, 4, 10, 8, 0, 0),
                RepeatPeriod = RepeatPeriod.Weekly,
                RepeatWeeklyDays = new bool[] { true, true, true, true, true, false, false }
            };

            var calc = new AudioReminderService.ReminderScheduler.Utils.NextReminderOccurenceCalculator();

            DateTime now1 = new DateTime(2020, 4, 11, 9, 0, 0);
            DateTime expectedNextOccurence1 = new DateTime(2020, 4, 12, 8, 0, 0);
            Test111(weekly8am, calc, now1, expectedNextOccurence1);

            DateTime now2 = new DateTime(2020, 4, 11, 7, 0, 0);
            DateTime expectedNextOccurence2 = new DateTime(2020, 4, 11, 8, 0, 0);
            Test111(weekly8am, calc, now2, expectedNextOccurence2);
            
        }

        private static void Test111(ReminderEntity weekly8am, NextReminderOccurenceCalculator calc, DateTime now, DateTime expectedNextOccurence)
        {
            var actualnextOccurence = calc.GetNextReminderOccurence(weekly8am, now).Value;
            Assert.AreEqual(expectedNextOccurence, actualnextOccurence);
        }
    }
}
