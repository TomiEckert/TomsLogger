using System;
using TomsLogger.Model;

namespace TomsLogger.Config {
    public class LoggerConfig {
        internal LoggerConfig() {
        }

        internal Action<string> Callback { get; set; }
        internal string FileName { get; set; }
        internal bool WriteToFile { get; set; }
        internal LogLevel DisplayLevel { get; set; }
        internal bool UseFullClassName { get; set; }
    }
}