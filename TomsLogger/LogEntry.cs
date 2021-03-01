using System;

namespace TomsLogger {
    public readonly struct LogEntry {
        private string ObjectName { get; }
        private string Message { get; }
        private TimeSpan Time { get; }
        internal LogLevel Level { get; }

        private LogEntry(string objectName, string message, LogLevel level) {
            ObjectName = objectName;
            Message = message;
            Time = DateTime.Now.TimeOfDay;
            Level = level;
        }

        internal static LogEntry Debug(string sender, string message) {
            return new LogEntry(sender, message, LogLevel.Debug);
        }
        internal static LogEntry Info(string sender, string message) {
            return new LogEntry(sender, message, LogLevel.Info);
        }
        internal static LogEntry Warning(string sender, string message) {
            return new LogEntry(sender, message, LogLevel.Warning);
        }
        internal static LogEntry Error(string sender, string message) {
            return new LogEntry(sender, message, LogLevel.Error);
        }

        public override string ToString() {
            var timeBlock = "[" + Time.ToString(@"hh\:mm\:ss") + "]";
            var levelBlock = "[" + Level + "]";
            var senderBlock = "[" + ObjectName + "]";
            return $"{timeBlock} {levelBlock} {senderBlock} {Message}";
        }
    }
}