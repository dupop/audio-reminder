using AudioReminderCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AudioReminderService.RingerCalling
{
    public static class RingingCaller
    {
        //TODO: handle this conflict with client names, maybe use guid... or we will use IDs intead of names anyway;we could use another line to signal that this is just a beep if needed; use separate name for beep; implement the beep on other side, and detection of beep request
        const string BeepReminderName = "test3";
        const string TestReminderName = "test3";

        public static void RingReminder(string reminderName)
        {
            new RingingClinetPipeHandler().TriggerRinging(reminderName);
        }

        public static void RingReminderTest()
        {
            new RingingClinetPipeHandler().TriggerRinging(TestReminderName);
        }

        public static void RingBeep()
        {
            new RingingClinetPipeHandler().TriggerRinging(BeepReminderName);
        }

    }
}
