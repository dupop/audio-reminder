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

namespace AudioReminder.RingingCaller
{
    class RingingTriggeringPipeHandler
    {
        NamedPipeServerStream pipe = NamedPipeHelper.CreateRingingTriggerNamedPipe();

        public void TriggerRinging(string reminderName, string ringerApplicationFullPath)
        {
            Log.Logger.Information($"Calling ReminderRinger app through a named pipe [path = {ringerApplicationFullPath}, arg = {reminderName}]");
            
            // Wait for a client to connect
            pipe.WaitForConnection();
        }

    }
}
