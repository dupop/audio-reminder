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
        public static IReminderScheduler scheduler; //TODO: make non-static if possible, persistence also

        public AudioReminderService()
        {
            LoggingHelper.RunWithExceptionLogging(InitializeService);
        }

        protected override void OnStart(string[] args)
        {
            //exceptions thrown during servcie starting (and probably stopping) are not catched by try-catch from Main method
            LoggingHelper.RunWithExceptionLogging(StartService);
        }

        protected override void OnStop()
        {
            LoggingHelper.RunWithExceptionLogging(StopService);
        }


        protected virtual void InitializeService()
        {
            InitializeComponent();
            webServiceHost = new AudioReminderWebserviceHost();
            scheduler = new TimerScheduler();
        }

        protected virtual void StartService()
        {
            Log.Logger.Information("Service starting");

            FilePersistenceAdapters.Start();

            Log.Logger.Information("Scheduler will start listening to changes of reminder entities");
            FilePersistenceAdapters.RemiderFilePersistence.EntitiesChanged += () => scheduler.UpdateReminderList(FilePersistenceAdapters.RemiderFilePersistence.Entities);
            FilePersistenceAdapters.SettingsFilePersistence.EntitiesChanged += () => scheduler.UpdateSettings(FilePersistenceAdapters.SettingsFilePersistence.Entities[0]);

            scheduler.UpdateSettings(FilePersistenceAdapters.SettingsFilePersistence.Entities[0]);
            scheduler.UpdateReminderList(FilePersistenceAdapters.RemiderFilePersistence.Entities);
            scheduler.ReminderTimeUp += RingingCaller.RingReminder;
            scheduler.BeeperTimeUp += RingingCaller.RingBeep;

            webServiceHost.Start();

            scheduler.Start();

            Log.Logger.Information("Service starting done");
        }

        protected virtual void StopService()
        {
            Log.Logger.Information("Service stopping");

            scheduler.Stop();

            webServiceHost.Stop();

            FilePersistenceAdapters.Stop();

            Log.Logger.Information("Service stopping done");
        }

    }
}
