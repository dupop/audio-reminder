using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminder.RingingCaller
{
    class ReminderRingingCaller
    {
        public virtual void CallReminderRinging(string reminderName)
        {
            string ringerApplicationFullPath = GetRingerFullFilePath();

            //new WinAPiHelper().TriggerRinging(reminderName, ringerApplicationFullPath);
            SimpleProcessStartApporach(reminderName, ringerApplicationFullPath);
            //new RingingTriggeringPipeHandler().TriggerRinging(reminderName, ringerApplicationFullPath);
        }

        private void SimpleProcessStartApporach(string reminderName, string ringerApplicationFullPath)
        {
            Log.Logger.Information($"Calling ReminderRinger app as simple process [path = {ringerApplicationFullPath}, arg = {reminderName}]");
            Process.Start(ringerApplicationFullPath, reminderName); //TODO: handle file not exist and other starting issues
        }

        protected virtual string GetRingerFullFilePath()
        {
            string serviceDir = AppDomain.CurrentDomain.BaseDirectory; //TODO: extract both occuranecs of this
            string productDir = new DirectoryInfo(serviceDir).Parent.Parent.Parent.FullName; // TODO: possible no rights execption + null exceptions + etc

            //TODO: extract harcoded part somwhere
            //string threeForldersUp = @"\.\.\.\";
            string ringingAppplicationSubDir = @"ReminerRinging\bin\Debug"; //TODO: fix after rename
            string ringingApplicationName = "ReminerRinging.exe"; //TODO fix after rename

            //string productDir = Path.Combine(threeForldersUp, serviceDir);
            string ringerDir = Path.Combine(productDir, ringingAppplicationSubDir);
            string ringerApplicationFullPath = Path.Combine(ringerDir, ringingApplicationName);
            return ringerApplicationFullPath;
        }
    }
}
