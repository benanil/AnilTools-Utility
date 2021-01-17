using System;
using System.Diagnostics;

namespace AnilTools
{
    public static class StopWatchTester
    {
        public static void Test(string aName, Action a, string bName, Action b, long iteration)
        {
            Stopwatch stopwatch = new Stopwatch();

            Debug2.Log(aName);

            stopwatch.Start();

            for (long i = 0; i < iteration; i++)
            {
                a.Invoke();
            }

            stopwatch.Stop();
            Debug2.Log($" {stopwatch.ElapsedMilliseconds: 0.000}");

            stopwatch.Reset();
            Debug2.Log(bName);

            for (long i = 0; i < iteration; i++)
            {
                b.Invoke();
            }

            stopwatch.Stop();
            Debug2.Log($" {stopwatch.ElapsedMilliseconds: 0.000}");
        }
    }
}