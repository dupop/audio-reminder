using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore
{
    /// <summary>
    /// File path related utility methods
    /// </summary>
    public static class FilePathHelper
    {
        //TODO: make this less hardcoded maybe?
        //TODO: root paths could be chosed during install (exe EXEs in Program Files and data in AppData)

        #region Directory and filename constants
#if DEBUG
        /// <summary>
        /// Beeper application dir relative to the product dir - for DEBUG build configuration.
        /// </summary>
        const string beeperAppplicationSubDir = @"AudioReminderBeeper\bin\Debug";
        /// <summary>
        /// Ringing application dir relative to the product dir - for DEBUG build configuration.
        /// </summary>
        const string ringingAppplicationSubDir = @"AudioReminderRinging\bin\Debug";
#else
        //Release configuration requires application to be installed before running.
        //These paths would be incorrect if run directly
        /// <summary>
        /// Beeper application dir relative to the product dir.
        /// This relative path for RELEASE build configuration is correct only after depoloyment to instal dir!
        /// </summary>
        const string beeperAppplicationSubDir = @"AudioReminderBeeper";
        /// <summary>
        /// Ringing application dir relative to the product dir.
        /// This relative path for RELEASE build configuration is correct only after depoloyment to instal dir!
        /// </summary>
        const string ringingAppplicationSubDir = @"AudioReminderRinging";
#endif
        const string beeperApplicationName = "AudioReminderBeeper.exe";
        const string ringingApplicationName = "AudioReminderRinging.exe";
        #endregion


        /// <summary>
        /// Finds current application directory.
        /// </summary>
        public static string GetProgramDirectory()
        {
            string programDirectory = AppDomain.CurrentDomain.BaseDirectory;

            return programDirectory;
        }

        /// <summary>
        /// Finds the root directory where all executables in the product are placed.
        /// This will by default be "...\Program Files\Audio Reminder\bin".
        /// </summary>
        public static string GetProductBinDir()
        {
            string currentProgramDir = GetProgramDirectory();
            DirectoryInfo currentProgramDirInfo = new DirectoryInfo(currentProgramDir);

            // TODO: possible no rights execption + null exceptions + etc
#if DEBUG
            //Product dir (Solution folder) is 3 folders up from executable in Visual Studio solution
            string productDir = currentProgramDirInfo.Parent.Parent.Parent.FullName; 
#else
            //Product dir is just 1 folder up from executable when deployed to install folder
            string productDir = currentProgramDirInfo.Parent.FullName;
#endif

            return productDir;
        }

        /// <summary>
        /// Finds the root directory where all user data in the product are placed.
        /// This will by default be "%OsDrive%\ProgramData".
        /// </summary>
        /// <remarks>
        /// Data is not stored in AppData of current user but in user-agnostic ProgramData dir because
        /// Audio Reminder service can't access this as it is a Windows service not running as currnet user.
        /// </remarks>
        public static string GetProductDataDir()
        {
            const string ProductName = "AudioReminder";
            //TODO: add to uninstall options
            string appDataLocal = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            return Path.Combine(appDataLocal, ProductName);
        }

        public static string GetBeeperFullFilePath()
        {
            string productDir = GetProductBinDir();

            return Path.Combine(productDir, beeperAppplicationSubDir, beeperApplicationName);
        }

        public static string GetRingerFullFilePath()
        {
            string productDir = GetProductBinDir();

            return Path.Combine(productDir, ringingAppplicationSubDir, ringingApplicationName);
        }

        public static string GetTranslationsDir()
        {
            string translationsDir = System.IO.Path.Combine(GetProductBinDir(), "Translations");

            return translationsDir;
        }
        
    }
}
