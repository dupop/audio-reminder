using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioReminderBeeper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExecuteInNewThread(PlayBeepSound, true);
        }

        //TODO: algorithm copied to Ringer, extract it
        /// <summary>
        /// Provides background execution which doesn't block UI.
        /// When keepProgramOpen is used, program is not closed until the task is finished.
        /// </summary>
        private static void ExecuteInNewThread(ThreadStart task, bool keepProgramOpen)
        {
            var newThread = new Thread(task);
            newThread.IsBackground = !keepProgramOpen;

            newThread.Start();
        }

        private static void PlayBeepSound()
        {
            var player = new System.Media.SoundPlayer();
            player.Stream = GetBeepSound();
            player.PlaySync();
        }

        private static Stream GetBeepSound()
        {
            return Properties.Resources._491889__lordfrodo10__19_5_magia2;

            //TODO: When configureable sounds are added validate file existance; Play this default sound if configured sound is not present
        }
    }
}
