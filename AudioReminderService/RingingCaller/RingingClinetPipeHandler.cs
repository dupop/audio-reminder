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
        public void TriggerRinging(string reminderName)
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
