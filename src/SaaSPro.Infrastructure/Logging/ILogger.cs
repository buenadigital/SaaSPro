using System;

namespace SaaSPro.Infrastructure.Logging
{
    public interface ILogger
    {
        void Log(string message, EventLogSeverity logSeverity);
        void LogError(Exception ex);
    }
}
