﻿using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.ReminderScheduler.Utils
{
    class ReminderDismissableValidator
    {

        /// <summary>
        /// Validates that this reminder should indeed be ringing now.
        /// Protection of some kind of double dismis request (maybe by multiple ringing windows etc..) that would cause next occurance of reminder to be skipped
        /// </summary>
        public virtual bool ValidateReminderShouldBeRinging(ReminderEntity reminderEntity)
        {
            if (!ValidateReminderIsInThePast(reminderEntity))
            {
                return false;
            }

            if (!ValidateReminderNotAlreadyDismissed(reminderEntity))
            {
                return false;
            }

            return true;
        }

        protected virtual bool ValidateReminderNotAlreadyDismissed(ReminderEntity reminderEntity)
        {
            if (reminderEntity.ScheduledTime == reminderEntity.LastDismissedOccurence)
            {
                Log.Logger.Error($"Reminder to be dismissed [reminderName = {reminderEntity.Name}] is already dissmissed [LastDismissedOccurence = {reminderEntity.LastDismissedOccurence}, scheduled time = {reminderEntity.ScheduledTime}]. Ignorring dismiss request.");
                return false;
            }

            return true;
        }

        protected virtual bool ValidateReminderIsInThePast(ReminderEntity reminderEntity)
        {
            DateTime now = DateTime.UtcNow;
            bool reminderNotYetReady = reminderEntity.ScheduledTime > now;

            if (reminderNotYetReady)
            {
                Log.Logger.Error($"Reminder to be dismissed [reminderName = {reminderEntity.Name}] is scheduled in the future [UtcNow = {now}, scheduled time = {reminderEntity.ScheduledTime}]. Ignorring dismiss request.");
                return false;
            }

            return true;
        }
    }
}