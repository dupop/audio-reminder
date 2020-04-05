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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReminerRinging
{
    //TODO: fix project name typo: Remin*er + in paths referncing that exe + add 'Audio' prefix so that they are sorted togeter in taskmgr an other tools
    public partial class ReminderRingingForm : Form
    {
        protected virtual AudioReminderWebServiceClient Proxy { get; set; }
        protected virtual ReminderEntity Reminder { get; set; }


        #region Constructor and events
        public ReminderRingingForm()
        {
            InitializeComponent();
        }

        private void ReminderRingForm_Load(object sender, EventArgs e)
        {
            Log.Logger.Information($"ReminderRinger form loading");

            //TODO: Should this be async?
            bool success = InitializeState();
            if(!success)
            {
                Close();
                return;
            }
            
            RingAsync();
        }

        private void snoozeButton_Click(object sender, EventArgs e)
        {
            SnoozeRdminder();
        }

        private void dismissButton_Click(object sender, EventArgs e)
        {
            DismissReminder();
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

            SetProxy();
            LoadReminderFromProxy(reminderName);
            if (Reminder == null)
            {
                Log.Logger.Fatal($"Reminder with given name could not be found. Closing application. ");
                return false;
            }

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
            Reminder = Proxy.Load(reminderName);//TODO: change to ID e.g. guid because spaces and unicode may become problem here
        }

        
        #endregion


        #region
        protected virtual void RingAsync()
        {
            Log.Logger.Information($"Making noise");

            //TODO: ringing
            Console.Beep();
            //sound.

            Log.Logger.Information($"Making noise done");
        }

        protected virtual void SnoozeRdminder()
        {
            Log.Logger.Information($"Snoozing reminder [reminder name = {Reminder.Name}]");

            Proxy.SnoozeReminder(Reminder.Name);

            //TODO: why is not form automatically closed because we have dialog reuslt in both button properties? maybe because its not called with RunDialog?
            Close();

            Log.Logger.Information($"Snoozing reminder [reminder name = {Reminder.Name}] done");
        }

        protected virtual void DismissReminder()
        {
            Log.Logger.Information($"Dismissing reminder [reminder name = {Reminder.Name}]");

            Proxy.DismissReminder(Reminder.Name);

            //TODO: why is not form automatically closed because we have dialog reuslt in both button properties? maybe because its not called with RunDialog?
            Close();

            Log.Logger.Information($"Dismissing reminder [reminder name = {Reminder.Name}] done");
        }
        #endregion

    }
}
