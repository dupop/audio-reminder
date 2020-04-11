using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AudioReminderService.ReminderScheduler.TimerBased
{
    class BeeperScheduler
    {
        public event Action BeeperTimeUp;

        protected Timer nextBeeperTimer { get; set; }

        bool IsRunning { get; set; }


        
        public BeeperScheduler()
        {
            nextBeeperTimer = new Timer();
            nextBeeperTimer.AutoReset = false;
            nextBeeperTimer.Elapsed += NextBeeperTimer_Elapsed;

            IsRunning = false;
        }

        private void NextBeeperTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnBeeperTimeUp();
        }


        #region Timer Callbacks
        protected void OnBeeperTimeUp()
        {
            Log.Logger.Information("BeeperScheduler triggering a beep");

            BeeperTimeUp?.Invoke();

            Log.Logger.Information("BeeperScheduler triggering a beep done");
        }
        #endregion


        #region Interface for controling BeeperScheduler
        /// <summary>
        /// Initial list of reminders must be given to this object before it is started.
        /// </summary>
        public void Start()
        {
            Log.Logger.Information("Starting BeeperScheduler");

            if (IsRunning)
            {
                Log.Logger.Warning("BeeperScheduler is already running.");
                return;
            }

            IsRunning = true;
            nextBeeperTimer.Start();

            Log.Logger.Information("Starting BeeperScheduler done");
        }

        public void Stop()
        {
            Log.Logger.Information("Stopping BeeperScheduler");

            if (!IsRunning)
            {
                Log.Logger.Warning("BeeperScheduler is already stopped.");
                return;
            }

            IsRunning = false;
            nextBeeperTimer.Stop();

            Log.Logger.Information("Stopping BeeperScheduler done");
        }
        #endregion

    }
}
