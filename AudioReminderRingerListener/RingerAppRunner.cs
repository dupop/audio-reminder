using AudioReminderCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderRingerListener
{
    class RingerAppRunner
    {
        private void SimpleProcessStartApporach(string arguments, string applicationFullPath)
        {
            if(arguments != null)
            {
                Log.Logger.Information($"Calling app as simple process [path = {applicationFullPath}, arg = {arguments}]");
                Process.Start(applicationFullPath, arguments); //TODO: handle file not exist and other starting issues
            }
            else
            {
                Log.Logger.Information($"Calling app as simple process [path = {applicationFullPath}, no args]");
                Process.Start(applicationFullPath); //TODO: handle file not exist and other starting issues
            }
        }

        public void RunRinger(string reminderName)
        {
            string ringerPath = FilePathHelper.GetRingerFullFilePath();

            SimpleProcessStartApporach(reminderName, ringerPath);
        }

        public void RunBeeper()
        {
            string beeperPath = FilePathHelper.GetBeeperFullFilePath();

            SimpleProcessStartApporach(null, beeperPath);
        }

    }
}
