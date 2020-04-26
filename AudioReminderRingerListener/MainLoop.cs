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

    class MainLoop
    {
        //TODO: change code to use different pipes or handle async issues in some other way because currently beep can't be played until ringing dialog is open - and after that it plays late! It could probably be played multiple times in that case.
        public static void RunLoop()
        {
            do
            {
                RingerPipeListener pipeListener = new RingerPipeListener();
                RingerAppRunner runner = new RingerAppRunner();

                string receivedCommand = pipeListener.WaitForRequest();

                if (receivedCommand.StartsWith(NamedPipeHelper.StartReminderRingingCommand))
                {
                    string reminderName = receivedCommand.Substring(NamedPipeHelper.StartReminderRingingCommand.Length + 1); //+1 for space after it //TODO: out of range exception handling
                    runner.RunRinger(reminderName);
                }
                else if (receivedCommand.StartsWith(NamedPipeHelper.StartReminderRingingTestCommand))
                {
                    //TODO: handle this name conflict with client names, use guid IDs intead of names in coomunication; As test is not a valid guid that will solve the conflict and also enable using names with spaces

                    string reminderName = NamedPipeHelper.TestReminderName;
                    runner.RunRinger(reminderName);
                }
                else if(receivedCommand.StartsWith(NamedPipeHelper.StartBeeperSoundCommand))
                {
                    runner.RunBeeper();
                }
                else
                {
                    Log.Logger.Error($"Unrecognized command: \"{receivedCommand}\" ignored.");
                }

            }
            while (true);//TODO handle this; consider how this app will be stopped.. how shuould it beahve during logoff/shutdown
        }
    }
}
