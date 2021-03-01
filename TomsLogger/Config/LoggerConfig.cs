using System;

namespace TomsLogger.Config {
    public class LoggerConfig {
        internal LoggerConfig(Action<string> callback, string fileName, bool writeToFile, LogLevel displayLevel) {
            Callback = callback;
            FileName = fileName;
            WriteToFile = writeToFile;
            DisplayLevel = displayLevel;
        }

        internal Action<string> Callback { get; }
        internal string FileName { get; }
        internal bool WriteToFile { get; }
        internal LogLevel DisplayLevel { get; }
    }
}