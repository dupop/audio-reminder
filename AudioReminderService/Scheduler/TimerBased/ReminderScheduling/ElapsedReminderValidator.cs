using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.Scheduler.TimerBased.ReminderScheduling
{
    class ElapsedReminderValidator
    {
        /// <summary>
        /// Validates that this reminder should indeed be ringing now (i.e. it is elapsed and not dismissed).
        /// </summary>
        public virtual bool ValidateReminderShouldBeRinging(ReminderEntity reminderEntity, DateTime now)
        {
            if (!ValidateReminderNotNull(reminderEntity))
            {
                return false;
            }

            if (!ValidateReminderIsInThePast(reminderEntity, now))
            {
                return false;
            }

            //Fail validation if reminder is dismissed already
            if (!ValidateReminderNotAlreadyDismissed(reminderEntity))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates that this reminder can be considered as elapsed
        /// </summary>
        public virtual bool ValidateReminderElapsed(ReminderEntity reminderEntity, DateTime now)
        {
            if (!ValidateReminderNotNull(reminderEntity))
            {
                return false;
            }

            if (!ValidateReminderIsInThePast(reminderEntity, now))
            {
                return false;
            }

            return true;
        }



        protected virtual bool ValidateReminderNotNull(ReminderEntity reminderEntity)
        {
            if (reminderEntity == null)
            {
                Log.Logger.Error("Reminder that was expected to be elapsed is null.");
                return false;
            }

            return true;
        }

        protected virtual bool ValidateReminderNotAlreadyDismissed(ReminderEntity reminderEntity)
        {
            if (reminderEntity.Dismissed)
            {
                Log.Logger.Error($"Reminder [reminderName = {reminderEntity.Name}] that was expected to be elapsed is already dissmissed [scheduled time = {reminderEntity.ScheduledTime}].");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates that reminder is in the past (current moment is also considered as the past)
        /// </summary>
        protected virtual bool ValidateReminderIsInThePast(ReminderEntity reminderEntity, DateTime now)
        {
            bool reminderNotYetReady = reminderEntity.ScheduledTime > now;

            if (reminderNotYetReady)
            {
                Log.Logger.Error($"Reminder [reminderName = {reminderEntity.Name}] that was expected to be elapsed is scheduled in the future [UtcNow = {now}, scheduled time = {reminderEntity.ScheduledTime}].");
                return false;
            }

            return true;
        }
    }
}
