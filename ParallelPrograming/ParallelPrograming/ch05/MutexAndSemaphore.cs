using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch05
{
    public class MutexAndSemaphore
    {
        private static Mutex mutex = new Mutex();
        public MutexAndSemaphore()
        {
        }

        public static void UseMutex()
        {
            var range = Enumerable.Range(1, 1000);
            if (!File.Exists("locktest-with-mutex.txt"))
            {
                File.Create("locktest-with-mutex.txt");
            }

            Stopwatch watch = Stopwatch.StartNew();
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                Thread.Sleep(10);
                //Here is the WaitHandle.WaitOne(), to lock the key section
                mutex.WaitOne();
                File.AppendAllText("locktest-with-mutex.txt", i.ToString());
                //Release mutex
                mutex.ReleaseMutex();
            });
            watch.Stop();
            Console.WriteLine($"Write data to file with parallel and mutex, total time is {watch.ElapsedMilliseconds}");
        }

        public static void UseSemaphore()
        {
            var range = Enumerable.Range(1, 1000);
            Action DummyService = new Action(() => Thread.Sleep(1000));
            Semaphore semaphore = new Semaphore(2, 3); // initial count is 2 and max number of count is 3
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                // Block thread until get recieve a semaphore
                semaphore.WaitOne();
                Console.WriteLine($"Index {i} make service call using Task {Task.CurrentId}");
                DummyService();
                Console.WriteLine($"Index {i} releasing semaphore using Task {Task.CurrentId}");
                semaphore.Release();
            });
        }
    }
}

