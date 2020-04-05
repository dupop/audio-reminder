using AudioReminderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderRingerListener
{
    class Program
    {
        static void Main(string[] args)
        {
            new LoggerInitializer().CreateLogger();

            LoggingHelper.RunWithExceptionLogging(MainLoop.RunLoop);
        }
    }
}
