using System;
using TomsLogger.Model;

namespace TomsLogger.Config {
    public class LoggerConfig {
        internal LoggerConfig(Action<string> callback, string fileName, bool writeToFile, LogLevel displayLevel,
                              bool fullClassName) {
            Callback = callback;
            FileName = fileName;
            WriteToFile = writeToFile;
            DisplayLevel = displayLevel;
            UseFullClassName = fullClassName;
        }

        internal Action<string> Callback { get; }
        internal string FileName { get; }
        internal bool WriteToFile { get; }
        internal LogLevel DisplayLevel { get; }
        internal bool UseFullClassName { get; }
    }
}