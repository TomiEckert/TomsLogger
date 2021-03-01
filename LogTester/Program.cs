using System;
using System.Threading.Tasks;
using TomsLogger;

namespace LogTester {
    internal class Program {
        public static void Main(string[] args) {
            new Program().AsyncMain().Wait();
        }

        private async Task AsyncMain() {
            var r = new Random();
            var tasks = new Task[250];
            
            for (var i = 0; i < 250; i++) {
                tasks[i] = DoStuff(i, r.Next(250, 1000));
            }

            await Task.Delay(250);
            Task.WaitAll(tasks);
            Logger.Info("Tasks finished");
            Console.ReadLine();
        }

        private async Task DoStuff(int i, int pause) {
            Logger.Info(i + "::Started");
            await Task.Delay(pause);
            Logger.Info(i + "::1st pause");
            await Task.Delay(pause);
            Logger.Info(i + "::2nd pause");
            await Task.Delay(pause);
            Logger.Info(i + "::3nd pause");
            await Task.Delay(pause);
            Logger.Info(i + "::exiting");
        }
    }
}