using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminder
{
    class UserInterfaceCommunication
    {
        const string NamedPipeName = @"AudioReminder-3D7C1DE8-2DFB-4291-9396-8E0CA4E8AD10";


        NamedPipeServerStream pipeServer = new NamedPipeServerStream(NamedPipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message);
        
        public void test()
        {
            //pipeServer.
        }
    }
}
