using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioReminderUI
{
    public static class ErrorDialogUtility
    {
        public static void ErrorDialog(string errorText)
        {
            MessageBox.Show(errorText, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Log.Logger.Information(errorText);
        }
    }
}
