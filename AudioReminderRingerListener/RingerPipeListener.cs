using AudioReminderCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderRingerListener
{
    class RingerPipeListener
    {
        public string WaitForRequest()
        {
            Log.Logger.Information($"Started pipe listener");

            //TODO: handling connection issues and connection termination, probably use use(stream){ ..} block
            //TODO: maybe add ACL later, PipeTransmissionMode.Message?
            NamedPipeServerStream pipeServer = new NamedPipeServerStream(NamedPipeHelper.RingingPipeName, PipeDirection.InOut, 1);
            StreamReader lineReader = new StreamReader(pipeServer);

            Log.Logger.Information($"Waiting for connection");
            pipeServer.WaitForConnection();

            //StreamWriter lineWriter = new StreamWriter(pipeServer);
            //lineWriter.WriteLine();
            
            Log.Logger.Information($"Clinet connected, waiting for input");
            string reminderName = lineReader.ReadLine();
            Log.Logger.Information($"Clinet sent '{reminderName}' as reminderName");

            pipeServer.Close();

            return reminderName;
        }
    }
}
