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
    
    class MainLoop
    {
        public static void RunLoop()
        {
            do
            {
                RingerPipeListener pipeListener = new RingerPipeListener();
                RingerAppRunner runner = new RingerAppRunner();

                string reminderName = pipeListener.WaitForRequest();
                runner.Run(reminderName);
            }
            while (true);//TODO handle this; consider how this app will be stopped.. how shuould it beahve during logoff/shutdown
        }
    }
}
