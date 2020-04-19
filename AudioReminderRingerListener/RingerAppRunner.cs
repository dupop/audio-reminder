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
        protected virtual string GetBeeperFullFilePath()
        {
            string productDir = FilePathHelper.GetProductDir();

            //TODO: extract harcoded part somwhere
            string beeperAppplicationSubDir = @"AudioReminderBeeper\bin\Debug";
            string beeperApplicationName = "AudioReminderBeeper.exe";

            return CombinePaths(productDir, beeperAppplicationSubDir, beeperApplicationName);
        }

        protected virtual string GetRingerFullFilePath()
        {
            string productDir = FilePathHelper.GetProductDir();

            //TODO: extract harcoded part somwhere
            string ringingAppplicationSubDir = @"AudioReminderRinging\bin\Debug";
            string ringingApplicationName = "AudioReminderRinging.exe";

            return CombinePaths(productDir, ringingAppplicationSubDir, ringingApplicationName);
        }

        protected virtual string CombinePaths(string productDir, string ringingAppplicationSubDir, string ringingApplicationName)
        {
            string ringerDir = Path.Combine(productDir, ringingAppplicationSubDir);
            string ringerApplicationFullPath = Path.Combine(ringerDir, ringingApplicationName);

            return ringerApplicationFullPath;
        }

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
            string ringerPath = GetRingerFullFilePath();

            SimpleProcessStartApporach(reminderName, ringerPath);
        }

        public void RunBeeper()
        {
            string beeperPath = GetBeeperFullFilePath();

            SimpleProcessStartApporach(null, beeperPath);
        }

    }
}
