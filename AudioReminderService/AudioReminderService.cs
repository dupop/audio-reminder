using AudioReminderCore;
using AudioReminderCore.Model;
using AudioReminderService.Persistence;
using AudioReminderService.RingerCalling;
using AudioReminderService.ReminderScheduler;
using AudioReminderService.WebService;
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
        IReminderScheduler scheduler;

        public AudioReminderService()
        {
            InitializeComponent();
            webServiceHost = new AudioReminderWebserviceHost();
            scheduler = new TimerReminderScheduler();
        }

        protected override void OnStart(string[] args)
        {
            Log.Logger.Information("Service starting");

            FilePersistenceAdapters.Start();

            Log.Logger.Information("Scheduler will start listening to changes of reminder entities");
            FilePersistenceAdapters.RemiderFilePersistence.EntitiesChanged += () => scheduler.UpdateReminderList(FilePersistenceAdapters.RemiderFilePersistence.Entities);

            Log.Logger.Information("Updating list of reminders in scheduler");
            scheduler.UpdateReminderList(FilePersistenceAdapters.RemiderFilePersistence.Entities);
            scheduler.OnReminderTimeup += RingingCaller.RingReminder;
            scheduler.OnBeeperTimeUp += RingingCaller.RingBeep;

            webServiceHost.Start();

            scheduler.Start();
            
            //TODO: really use settings given from UI

            Log.Logger.Information("Service starting done");
        }

        protected override void OnStop()
        {
            Log.Logger.Information("Service stopping");

            scheduler.Stop();

            webServiceHost.Stop();

            FilePersistenceAdapters.Stop();

            Log.Logger.Information("Service stopping done");
        }

        
    }
}
