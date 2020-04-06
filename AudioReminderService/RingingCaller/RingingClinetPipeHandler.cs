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

namespace AudioReminderService.RingingCaller
{
    class RingingClinetPipeHandler
    {
        //TODO: handle this conflict with client names, maybe use guid... or we will use IDs intead of names anyway;we could use another line to signal that this is just a beep if needed; use separate name for beep; implement the beep on other side, and detection of beep request
        const string BeepReminderName = "test3";
        const string TestReminderName = "test3";
        
        public void RingReminder(string reminderName)
        {
            TriggerRinging(reminderName);
        }

        public void RingReminderTest()
        {
            TriggerRinging(TestReminderName);
        }

        public void RingBeep()
        {
            TriggerRinging(BeepReminderName);
        }

        protected void TriggerRinging(string reminderName)
        {
            Log.Logger.Information($"Calling ReminderRinger app through a named pipe [arg = {reminderName}]");

            //TODO: do I need this: , PipeOptions.None, TokenImpersonationLevel.Impersonation
            NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", NamedPipeHelper.RingingPipeName, PipeDirection.InOut);

            //TODO: handle server pipe not present etc
            StreamWriter sw = new StreamWriter(pipeClient);
            //StreamReader sr = new StreamReader(pipeClient); //TODO: remove if connection can properly be made wiotut this

            Log.Logger.Information($"Connecting to pipe server");
            pipeClient.Connect();//TODO: timeout etc

            //sr.ReadLine(); 

            Log.Logger.Information($"Connected. Wrinting line to pipe");
            sw.WriteLine(reminderName);

            sw.Close();
            Log.Logger.Information($"Calling ReminderRinger app through a named pipe done");
        }

    }
}
