using System;
using System.Collections.Generic;
using System.IO;
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
        public static string FindProgramDirectory()
        {
            string programDirectory = AppDomain.CurrentDomain.BaseDirectory;

            return programDirectory;
        }

        public static string GetProductDir()
        {
            string currentProgramDir = FindProgramDirectory();
            string productDir = new DirectoryInfo(currentProgramDir).Parent.Parent.Parent.FullName; // TODO: possible no rights execption + null exceptions + etc
            return productDir;
        }

    }
}
