using AudioReminderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioReminderUI
{
    static class Program
    {
        //TODO: block paralel exec to prevent data conlficts
        //TODO: add settings to show detailed info in list
        //TODO: how should provider work when service is not running?
        //TODO: rolling file sink instead of normal file sink to prrevent HDD overflow
        //TODO: flush reminders and settings to disk after each change in list, to prevent data loss on powerdown
        //TODO: why do i have differnet version of serilog in projects..

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new LoggerInitializer().CreateLogger();

            LoggingHelper.RunWithExceptionLogging(RunApplication);
        }

        private static void RunApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenuForm());
        }
    }
}
