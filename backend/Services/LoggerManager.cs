﻿using NLog;

namespace Logger.Service
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
    public class LoggerManager : ILoggerManager
    {
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message) => logger?.Debug(message);
        public void LogError(string message) => logger?.Error(message);
        public void LogInfo(string message) => logger?.Info(message);
        public void LogWarning(string message) => logger?.Warn(message);

    }
}
