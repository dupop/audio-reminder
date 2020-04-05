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

    public class WinAPiHelper
    {
        //TODO:appropriate handle returnes error codes https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-wtsgetactiveconsolesessionid

        [DllImport("Kernel32.dll")]
        protected static extern uint WTSGetActiveConsoleSessionId();

        public void TriggerRinging(string reminderName, string ringerApplicationFullPath)
        {
            Log.Logger.Information($"Calling ReminderRinger app through WinApi [path = {ringerApplicationFullPath}, arg = {reminderName}]");

            uint desktopWindowSessionId = WTSGetActiveConsoleSessionId();
            uint winlogonProcessId = GetWinLogonProcessId(desktopWindowSessionId); //TODO:appropriate handle returnes error codes

            // obtain a handle to the winlogon process
            //hProcess = OpenProcess(MAXIMUM_ALLOWED, false, winlogonProcessId);
        }

        private uint GetWinLogonProcessId(uint dwSessionId)
        {
            uint winlogonPid = 0;

            // obtain the process id of the winlogon process that 
            // is running within the currently active session
            Process[] processes = Process.GetProcessesByName("winlogon");
            foreach (Process p in processes)
            {
                if ((uint)p.SessionId == dwSessionId)
                {
                    winlogonPid = (uint)p.Id;
                }
            }

            return winlogonPid;
        }

    }
}
