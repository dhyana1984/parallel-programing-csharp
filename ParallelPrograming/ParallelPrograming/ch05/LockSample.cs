using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace ParallelPrograming.ch05
{
    public class LockSample
    {
        static object _locker = new object();

        public LockSample()
        {
        }

        public static void UseLock()
        {
            var range = Enumerable.Range(1, 1000);
            if (!File.Exists("locktest-with-lock.txt"))
            {
                File.Create("locktest-with-lock.txt");
            }

            if (!File.Exists("locktest-without-lock.txt"))
            {
                File.Create("locktest-without-lock.txt");
            }

            Stopwatch watch = Stopwatch.StartNew();
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                Thread.Sleep(10);
                lock (_locker)
                {
                    File.WriteAllText("locktest-with-lock.txt", i.ToString());
                }

                // The same as

            /*
                Monitor.Enter(_locker);
                try
                {
                    File.WriteAllText("locktest-with-lock.txt", i.ToString());
                }
                finally
                {
                    Monitor.Exit(_locker);
                }
            */
            });
            watch.Stop();
            Console.WriteLine($"Write data to file with parallel and lock, total time is {watch.ElapsedMilliseconds}");

            watch.Start();
            for (int i = 0; i < 1000; i++)
            {
                Thread.Sleep(10);
                File.WriteAllText("locktest-without-lock.txt", i.ToString());
            }
            watch.Stop();
            Console.WriteLine($"Write data to file witout parallel, total time is {watch.ElapsedMilliseconds}");
        }
    }
}

