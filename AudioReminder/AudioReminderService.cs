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

namespace AudioReminder
{
    public partial class AudioReminderService : ServiceBase
    {
        AudioReminderWebserviceHost webServiceHost;

        public static FilePersistanceAdapter<ReminderEntity> RemiderFilePersistence { get; set; } //TODO: extract theese singletons somewhere
        public static FilePersistanceAdapter<ServiceSettingsDto> SettingsFilePersistence { get; set; }

        public AudioReminderService()
        {
            InitializeComponent();
            webServiceHost = new AudioReminderWebserviceHost();
        }

        protected override void OnStart(string[] args)
        {
            Log.Logger.Information("Service starting");

            RemiderFilePersistence = new FilePersistanceAdapter<ReminderEntity>(GetDefaultReminderList());
            SettingsFilePersistence = new FilePersistanceAdapter<ServiceSettingsDto>(GetDefaultSettings());

            webServiceHost.Start();

            //TODO: service implementation

            Log.Logger.Information("Service starting done");
        }

        //TODO: to be removed after rtesting?
        private static List<ReminderEntity> GetDefaultReminderList()
        {
            return MockData.MockReminders.ToList();
        }

        //TODO: to be removed after rtesting?
        private static List<ServiceSettingsDto> GetDefaultSettings()
        {
            return new List<ServiceSettingsDto> {MockData.DefaultServiceSettings };
        }

        protected override void OnStop()
        {
            Log.Logger.Information("Service stopping");

            StopService();
            RemiderFilePersistence.SaveRemindersToFile();
            SettingsFilePersistence.SaveRemindersToFile();

            Log.Logger.Information("Service stopping done");
        }

        private void StopService()
        {
            webServiceHost.Stop();
        }

    }
}
