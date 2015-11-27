using System;
using System.Diagnostics;
using System.Threading;

namespace Blog.FitNesse.Tests
{
    internal class Helpers
    {
        public static void WaitUntil(Func<bool> func, int maxTimeInSeconds)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (!func())
            {
                if (stopwatch.ElapsedMilliseconds > maxTimeInSeconds * 1000)
                {
                    throw new TimeoutException(string.Format("Не удалось выполнить операцию за {0} секунд", maxTimeInSeconds));
                }
                Thread.Sleep(100);
            }
        }
    }
}
