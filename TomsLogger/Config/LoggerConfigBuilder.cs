using System;
using System.Diagnostics.CodeAnalysis;
using TomsLogger.Model;

namespace TomsLogger.Config {
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class LoggerConfigBuilder {
        private LoggerConfigBuilder() {
            Config = new LoggerConfig {
                Callback = Console.WriteLine,
                FileName = "log.txt",
                WriteToFile = true,
                DisplayLevel = LogLevel.Info,
                UseFullClassName = false
            };
        }

        private LoggerConfig Config { get; }

        public static LoggerConfigBuilder Default => new LoggerConfigBuilder();

        public LoggerConfigBuilder SetCallback(Action<string> callback) {
            Config.Callback = callback;
            return this;
        }

        public LoggerConfigBuilder SetFilename(string filename) {
            Config.WriteToFile = true;
            Config.FileName = filename;
            return this;
        }

        public LoggerConfigBuilder SetSaveToFile(bool save) {
            Config.WriteToFile = save;
            return this;
        }

        public LoggerConfigBuilder SetUseFullClassName(bool fullName) {
            Config.UseFullClassName = fullName;
            return this;
        }

        public LoggerConfigBuilder SetDisplayLevel(LogLevel level) {
            Config.DisplayLevel = level;
            return this;
        }

        public LoggerConfig Build() {
            return Config;
        }
    }
}