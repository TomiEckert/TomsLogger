using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TomsLogger.Config;

namespace TomsLogger {
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class Logger {
        private static LoggerService service;

        public static void Initialize(LoggerConfig config) {
            service = new LoggerService(config);
        }

        public static void SaveToFile(string filename) {
            service.SaveToFile(filename);
        }

        public static void Debug(string message, [CallerMemberName] string objectName = "unknown") {
            var entry = LogEntry.Debug(objectName, message);
            service ??= new LoggerService(LoggerConfigBuilder.Default.Build());
            service.Add(entry);
        }

        public static void Info(string message, [CallerMemberName] string objectName = "unknown") {
            var entry = LogEntry.Info(objectName, message);
            service ??= new LoggerService(LoggerConfigBuilder.Default.Build());
            service.Add(entry);
        }

        public static void Warning(string message, [CallerMemberName] string objectName = "unknown") {
            var entry = LogEntry.Warning(objectName, message);
            service ??= new LoggerService(LoggerConfigBuilder.Default.Build());
            service.Add(entry);
        }

        public static void Error(string message, [CallerMemberName] string objectName = "unknown") {
            var entry = LogEntry.Error(objectName, message);
            service ??= new LoggerService(LoggerConfigBuilder.Default.Build());
            service.Add(entry);
        }
    }
}