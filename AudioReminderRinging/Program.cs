﻿using AudioReminderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioReminderRinging
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new LoggerInitializer("AudioReminderRinging").CreateLogger();

            LoggingHelper.RunWithExceptionLogging(RunApplication);
        }

        private static void RunApplication( )
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new reminderRingingForm());
        }
    }
}
