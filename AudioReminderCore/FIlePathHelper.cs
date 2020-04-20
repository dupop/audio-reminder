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
        //TODO: make this less hardcoded maybe?
        const string beeperAppplicationSubDir = @"AudioReminderBeeper\bin\Debug";
        const string beeperApplicationName = "AudioReminderBeeper.exe";
        const string ringingAppplicationSubDir = @"AudioReminderRinging\bin\Debug";
        const string ringingApplicationName = "AudioReminderRinging.exe";


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

        public static string GetBeeperFullFilePath()
        {
            string productDir = GetProductDir();

            return CombinePaths(productDir, beeperAppplicationSubDir, beeperApplicationName);
        }

        public static string GetRingerFullFilePath()
        {
            string productDir = GetProductDir();

            return CombinePaths(productDir, ringingAppplicationSubDir, ringingApplicationName);
        }

        protected static string CombinePaths(string productDir, string ringingAppplicationSubDir, string ringingApplicationName)
        {
            string ringerDir = Path.Combine(productDir, ringingAppplicationSubDir);
            string ringerApplicationFullPath = Path.Combine(ringerDir, ringingApplicationName);

            return ringerApplicationFullPath;
        }
    }
}
