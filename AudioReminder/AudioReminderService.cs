using GlobalHotKey;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace AudioReminder
{
    public partial class AudioReminderService : ServiceBase
    {
        HotKeyListener hotKeyListener;

        public AudioReminderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log.Logger.Information("Service starting");
            
            //hotKeyListener = new HotKeyListener();

            Log.Logger.Information("Service started");
        }

       
        protected override void OnStop()
        {
            Log.Logger.Information("Service stopping");



            Log.Logger.Information("Service stopped");
        }

       
    }
}
