using AudioReminderCore.Model;
using AudioReminderService.Persistence;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService
{
    //TODO: move this inside scheduler
    public static class ReminderDissmisingHelper
    {

        public static void DismissReminder(ReminderEntity reminderEntity)
        {
            if(!ValidateReminderShouldBeRining(reminderEntity))
            {
                return;
            }

            reminderEntity.LastDismissedOccurence = reminderEntity.ScheduledTime;
            
            if(reminderEntity.IsRepeatable())
            {
                reminderEntity.ScheduledTime = GetNextReminderOccurence(reminderEntity);
            }

            FilePersistenceAdapters.RemiderFilePersistence.TriggerEntitesChangedEvent();
        }

        public static void SnoozeReminder(ReminderEntity reminderEntity)
        {
            //TODO: do we need any implementation regarding this at all, should we maybe keep status DialogShown, and not show again for some time if there is no (at least) snooze response?
            FilePersistenceAdapters.RemiderFilePersistence.TriggerEntitesChangedEvent();
        }

        //TODO: this is just mock method - implement based on reminder repeat settings
        public static DateTime GetNextReminderOccurence(ReminderEntity reminder)
        {
            TimeSpan twoMinutes = new TimeSpan(0, 2, 0);
            DateTime mockNextOccurence = DateTime.UtcNow + twoMinutes;

            return mockNextOccurence;
        }

        /// <summary>
        /// Validates that this reminder should indeed be ringing now.
        /// Protection of some kind of double dismis request (maybe by multiple ringing windows etc..) that would cause next occurance of reminder to be skipped
        /// </summary>
        public static bool ValidateReminderShouldBeRining(ReminderEntity reminderEntity)
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
