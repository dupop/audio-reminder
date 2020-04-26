using AudioReminderCore;
using AudioReminderCore.ClientProxies;
using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioReminderRinging
{
    public partial class ReminderRingingForm : Form
    {
        //TODO: add more logs to this class

        protected virtual AudioReminderWebServiceClient Proxy { get; set; }
        protected virtual ReminderEntity Reminder { get; set; }

        /// <summary>
        /// Is ringing run just as an example to user.
        /// </summary>
        protected virtual bool IsTestMode { get; set; }

        Thread SoundPlayingThread { get; set; }
        System.Media.SoundPlayer SoundPlayer { get; set; }

        #region Constructor and events
        public ReminderRingingForm()
        {
            InitializeComponent();

            Icon = AudioReminderCore.Properties.Resources.AudioReminderIcon;
        }

        //TODO: check if sound will cover the name of the reimnder, so it will not be possible to hear it. Which should play first, should thay play together?
        /// <summary>
        /// When user shows any reaction, stop anoying him with the sound.
        /// </summary>
        protected virtual void StopSoundAfterUserReaction()
        {
            //TODO: implement properly & test - this code freezes the form
            //consider making this check less sensitive to prevent user from accidently stopping the sound and missing the reminder

            //dismissButton.GotFocus += StopSound;
            //snoozeButton.GotFocus += StopSound;
            //this.GotFocus += StopSound;
        }

        private void StopSound(object sender, EventArgs e)
        {
            SoundPlayer?.Stop();
        }

        private void ReminderRingForm_Load(object sender, EventArgs e)
        {
            Log.Logger.Information($"ReminderRinger form loading");

            StopSoundAfterUserReaction();

            bool success = InitializeState();
            if (!success)
            {
                Close();
                return;
            }

            RingAsync();
        }

        private void snoozeButton_Click(object sender, EventArgs e)
        {
            if (!IsTestMode)
            {
                SnoozeReminder();
            }
            else
            {
                Log.Logger.Information($"Snooze request not sent because ringing is started in test mode");
            }

            Close();
        }

        private void dismissButton_Click(object sender, EventArgs e)
        {
            if (!IsTestMode)
            {
                DismissReminder();
            }
            else
            {
                Log.Logger.Information($"Dismiss request not sent because ringing is started in test mode");
            }

            Close();
        }
        #endregion


        #region State initialization
        protected virtual bool InitializeState()
        {

            string reminderName = GetReminderName();
            if (string.IsNullOrWhiteSpace(reminderName))
            {
                return false;
            }

            if (reminderName == NamedPipeHelper.TestReminderName)
            {
                IsTestMode = true;
                Log.Logger.Information($"Ringer started just as an ringing example. Snooze/dismiss will have no effect.");
                return true;
            }

            SetProxy();
            LoadReminderFromProxy(reminderName);
            if (Reminder == null)
            {
                Log.Logger.Fatal($"Reminder with given name could not be found. Closing application. ");
                return false;
            }
            this.Text = Reminder.Name + " - reminder ringing";

            return true;
        }

        protected virtual string GetReminderName()
        {
            List<string> args = Environment.GetCommandLineArgs().ToList();

            if (args.Count < 2)
            {
                Log.Logger.Fatal($"Less than 2 arguments. Closing application. ");
                return string.Empty;
            }

            string reminderNameArgument = args[1];
            Log.Logger.Information($"Argument at index 1 is [value = {reminderNameArgument}]");

            return reminderNameArgument;
        }

        protected virtual void SetProxy()
        {
            Proxy = new AudioReminderWebServiceClient();
        }

        protected virtual void LoadReminderFromProxy(string reminderName)
        {
            Log.Logger.Information($"Fetching reminder data [reminder name = {reminderName}]");
            Reminder = Proxy.Load(reminderName);
        }

        #endregion


        #region
        protected virtual void RingAsync()
        {
            Log.Logger.Information($"Making noise");

            SoundPlayingThread = ExecuteInNewThread(PlayRingingSound, false);

            Log.Logger.Information($"Making noise done");
        }

        protected virtual void SnoozeReminder()
        {
            Log.Logger.Information($"Snoozing reminder [reminder name = {Reminder.Name}]");

            Proxy.SnoozeReminder(Reminder.Name);

            Log.Logger.Information($"Snoozing reminder [reminder name = {Reminder.Name}] done");
        }

        protected virtual void DismissReminder()
        {
            Log.Logger.Information($"Dismissing reminder [reminder name = {Reminder.Name}]");

            Proxy.DismissReminder(Reminder.Name);

            Log.Logger.Information($"Dismissing reminder [reminder name = {Reminder.Name}] done");
        }
        #endregion


        #region Playing sound

        /// <summary>
        /// Provides background execution which doesn't block UI.
        /// When keepProgramOpen is used, program is not closed until the task is finished.
        /// </summary>
        private static Thread ExecuteInNewThread(ThreadStart task, bool keepProgramOpen)
        {
            var newThread = new Thread(task);
            newThread.IsBackground = !keepProgramOpen;

            newThread.Start();

            return newThread;
        }

        private void PlayRingingSound()
        {
            SoundPlayer = new System.Media.SoundPlayer();
            SoundPlayer.Stream = Properties.Resources._18637_1464805961;
            SoundPlayer.PlaySync();

            //TODO: When configureable sounds are added validate file existance; Play this default sound if configured sound is not present
        }

        #endregion
        
        
        private void ReminderRingingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO: check if we should catch OnClosing event (excluding when dismiss is pressed or validation failed or we are in test mode). That may be dangerous if there are unhandled exceptions during sending of snooze request
        }
    }
}
