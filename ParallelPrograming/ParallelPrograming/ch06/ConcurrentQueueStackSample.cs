using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch06
{
    public class ConcurrentQueueStackSample
    {
        static object _locker = new object();
        public ConcurrentQueueStackSample()
        {
        }

        public static void UseConcurrentQueue()
        {
            var queue = new Queue<int>();
            var shouldBe = Enumerable.Range(0, 500).Sum();
            for (int i = 0; i < 500; i++)
            {
                queue.Enqueue(i);
            }

            /*** Normal queue ***/
            int sum = 0;
            Stopwatch watch = Stopwatch.StartNew();
            Parallel.For(0, 500, i =>
              {
                  int localSum = 0;
                  int localValue;

                  while (queue.TryDequeue(out localValue))
                  {
                      Thread.Sleep(10);
                      localSum += localValue;
                  }
                  Interlocked.Add(ref sum, localSum);
              });
            watch.Stop();
            Console.WriteLine($"Normal Queue calculated Sum is {sum} and should be {shouldBe}, time cost is {watch.ElapsedMilliseconds}");// 122207 that's not correct

            /*** Normal queue with lock ***/
            for (int i = 0; i < 500; i++)
            {
                queue.Enqueue(i);
            }
            int lockSum = 0;
            watch.Start();
            Parallel.For(0, 500, i =>
            {
                int localSum = 0;
                int localValue;
                lock (_locker)
                {
                    while (queue.TryDequeue(out localValue))
                    {
                        Thread.Sleep(10);

                        localSum += localValue;
                    }
                }
                Interlocked.Add(ref lockSum, localSum);
            });
            watch.Stop();
            Console.WriteLine($"Normal Queue with lock calculated Sum is {lockSum} and should be {shouldBe}, time cost is {watch.ElapsedMilliseconds}");// 124750 but slow

            /*** Concurrency queue ***/
            int cqSum = 0;
            var cq = new ConcurrentQueue<int>();
            for (int i = 0; i < 500; i++)
            {
                cq.Enqueue(i);
            }
            watch.Start();
            Parallel.For(0, 500, i =>
             {
                 int localSum = 0;
                 int localValue;

                 while (cq.TryDequeue(out localValue))
                 {
                     Thread.Sleep(10);
                     localSum += localValue;
                 }
                 Interlocked.Add(ref cqSum, localSum);
             });
            watch.Stop();
            Console.WriteLine($"Concurrency queue calculated Sum is {cqSum} and should be {shouldBe}, time cost is {watch.ElapsedMilliseconds}"); //124750, slow, but no lock
        }

        public static void UseConcurrentStack()
        {
            var concurrentStack = new ConcurrentStack<int>();
            for (int i = 0; i < 500; i++)
            {
                concurrentStack.Push(i);
            }
            concurrentStack.PushRange(new[] { 1, 2, 3, 4, 5 });
            int sum = 0;
            Parallel.For(0, 500, i =>
            {
                int localSum = 0;
                int localValue;
                while (concurrentStack.TryPop(out localValue))
                {
                    Thread.Sleep(10);
                    localSum += localValue;
                }
                Interlocked.Add(ref sum, localSum);
            });
            Console.WriteLine($"outsum = {sum}, should be 124765");
        }
    }
}

