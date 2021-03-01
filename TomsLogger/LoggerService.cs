using System;
using System.Collections.Generic;
using System.IO;
using TomsLogger.Config;
using TomsLogger.Model;

namespace TomsLogger {
    internal class LoggerService {
        internal LoggerService(LoggerConfig config) {
            Entries = new List<LogEntry>();
            Callback = config.Callback;
            WriteToFile = config.WriteToFile;
            Filename = config.FileName;
            DisplayLevel = config.DisplayLevel;
        }

        private readonly object _listLock = new object();
        private readonly object _fileLock = new object();
        private Action<string> Callback { get; }
        private List<LogEntry> Entries { get; }
        private string Filename { get; }
        private bool WriteToFile { get; }
        private LogLevel DisplayLevel { get; }

        internal void Add(LogEntry entry) {
            lock (_listLock) {
                Entries.Add(entry);
            }

            if (entry.Level >= DisplayLevel)
                Callback?.Invoke(entry.ToString());

            if (!WriteToFile) return;
            lock (_fileLock) {
                File.AppendAllText(Filename, entry + Environment.NewLine);
            }
        }

        internal void SaveToFile(string filename, bool append = true) {
            string content;
            lock (_listLock) {
                content = string.Join(Environment.NewLine, Entries);
            }

            lock (_fileLock) {
                if (!File.Exists(filename)) File.Create(filename).Close();
                else if (append) content = File.ReadAllText(filename) + Environment.NewLine + content;
                File.WriteAllText(filename, content);
            }
        }
    }
}