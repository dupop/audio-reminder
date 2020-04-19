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
        public static void RingReminder(string reminderName)
        {
            string commandToSend = NamedPipeHelper.StartReminderRingingCommand + " " + reminderName;
            new RingingClinetPipeHandler().TriggerRinging(commandToSend);
        }

        public static void RingReminderTest()
        {
            string commandToSend = NamedPipeHelper.StartReminderRingingTestCommand;
            new RingingClinetPipeHandler().TriggerRinging(commandToSend);
        }

        public static void RingBeep()
        {
            string commandToSend = NamedPipeHelper.StartBeeperSoundCommand;
            new RingingClinetPipeHandler().TriggerRinging(commandToSend);
        }

    }
}
