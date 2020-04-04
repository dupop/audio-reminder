using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore
{
    public static class LoggingHelper
    {
        public static void RunWithTryCatch(Action methodToRun)
        {
            try
            {
                methodToRun();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Error during AudioRemidner service execution:");
            }
        }
    }
}
