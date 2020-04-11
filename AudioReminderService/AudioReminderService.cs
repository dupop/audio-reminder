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
using AudioReminderService.ReminderScheduler.TimerBased;

namespace AudioReminderService
{
    public partial class AudioReminderService : ServiceBase
    {
        AudioReminderWebserviceHost webServiceHost;
        public static IReminderScheduler ReminderScheduler; //TODO: make non-static if possible, persistence also

        public AudioReminderService()
        {
            InitializeComponent();
            webServiceHost = new AudioReminderWebserviceHost();
            ReminderScheduler = new TimerScheduler();
        }

        protected override void OnStart(string[] args)
        {
            Log.Logger.Information("Service starting");

            FilePersistenceAdapters.Start();

            Log.Logger.Information("Scheduler will start listening to changes of reminder entities");
            FilePersistenceAdapters.RemiderFilePersistence.EntitiesChanged += () => ReminderScheduler.UpdateReminderList(FilePersistenceAdapters.RemiderFilePersistence.Entities);
            FilePersistenceAdapters.SettingsFilePersistence.EntitiesChanged += () => ReminderScheduler.UpdateSettings(FilePersistenceAdapters.SettingsFilePersistence.Entities[0]);

            Log.Logger.Information("Updating list of reminders in scheduler");
            ReminderScheduler.UpdateSettings(FilePersistenceAdapters.SettingsFilePersistence.Entities[0]);
            ReminderScheduler.UpdateReminderList(FilePersistenceAdapters.RemiderFilePersistence.Entities);
            ReminderScheduler.ReminderTimeUp += RingingCaller.RingReminder;
            ReminderScheduler.BeeperTimeUp += RingingCaller.RingBeep;

            webServiceHost.Start();

            ReminderScheduler.Start();
            
            //TODO: really use settings given from UI

            Log.Logger.Information("Service starting done");
        }

        protected override void OnStop()
        {
            Log.Logger.Information("Service stopping");

            ReminderScheduler.Stop();

            webServiceHost.Stop();

            FilePersistenceAdapters.Stop();

            Log.Logger.Information("Service stopping done");
        }

        
    }
}
