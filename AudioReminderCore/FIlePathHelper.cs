using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore
{
    /// <summary>
    /// Path related helper methods
    /// </summary>
    public class FilePathHelper
    {
        /// <summary>
        /// Finds current program directory.
        /// </summary>
       public static string FindDirectory()
        {
            string programDirectory = AppDomain.CurrentDomain.BaseDirectory;

            return programDirectory;
        }

    }
}
