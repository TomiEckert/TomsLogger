using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TomsLogger.Config;
using TomsLogger.Model;

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

        public static void Debug(string message) {
            if(service.DisplayLevel > LogLevel.Debug) return;
            var entry = LogEntry.Debug(GetSender(), message);
            service ??= new LoggerService(LoggerConfigBuilder.Default.Build());
            service.Add(entry);
        }

        public static void Info(string message) {
            if(service.DisplayLevel > LogLevel.Info) return;
            var entry = LogEntry.Info(GetSender(), message);
            service ??= new LoggerService(LoggerConfigBuilder.Default.Build());
            service.Add(entry);
        }

        public static void Warning(string message) {
            if(service.DisplayLevel > LogLevel.Warning) return;
            var entry = LogEntry.Warning(GetSender(), message);
            service ??= new LoggerService(LoggerConfigBuilder.Default.Build());
            service.Add(entry);
        }

        public static void Error(string message) {
            if(service.DisplayLevel > LogLevel.Error) return;
            var entry = LogEntry.Error(GetSender(), message);
            service ??= new LoggerService(LoggerConfigBuilder.Default.Build());
            service.Add(entry);
        }

        private static string GetSender() {
            var mb = new StackTrace().GetFrame(2)?.GetMethod();
            var mi = mb as MethodInfo;
            if (mi == null) return mb?.Name;
            var c = mi.DeclaringType?.Name + ".";
            return c + mi.Name;
        }
    }
}