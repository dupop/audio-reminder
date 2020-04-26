using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore
{
    public static class NamedPipeHelper
    {
        public const string RingingPipeName = @"AudioReminder-3D7C1DE8-2DFB-4291-9396-8E0CA4E8AD10test3";

        //Commands used over the pipe. Semmicolon terminates the command name. That prevents against e.g. remindertest being detected as reminder command because it also starts with those letters
        public const string StartReminderRingingCommand = "reminder;";
        public const string StartReminderRingingTestCommand = "remindertest;";
        public const string StartBeeperSoundCommand = "beeper;";

        /// <summary>
        /// Name of reminder that will be passed from RingerRunner to Ringer when ringing should be done just as an example to user.
        /// </summary>
        public const string TestReminderName = "test";
    }
}
