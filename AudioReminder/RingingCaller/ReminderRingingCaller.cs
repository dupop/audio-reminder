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

            //Win32ApiApproach(reminderName, ringerApplicationFullPath);
            SimpleProcessStartApporach(reminderName, ringerApplicationFullPath);
            //NamedPipeApproach(reminderName, ringerApplicationFullPath);
        }

        private void SimpleProcessStartApporach(string reminderName, string ringerApplicationFullPath)
        {
            Log.Logger.Information($"Calling ReminderRinger app [path = {ringerApplicationFullPath}, arg = {reminderName}]");
            Process.Start(ringerApplicationFullPath, reminderName); //TODO: handle file not exist and other starting issues
        }

        private static void NamedPipeApproach(string reminderName, string ringerApplicationFullPath)
        {
            Log.Logger.Information($"Contacting ringer application thorugh a named pipe");

            RingingTriggeringPipeHandler pipeHandler = new RingingTriggeringPipeHandler();
            pipeHandler.triggerRinging();
        }

        private static string GetRingerFullFilePath()
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
