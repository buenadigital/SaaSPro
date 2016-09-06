using System;
using System.Configuration;
using log4net;
using log4net.Config;

namespace SaaSPro.Infrastructure.Logging
{
    public class LogAdapter : ILogger
    {
        private readonly ILog _log;

        public LogAdapter()
        {
            XmlConfigurator.Configure();
            _log = LogManager.GetLogger(ConfigurationManager.AppSettings["LoggerName"]);
        }

        public void Log(string message,EventLogSeverity logSeverity)
        {
            switch (logSeverity)
            {
                case EventLogSeverity.Debug:
                    _log.Debug(message);
                    break;
                case EventLogSeverity.Error:
                    _log.Error(message);
                    break;
                case EventLogSeverity.Fatal:
                    _log.Error(message);
                    break;
                case EventLogSeverity.Information:
                    _log.Info(message);
                    break;
                case EventLogSeverity.None:
                    _log.Info(message);
                    break;
                case EventLogSeverity.Warning:
                    _log.Warn(message);
                    break;
            }
        }

        public void LogError(Exception ex)
        {
            _log.Error(ex.Message);
        }
    }
    /// <summary>
    /// EventLogSeverity
    /// </summary>
    public enum EventLogSeverity
    {
        /// <summary>
        /// Debug
        /// </summary>
        Debug,

        /// <summary>
        /// Error
        /// </summary>
        Error,

        /// <summary>
        /// Fatal
        /// </summary>
        Fatal,

        /// <summary>
        /// Information
        /// </summary>
        Information,

        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// Warning
        /// </summary>
        Warning
    }
}
