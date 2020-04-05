using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore
{
    public static class NamedPipeHelper
    {
        const string NamedPipeName = @"AudioReminder-3D7C1DE8-2DFB-4291-9396-8E0CA4E8AD10";

        public static NamedPipeServerStream CreateRingingTriggerNamedPipe()
        {
            return new NamedPipeServerStream(NamedPipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message); //TODO: maybe add ACL later
        }
    }
}
