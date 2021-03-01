using System;
using System.Collections.Generic;
using System.IO;
using TomsLogger.Config;

namespace TomsLogger {
    internal class LoggerService {
        private readonly object _listLock = new object();
        private readonly object _fileLock = new object();
        private readonly List<LogEntry> _entries;
        private readonly Action<string> _callback;
        private readonly bool _writeToFile;
        private readonly string _filename;
        private readonly LogLevel _displayLevel;

        public LoggerService(LoggerConfig config) {
            _entries = new List<LogEntry>();
            _callback = config.Callback;
            _writeToFile = config.WriteToFile;
            _filename = config.FileName;
            _displayLevel = config.DisplayLevel;
        }
        
        public void Add(LogEntry entry) {
            lock (_listLock) {
                _entries.Add(entry);
            }

            if(entry.Level >= _displayLevel)
                _callback?.Invoke(entry.ToString());
            
            if (!_writeToFile) return;
            lock (_fileLock) {
                File.AppendAllText(_filename, entry + Environment.NewLine);
            }
        }
        
        public void SaveToFile(string filename, bool append = true) {
            string content;
            lock (_listLock) {
                content = string.Join(Environment.NewLine, _entries);
            }

            lock (_fileLock) {
                if(!File.Exists(filename)) File.Create(filename).Close();
                else if (append) content = File.ReadAllText(filename) + Environment.NewLine + content;
                File.WriteAllText(filename, content);
            }
        }
    }
}