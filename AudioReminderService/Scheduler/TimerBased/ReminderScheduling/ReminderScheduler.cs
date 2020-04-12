using AudioReminderCore.Model;
using AudioReminderService.Persistence;
using AudioReminderService.Scheduler.TimerBased.DateTimeArithmetic;
using Quartz;
using Quartz.Impl;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AudioReminderService.Scheduler.TimerBased.ReminderScheduling
{
    //TODO: optimize later so that one dismiss does not cause recreating of all timers
    //TODO: implement whole this class
    //TODO: draw detailed state diagram for all this...
    //TODO: check again if same timestamp is everwhere passed and used in complete algorightm to prevent contradicting situations that some condition is true and few lines later the same condition is false
    //TODO: review loging in this and the 2 subclasses
    //TODO: subscribing to and handlign system clock changes, especially when we go back in time
    //TODO: add more unit tests, and plotting of methods as graph f(x) = y to find edge cases
    //TODO: check if quartz or other dependency have time calculation library
    //TODO: After e.g. 1 year of not using service shoud we show that all recuring reminders are missed?
    //TODO: Consider option of using sched + new TimeStamp, see datetime aritchmetic rules

    class ReminderScheduler
    {
        public event Action<string> RingingNeeded;
        protected NextReminderNotifier NextReminderNotifier { get; set; }
        protected UserInteractionManager UserInteractionManager { get; set; }
        protected bool IsEnabled { get; set; }


        public ReminderScheduler()
        {
            IsEnabled = false;

            NextReminderNotifier = new NextReminderNotifier();
            UserInteractionManager = new UserInteractionManager();

            NextReminderNotifier.ReminderElapsed += UserInteractionManager.OnReminderElapsed;
            UserInteractionManager.RingingNeeded += OnRingingNeeded;

            NextReminderNotifier.IsEnabled = true;
        }

        protected void OnRingingNeeded(string reminderName)
        {
            Log.Logger.Information("ReminderScheduler triggering a ring");

            RingingNeeded?.Invoke(reminderName);

            Log.Logger.Information("ReminderScheduler triggering a ring done");
        }

        public void Start()
        {
            Log.Logger.Information("Starting ReminderScheduler");

            //good to be constant in a variable during this analysis in method so that it doesn't change during analysis. It could make some kind of timer deadlock where timer would never ring.
            //TODO: should we change IsEnabled to Start/Stop methods so that we can pass this
            DateTime now = DateTime.UtcNow;

            if (IsEnabled)
            {
                Log.Logger.Warning("ReminderScheduler is already running.");
                return;
            }

            IsEnabled = true;

            NextReminderNotifier.IsEnabled = true;
            //UserInteractionManager.IsEnabled = true; //Enabled this component when it is ready

            Log.Logger.Information("Starting ReminderScheduler done");
        }

        public void Stop()
        {
            Log.Logger.Information("Stopping ReminderScheduler");

            if (!IsEnabled)
            {
                Log.Logger.Warning("ReminderScheduler is already stopped.");
                return;
            }

            IsEnabled = false;

            NextReminderNotifier.IsEnabled = false;
            UserInteractionManager.IsEnabled = false;


            Log.Logger.Information("Stopping ReminderScheduler done");
        }

        public void UpdateReminderList(IList<ReminderEntity> upToDateReminders)
        {
            //TODO: maybe choose here only appropriate data for both

            NextReminderNotifier.UpdateReminderList(upToDateReminders);
            UserInteractionManager.UpdateReminderList(upToDateReminders);
        }

        public void DismissReminder(ReminderEntity reminderEntity)
        {
            UserInteractionManager.DismissReminder(reminderEntity);
        }

        public void SnoozeReminder(ReminderEntity reminderEntity)
        {
            UserInteractionManager.SnoozeReminder(reminderEntity);
        }

        public virtual void ConfigureSnooze(bool snoozeEnabled, int snoozeIntervalMinutes)
        {
            UserInteractionManager.ConfigureSnooze(snoozeEnabled, snoozeIntervalMinutes);
        }
    }
}
