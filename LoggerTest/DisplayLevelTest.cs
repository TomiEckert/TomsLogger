using System;
using System.Collections.Generic;
using NUnit.Framework;
using TomsLogger;
using TomsLogger.Config;
using TomsLogger.Model;

namespace LoggerTest {
    public class DisplayLevelTest {
        private const int LOG_COUNT = 25;
        private readonly object _lock = new object();
        private readonly Action<string>[] _logs = {Logger.Debug, Logger.Info, Logger.Warning, Logger.Error};
        private List<string> _result;

        private void SetDisplayLevel(LogLevel level) {
            _result = new List<string>();
            var config = LoggerConfigBuilder.Default
                                            .SetCallback(SaveLog)
                                            .SetDisplayLevel(level)
                                            .Build();
            Logger.Initialize(config);
        }

        private void SaveLog(string entry) {
            lock (_lock) {
                _result.Add(entry);
            }
        }

        [Test]
        public void DisplayLevelNoneTest() {
            SetDisplayLevel(LogLevel.None);
            RunTest(0, 0, 0, 0);
        }

        [Test]
        public void DisplayLevelDebugTest() {
            SetDisplayLevel(LogLevel.Debug);
            RunTest(LOG_COUNT, LOG_COUNT * 2, LOG_COUNT * 3, LOG_COUNT * 4);
        }

        [Test]
        public void DisplayLevelInfoTest() {
            SetDisplayLevel(LogLevel.Info);
            RunTest(0, LOG_COUNT, LOG_COUNT * 2, LOG_COUNT * 3);
        }

        [Test]
        public void DisplayLevelWarningTest() {
            SetDisplayLevel(LogLevel.Warning);
            RunTest(0, 0, LOG_COUNT, LOG_COUNT * 2);
        }

        [Test]
        public void DisplayLevelErrorTest() {
            SetDisplayLevel(LogLevel.Error);
            RunTest(0, 0, 0, LOG_COUNT);
        }

        private void RunTest(params int[] expected) {
            for (var j = 0; j < 4; j++) {
                for (var i = 0; i < LOG_COUNT; i++) _logs[j].Invoke("Text");
                Assert.AreEqual(expected[j], _result.Count);
            }
        }
    }
}