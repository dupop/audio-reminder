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

namespace AudioReminder
{
    class AudioReminderWebserviceHost
    {
        const ushort AudioReminderPort = 63116; //Randomly chosen port in range reseverd for private & dynamic use (49152–65535). https://en.wikipedia.org/wiki/List_of_TCP_and_UDP_port_numbers
        const string AudioReminderUriPath = "AudioReminder";

        ServiceHost serviceHost;

        public AudioReminderWebserviceHost()
        {
            string uriString = CreateUriAdress();
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
            
            serviceHost.Close(new TimeSpan(0,0,10));

            Log.Logger.Information("Stopping webservice done");
        }



        protected virtual string CreateUriAdress()
        {
            string uriAdress = $"http://localhost:{AudioReminderPort}/{AudioReminderUriPath}";

            return uriAdress;
        }

        protected virtual ServiceMetadataBehavior CreateServiceBehveior()
        {
            ServiceMetadataBehavior behaveior = new ServiceMetadataBehavior();

            // Enable metadata publishing
            behaveior.HttpGetEnabled = true; //TODO: remobe this
            
            // ?
            behaveior.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            
            return behaveior;
        }

    }
}

