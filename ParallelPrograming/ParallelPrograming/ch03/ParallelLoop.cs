using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch03
{
    public class ParallelLoop
    {
        public ParallelLoop()
        {
        }

        public static void ParallelInvokeMethod()
        {
            try
            {
                Parallel.Invoke(
                    () => Console.WriteLine("Action1"),
                    new Action(() => Console.WriteLine("Action2")),
                           () => Console.WriteLine("Action3"));
            }
            catch(AggregateException aggregateException)
            {
                foreach (var ex in aggregateException.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Unblocked");
            Console.ReadLine();
        }

        public static void ParallelForMethod()
        {
            Parallel.For(0, 100, (i) => Console.WriteLine(i));
        }

        public static void ParallelForEachMethod()
        {
            List<string> urls = new List<string>() { "www.bing.com", "www.sina.com", "www.baidu.com" };
            Parallel.ForEach(urls, url =>
            {
                Ping pinger = new Ping();
                Console.WriteLine($"Ping Url {url} status is {pinger.Send(url).Status} by Task {Task.CurrentId}");
            });
        }

        public static void ParallelDegreeOfParallelism()
        {
            // Default MaxDegreeOfParallelism value is 64
            Parallel.For(
                1,
                20,
                new ParallelOptions { MaxDegreeOfParallelism = 2 }, // it's in static method, need to rebuild or run other method to change this variable
                index =>
             {
                 Console.WriteLine($"Index {index} excuting on Task Id {Task.CurrentId}");
             });
        }

        public static void ParallelBreak()
        {
            var numbers = Enumerable.Range(1, 1000);
            Parallel.ForEach(numbers, (i, parallelLoopState) =>
            {
                //To reduce LowestBreakIteration to make better performance
                Console.WriteLine($"For i={i} LowestBreakIteration = {parallelLoopState.LowestBreakIteration} and Task id = {Task.CurrentId}");
                if(i >= 10)
                {
                    parallelLoopState.Break();
                }
            });
        }

        public static void ParallelStop()
        {
            var numbers = Enumerable.Range(1, 1000);
            Parallel.ForEach(
                numbers,
                (i, parallelLoopState) =>
                {
                    Console.Write(i+" ");
                    if (i % 4 == 0)
                    {
                        Console.WriteLine($"Loop Stopped on {i}");
                        parallelLoopState.Stop();
                    }
                });
        }

        public static void ParallelSumTask()
        {
            var numbers = Enumerable.Range(1, 60);
            long sumOfNumbers = 0;
            Action<long> taskFinishedMethod = (taskResult) =>
            {
                Console.WriteLine($"Sum at the end of all task iterations for task {Task.CurrentId} is {taskResult}");
                Interlocked.Add(ref sumOfNumbers, taskResult);
            };

            Parallel.For(
                0,
                numbers.Count(),
                ()=>0,// seed for thread local variable
                (j, loop, subtotal) =>
                {
                    subtotal += j;
                    return subtotal;
                },
                taskFinishedMethod
                );
            Console.WriteLine($"The total of 60 numbers is {sumOfNumbers}");
        }
    }
}

