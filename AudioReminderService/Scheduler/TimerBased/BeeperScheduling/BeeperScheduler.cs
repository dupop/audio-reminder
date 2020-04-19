using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AudioReminderService.Scheduler.TimerBased.BeeperScheduling
{
    //TODO DP->SI: beeper should probably be triggered on full hours not 60min from computer starting moment, with current settings it may fire any time, change this on UI to 5,15,30,60,120,180 & never.

    /// <summary>
    /// Wrapper around timer that fires BeeperTimeUp events.
    /// Provides two controlling interfaces (ServiceEnabled and BeeperEnabledInSettings) and handles their combinations.
    /// Provides means to change Interval. Timer starting will be aborted if interval is not set.
    /// </summary>
    class BeeperScheduler
    {
        public event Action BeeperTimeUp;
        protected Timer beeperTimer;

        /// <summary>
        /// Protection against running timer with undefined interval.
        /// </summary>
        protected bool intervalValueSet;

        /// <summary>
        /// Configured beeper repeating period in minutes.
        /// </summary>
        protected int intervalMinutes;

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
                return intervalMinutes;
            }
            set
            {
                beeperTimer.Stop();//preventing conncurency issues, it will be started again if needed

                int intervalMinutesValue = value;
                const int minimalIntervalLengthMinutes = 1;

                if (intervalMinutesValue < minimalIntervalLengthMinutes)
                {
                    Log.Logger.Error($"Attempted to set interval for BeeperScheduler of {intervalMinutesValue} min which is less than 1 min. Interval change will be ignored in BeeperScheduler.");
                    return;
                }

                intervalMinutes = intervalMinutesValue;
                intervalValueSet = true;
                Log.Logger.Information($"BeeperScheduler interval updated to {intervalMinutesValue} min");

                if (IsRunning)
                {
                    ScheduleNextBeep();
                }
            }
        }


        /// <summary>
        /// Creates BeeperScheduler in stopped state without defined Interval.
        /// </summary>
        public BeeperScheduler()
        {
            Log.Logger.Information($"Creating BeeperScheduler");

            beeperTimer = new Timer();
            beeperTimer.Enabled = false;
            beeperTimer.Elapsed += BeeperTimer_Elapsed;
            beeperTimer.AutoReset = false;

            shedulerEnabled = false;
            beeperEnabledInSettings = false;
            intervalValueSet = false;

            Log.Logger.Information($"Creating BeeperScheduler done");
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
                if (!intervalValueSet)
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

        private void BeeperTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnBeeperTimeUp();
            ScheduleNextBeep();
        }

        protected virtual void ScheduleNextBeep()
        {
            beeperTimer.Interval = CalculateNextTimerInterval();
            beeperTimer.Start();
        }

        /// <summary>
        /// Calculates exact timespan until next beep should be played.
        /// </summary>
        /// <returns>Returns timespan in miliseconds until next beep should be played.</returns>
        /// <remarks>
        /// Always calculating next timer interval based on current time and not just using the same already known interval
        /// so that we compensate for time lost between timer starting (e.g. for synchornous triggering of the beeper).
        /// </remarks>
        protected virtual double CalculateNextTimerInterval()
        {
            DateTime now = DateTime.UtcNow;
            DateTime today = now.Date;

            int minnutesElapsedToday = now.Hour * 24 + now.Minute;

            //intentionally using divison of integers to get number of complete periods
            int beeperPeriodsElapsedToday = minnutesElapsedToday / intervalMinutes;

            //number of minutes from start of today (i.e. midnight) until the moment that next beep should be played
            int minuteOfNextBeepToday = (beeperPeriodsElapsedToday + 1) * intervalMinutes;

            //overflow from today to tommorow is not an issue for this method
            DateTime nextBeep = today + TimeSpan.FromMinutes(minuteOfNextBeepToday);

            TimeSpan timerInterval = nextBeep - now;
            double intervalMs = timerInterval.TotalMilliseconds;

            Log.Logger.Information($"Next beep will be played in {timerInterval.TotalMilliseconds}ms");
            return intervalMs;
        }

        protected virtual void OnBeeperTimeUp()
        {
            Log.Logger.Information($"BeeperScheduler triggering a beep");

            BeeperTimeUp?.Invoke();

            Log.Logger.Information($"BeeperScheduler triggering a beep done");
        }

    }
}
