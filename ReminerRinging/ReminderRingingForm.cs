using AudioReminderCore.ClientProxies;
using AudioReminderCore.Model;
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
                //TODO: fatal error log
                return false;
            }

            return true;
        }
        
        protected virtual string GetReminderName()
        {
            List<string> args = Environment.GetCommandLineArgs().ToList();

            //args.RemoveAt(0); //remove program name from args

            if (args.Count < 2)
            {
                //TODO: fatal error log
                return string.Empty;
            }

            string reminderNameArgument = args[1];

            return reminderNameArgument;
        }
        
        protected virtual void SetProxy()
        {
            Proxy = new AudioReminderWebServiceClient();
        }

        protected virtual void LoadReminderFromProxy(string reminderName)
        {
            Reminder = Proxy.Load(reminderName);//TODO: change to ID e.g. guid because spaces and unicode may become problem here
        }

        
        #endregion


        #region
        protected virtual void RingAsync()
        {
            //TODO: ringing
            Console.Beep();
        }

        protected virtual void SnoozeRdminder()
        {
            Proxy.SnoozeReminder(Reminder.Name);

            //TODO: why is not form automatically closed because we have dialog reuslt in both button properties? maybe because its not called with RunDialog?
            Close();
        }

        protected virtual void DismissReminder()
        {
            Proxy.DismissReminder(Reminder.Name);

            //TODO: why is not form automatically closed because we have dialog reuslt in both button properties? maybe because its not called with RunDialog?
            Close();
        }
        #endregion

    }
}
