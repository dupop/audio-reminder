using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore
{
    public static class WebserviceAdressHelper
    {
        const ushort AudioReminderPort = 63116; //Randomly chosen port in range reseverd for private & dynamic use (49152–65535). https://en.wikipedia.org/wiki/List_of_TCP_and_UDP_port_numbers
        const string AudioReminderUriPath = "AudioReminder";

        public static string GetAudioReminderUri()
        {
            string uriAdress = $"http://localhost:{AudioReminderPort}/{AudioReminderUriPath}";

            return uriAdress;
        }


        #region Client side only code
        public static Binding GetAudioReminderBinding()
        {
            //Specify the binding to be used for the client.
            BasicHttpBinding binding = new BasicHttpBinding();

            return binding;
        }

        public static EndpointAddress GetAudioReminderEndpoint()
        {
            string uriAdress = GetAudioReminderUri();

            //Specify the address to be used for the client.
            EndpointAddress endpoint = new EndpointAddress(uriAdress);

            return endpoint;
        }
        #endregion


    }
}
