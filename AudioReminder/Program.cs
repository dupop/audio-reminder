using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AudioReminder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            Thread.Sleep(5000);
#endif

            new LoggerInitializer().CreateLogger();

            Log.Logger.Information("Program started");

            RunService();

            Log.Logger.Information("Program ended");
        }

        private static void RunService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new AudioReminderService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }

    //TODO: 
    //add all copyrights...
    //align namespace

}
