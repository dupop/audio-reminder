using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AudioReminderService.ReminderScheduler.TimerBased
{
    /// <summary>
    /// Wrapper around timer that fires BeeperTimeUp events.
    /// Provides two controlling interfaces (ServiceEnabled and BeeperEnabledInSettings) and handles their combinations.
    /// Provides means to change Interval. Timer starting will be aborted if interval is not set.
    /// </summary>
    class BeeperScheduler
    {
        public event Action BeeperTimeUp;
        protected Timer beeperTimer { get; set; }

        /// <summary>
        /// Protection against running timer with undefined interval.
        /// </summary>
        protected bool intervalValueSet;

        protected bool shedulerEnabled;
        protected bool beeperEnabledInSettings;

        /// <summary>
        /// Is the TimerScheduler running at all. This is one of the preconditions for running the beeper timer.
        /// </summary>
        public bool SchedulerEnabled
        {
            get 
            { 
                return shedulerEnabled;
            }
            set
            {
                bool wasEnabledBefore = IsRunning;

                shedulerEnabled = value;

                HandleStatusChange(wasEnabledBefore);
            }
        }

        /// <summary>
        /// Is beeper enabled in settings. This is one of the preconditions for running the beeper timer.
        /// </summary>
        public bool BeeperEnabledInSettings
        {
            get 
            {
                return beeperEnabledInSettings; 
            }
            set 
            {
                bool wasEnabledBefore = IsRunning;

                beeperEnabledInSettings = value;

                HandleStatusChange(wasEnabledBefore);
            }
        }

        /// <summary>
        /// Is beeper timer currently running.
        /// </summary>
        public bool IsRunning => shedulerEnabled && beeperEnabledInSettings;
        
        /// <summary>
        /// Beeper interval in minutes.
        /// No default value, needs to be set before starting timer.
        /// Error logged and new value ignored if value is less than 1 minute
        /// </summary>
        public int Interval
        {
            get
            {
                double timerIntervalMs = beeperTimer.Interval;
                int intervalInMinutes = CovertMilisecondsToMinutes(timerIntervalMs);

                return intervalInMinutes;
            }
            set
            {
                int intervalInMinutes = value;
                const int minimalIntervalLengthMinutes = 1;

                if(intervalInMinutes < minimalIntervalLengthMinutes)
                {
                    Log.Logger.Error($"Attempted to set interval for BeeperScheduler of {intervalInMinutes} min which is less than 1 min. Interval change will be ignored in BeeperScheduler.");
                    return;
                }

                double intervalMs = CovertMinutesToMiliseconds(intervalInMinutes);

                beeperTimer.Interval = intervalMs;
                intervalValueSet = true;
                Log.Logger.Information($"BeeperScheduler interval updated to {intervalInMinutes} min (i.e. {intervalMs:0.##}ms)");
            }
        }


        /// <summary>
        /// Creates BeeperScheduler in stopped state without defined Interval.
        /// </summary>
        public BeeperScheduler()
        {
            beeperTimer = new Timer();
            beeperTimer.Enabled = false;
            beeperTimer.AutoReset = true; //TODO: check this again when handling conccurency
            beeperTimer.Elapsed += BeeperTimer_Elapsed;
            
            shedulerEnabled = false;
            beeperEnabledInSettings = false;
            intervalValueSet = false;
        }

        /// <summary>
        /// Starts or stops timer if value of Enabled cahnged.
        /// </summary>
        protected virtual void HandleStatusChange(bool wasEnabledBefore)
        {
            bool startedNow = !wasEnabledBefore && IsRunning;
            bool stoppedNow = wasEnabledBefore && !IsRunning;

            if (startedNow)
            {
                if(!intervalValueSet)
                {
                    Log.Logger.Error($"Attempted to run BeeperScheduler without defined interval. Timer will not be started until interval is set and scheduler is stopped and started again.");
                    return;
                }

                beeperTimer.Start();
                Log.Logger.Information($"Started BeeperScheduler");
            }
            else if (stoppedNow)
            {
                beeperTimer.Stop();
                Log.Logger.Information($"Stopped BeeperScheduler");
            }
            else
            {
                Log.Logger.Information($"BeeperScheduler already {(IsRunning ? "started" : "stopped")} [serviceEnabled = {shedulerEnabled}, beeperEnabledInSettings = {beeperEnabledInSettings}, interval = {Interval}]");
            }
        }

        protected virtual int CovertMilisecondsToMinutes(double intervalInMs)
        {
            double intervalInMinutes = intervalInMs / (60 * 1000);
            int intervalInMinutesAsInt = (int)Math.Round(beeperTimer.Interval);

            return intervalInMinutesAsInt;
        }

        protected virtual double CovertMinutesToMiliseconds(int intervalInMinutes)
        {
            int intervalMs = intervalInMinutes * 60 * 1000;

            return intervalMs;
        }

        private void BeeperTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnBeeperTimeUp();
        }

        protected void OnBeeperTimeUp()
        {
            Log.Logger.Information($"BeeperScheduler triggering a beep");

            BeeperTimeUp?.Invoke();

            Log.Logger.Information($"BeeperScheduler triggering a beep done");
        }

    }
}
