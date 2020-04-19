using AudioReminderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioReminderUI
{
    //TODO: make master default branch again or keep develop as default?
    //TODO: DP->SI add help button and add there thanks to Rale & test that with NVDA free narator program from internet
    //TODO: creating (remote?) backup of reminders file, and "exporting" feature
    //TODO: Useful feature - time until next reminder on UI

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
