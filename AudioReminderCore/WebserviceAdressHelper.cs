using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore
{
    public static class WebserviceAdressHelper
    {
        const ushort AudioReminderPort = 63116; //Randomly chosen port in range reseverd for private & dynamic use (49152–65535). https://en.wikipedia.org/wiki/List_of_TCP_and_UDP_port_numbers
        const string AudioReminderUriPath = "AudioReminder";

        public static string CreateUriAdress()
        {
            string uriAdress = $"http://localhost:{AudioReminderPort}/{AudioReminderUriPath}";

            return uriAdress;
        }
    }
}
