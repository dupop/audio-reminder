using AudioReminderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioReminderUI
{
    //TODO: make master default branch again or keep develop as default?
    //TODO: add help button and add there thanks to Rale
    //TODO: DP->SI: validation during reading from file
    //TODO: creating (remote?) beckap of reminders file, or "exporting" feature

    static class Program
    {
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
