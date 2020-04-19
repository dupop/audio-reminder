using System;
using System.Collections.Generic;
using System.Linq;
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
            PlayBeepSound();
        }

        private static void PlayBeepSound()
        {
            //TODO: read sound path from a simple config file nearby with a hardcoded name; validate file existance and play it instead of beep; Play Console.Beep as fallback; Reuse same code in ringer
            Console.Beep();
        }
    }
}
