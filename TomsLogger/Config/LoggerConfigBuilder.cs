using System;
using System.Diagnostics.CodeAnalysis;
using TomsLogger.Model;

namespace TomsLogger.Config {
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class LoggerConfigBuilder {
        private LoggerConfigBuilder() {
            Callback = Console.WriteLine;
            Filename = "log.txt";
            WriteToFile = true;
            DisplayLevel = LogLevel.Info;
        }

        private Action<string> Callback { get; set; }
        private string Filename { get; set; }
        private bool WriteToFile { get; set; }
        private LogLevel DisplayLevel { get; set; }

        public static LoggerConfigBuilder Default => new LoggerConfigBuilder();

        public LoggerConfigBuilder SetCallback(Action<string> callback) {
            Callback = callback;
            return this;
        }

        public LoggerConfigBuilder SetFilename(string filename) {
            WriteToFile = true;
            Filename = filename;
            return this;
        }

        public LoggerConfigBuilder DoNotSaveToFile() {
            WriteToFile = false;
            return this;
        }

        public LoggerConfigBuilder SetDisplayLevel(LogLevel level) {
            DisplayLevel = level;
            return this;
        }

        public LoggerConfig Build() {
            return new LoggerConfig(
                Callback,
                Filename,
                WriteToFile,
                DisplayLevel
            );
        }
    }
}