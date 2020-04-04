using AudioReminderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioUserInterface
{
    static class Program
    {
        //TODO: block paralel exec to prevent data conlficts
        //TODO: add settings to show detailed info in list
        //TODO: how should provider work when service is not running?

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
