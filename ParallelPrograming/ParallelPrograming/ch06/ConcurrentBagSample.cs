using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch06
{
    public class ConcurrentBagSample
    {
        static ConcurrentBag<int> concurrentBag = new ConcurrentBag<int>();
        public ConcurrentBagSample()
        {
        }

        public static void UseConcurrentBag()
        {

            ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);
            Task produceAndConsumerTask = Task.Factory.StartNew(() =>
            {
                for (int i = 1; i <= 3; ++i)
                {
                    // 1,2,3 added to produceAndConsumerTask local task queue
                    concurrentBag.Add(i);
                }

                manualResetEvent.Wait();
                while (concurrentBag.IsEmpty == false)
                {
                    int item;
                    if (concurrentBag.TryTake(out item))
                    {
                        Console.WriteLine($"Item is {item}");
                    }
                }
            });

            Task produceTask = Task.Factory.StartNew(() =>
            {
                for (int i = 4; i <= 6; ++i)
                {
                    //4,5,6 added to produceTask local task queue
                    concurrentBag.Add(i);
                }
                manualResetEvent.Set();
            });

            Task.WaitAll(produceAndConsumerTask, produceTask);
        }
    }
}

