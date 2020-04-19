using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore
{
    /// <summary>
    /// Provides global logger configuration and creating functionallity
    /// </summary>
    public class LoggerInitializer
    {
        #region Properties and contants
        public string LoggingFilenameFormat { get; set; }
        public string LoggingPath { get; set; }


        private const string DefaultLoggingFilenameFormat = "log-.log";
        #endregion


        #region Public methods
        public LoggerInitializer()
        {
            LoggingFilenameFormat = GetDefaultLoggingFilenameFormat();
            LoggingPath = GetDefaultLoggingPath();
        }

        /// <summary>
        /// Creates the global logger
        /// </summary>
        public virtual void CreateLogger()
        {
            string loggingPathAndFilenameFormat = GetLoggingPathAndFilenameFormat();

            Log.Logger = new LoggerConfiguration()
            .WriteTo.File(loggingPathAndFilenameFormat, rollingInterval: RollingInterval.Day)
            .CreateLogger();

            Log.Logger.Information($"Serilog configured [path='{LoggingPath}', filename='{LoggingFilenameFormat}']");
        }
        #endregion


        #region Protected and private methods
        protected virtual string GetDefaultLoggingPath()
        {
            const string LogsSubfolderName = @"logs";
            string servicePath = FilePathHelper.FindProgramDirectory();

            string loggingPath = System.IO.Path.Combine(servicePath, LogsSubfolderName);

            return loggingPath;
        }

        protected virtual string GetDefaultLoggingFilenameFormat()
        {
            return DefaultLoggingFilenameFormat;
        }

        protected virtual string GetLoggingPathAndFilenameFormat()
        {
            return System.IO.Path.Combine(LoggingPath, LoggingFilenameFormat);
        }
        #endregion
    }
}
