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
        protected virtual string GetRingerFullFilePath()
        {
            string serviceDir = AppDomain.CurrentDomain.BaseDirectory; //TODO: extract both occuranecs of this to PathHelper
            string productDir = new DirectoryInfo(serviceDir).Parent.Parent.Parent.FullName; // TODO: possible no rights execption + null exceptions + etc

            //TODO: extract harcoded part somwhere
            //string threeForldersUp = @"\.\.\.\";
            string ringingAppplicationSubDir = @"AudioReminderRinging\bin\Debug"; 
            string ringingApplicationName = "AudioReminderRinging.exe";

            //string productDir = Path.Combine(threeForldersUp, serviceDir);
            string ringerDir = Path.Combine(productDir, ringingAppplicationSubDir);
            string ringerApplicationFullPath = Path.Combine(ringerDir, ringingApplicationName);
            return ringerApplicationFullPath;
        }

        private void SimpleProcessStartApporach(string reminderName, string ringerApplicationFullPath)
        {
            Log.Logger.Information($"Calling ReminderRinger app as simple process [path = {ringerApplicationFullPath}, arg = {reminderName}]");
            Process.Start(ringerApplicationFullPath, reminderName); //TODO: handle file not exist and other starting issues
        }

        public void Run(string reminderName)
        {
            string ringerPath = GetRingerFullFilePath();

            SimpleProcessStartApporach(reminderName, ringerPath);
        }

    }
}
