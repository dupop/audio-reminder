using AudioReminderCore;
using AudioReminderCore.Interfaces;
using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.WebService
{
    class AudioReminderWebserviceHost
    {
        ServiceHost serviceHost;

        public AudioReminderWebserviceHost()
        {
            string uriString = WebserviceAdressHelper.GetAudioReminderUri();
            Uri uri = new Uri(uriString);

            serviceHost = new ServiceHost(typeof(AudioReminderWebservice), uri);
        }

        public void Start()
        {
            Log.Logger.Information("Starting webservice");

            ServiceMetadataBehavior behaveior = CreateServiceBehveior();
            serviceHost.Description.Behaviors.Add(behaveior);

            serviceHost.Open();

            Log.Logger.Information("Starting webservice done");
        }

        public void Stop()
        {
            Log.Logger.Information("Stopping webservice");

            serviceHost.Close(new TimeSpan(0, 0, 10));

            Log.Logger.Information("Stopping webservice done");
        }

        protected virtual ServiceMetadataBehavior CreateServiceBehveior()
        {
            ServiceMetadataBehavior behaveior = new ServiceMetadataBehavior();

            // Enable metadata publishing
            behaveior.HttpGetEnabled = true; //TODO: remobe this

            //behaveior.ExternalMetadataLocation //todo:explore this

            // ?
            behaveior.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

            return behaveior;
        }

    }
}

