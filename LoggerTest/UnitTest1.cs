using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TomsLogger;
using TomsLogger.Config;
using TomsLogger.Model;

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
            _result = new List<string>();

            var config = LoggerConfigBuilder.Default
                                            .SetSaveToFile(false)
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

        private void SaveLog(string entry) {
            lock (_lock) {
                _result.Add(entry);
            }
        }
    }
}