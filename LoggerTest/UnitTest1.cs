using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TomsLogger;
using TomsLogger.Config;

namespace LoggerTest {
    public class Tests {
        private const int TASK_COUNT = 500;
        private const int MAX_DELAY = 500;
        private const int MIN_DELAY = 100;
        private const int LOG_COUNT = 25;
        private readonly object _lock = new object();
        private List<string> _result;

        [SetUp]
        public void Setup() {
            // ReSharper disable once InconsistentlySynchronizedField
            _result = new List<string>();

            var config = LoggerConfigBuilder.Default
                                            .DoNotSaveToFile()
                                            .SetDisplayLevel(LogLevel.Info)
                                            .SetCallback(SaveLog)
                                            .Build();
            Logger.Initialize(config);
        }

        [Test]
        public void SimpleLogTest() {
            for (var i = 0; i < LOG_COUNT; i++) Logger.Info(string.Empty);
            Assert.AreEqual(LOG_COUNT, _result.Count);
        }

        [Test]
        public void DisplayLevelNoneTest() {
            ChangeDisplayLevel(LogLevel.None);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Debug("First log");
            Assert.AreEqual(0, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Info("Second log");
            Assert.AreEqual(0, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Warning("Third log");
            Assert.AreEqual(0, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Error("Fourth log");
            Assert.AreEqual(0, _result.Count);
        }

        [Test]
        public void DisplayLevelDebugTest() {
            ChangeDisplayLevel(LogLevel.Debug);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Debug("First log");
            Assert.AreEqual(LOG_COUNT, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Info("Second log");
            Assert.AreEqual(LOG_COUNT * 2, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Warning("Third log");
            Assert.AreEqual(LOG_COUNT * 3, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Error("Fourth log");
            Assert.AreEqual(LOG_COUNT * 4, _result.Count);
        }

        [Test]
        public void DisplayLevelInfoTest() {
            ChangeDisplayLevel(LogLevel.Info);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Debug("First log");
            Assert.AreEqual(0, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Info("Second log");
            Assert.AreEqual(LOG_COUNT * 1, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Warning("Third log");
            Assert.AreEqual(LOG_COUNT * 2, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Error("Fourth log");
            Assert.AreEqual(LOG_COUNT * 3, _result.Count);
        }

        [Test]
        public void DisplayLevelWarningTest() {
            ChangeDisplayLevel(LogLevel.Warning);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Debug("First log");
            Assert.AreEqual(0, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Info("Second log");
            Assert.AreEqual(0, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Warning("Third log");
            Assert.AreEqual(LOG_COUNT, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Error("Fourth log");
            Assert.AreEqual(LOG_COUNT * 2, _result.Count);
        }

        [Test]
        public void DisplayLevelErrorTest() {
            ChangeDisplayLevel(LogLevel.Error);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Debug("First log");
            Assert.AreEqual(0, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Info("Second log");
            Assert.AreEqual(0, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Warning("Third log");
            Assert.AreEqual(0, _result.Count);

            for (var i = 0; i < LOG_COUNT; i++) Logger.Error("Fourth log");
            Assert.AreEqual(LOG_COUNT, _result.Count);
        }

        [Test]
        public void ThreadSafetyTest() {
            var tasks = new Task[TASK_COUNT];
            var random = new Random();

            for (var i = 0; i < tasks.Length; i++) tasks[i] = DoStuffAsync(random.Next(MIN_DELAY, MAX_DELAY));

            Task.WaitAll(tasks);

            Assert.False(tasks.Any(t => !((Task<bool>) t).Result));

            Assert.AreEqual(TASK_COUNT * 3, _result.Count);
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private async Task<bool> DoStuffAsync(int random) {
            try {
                Logger.Info("Stage 1");
            }
            catch (Exception) {
                return false;
            }

            await Task.Delay(random);
            try {
                Logger.Info("Stage 2");
            }
            catch (Exception) {
                return false;
            }

            await Task.Delay(random);
            try {
                Logger.Info("Exiting");
            }
            catch (Exception) {
                return false;
            }

            return true;
        }

        private void ChangeDisplayLevel(LogLevel level) {
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
    }
}