using AudioReminderCore;
using AudioReminderCore.Model;
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

namespace AudioReminderService
{
    public partial class AudioReminderService : ServiceBase
    {
        AudioReminderWebserviceHost webServiceHost;

        
        public AudioReminderService()
        {
            InitializeComponent();
            webServiceHost = new AudioReminderWebserviceHost();
        }

        protected override void OnStart(string[] args)
        {
            Log.Logger.Information("Service starting");

            FilePersistenceAdapters.Start();
            webServiceHost.Start();

            //TODO: use quartz, update it after each change
            //TODO: really use settings given from UI

            Log.Logger.Information("Service starting done");
        }

        protected override void OnStop()
        {
            Log.Logger.Information("Service stopping");

            webServiceHost.Stop();
            FilePersistenceAdapters.Stop();

            Log.Logger.Information("Service stopping done");
        }

        
    }
}
