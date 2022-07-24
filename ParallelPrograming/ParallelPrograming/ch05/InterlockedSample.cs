using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch05
{
    public class InterlockedSample
    {
        public InterlockedSample()
        {
        }

        public static void UseInterlocked()
        {
            var _countWithoutInterlocked = 0;
            var _countWithInterlocked = 0;
            Parallel.For(1, 1000, i =>
             {
                 Task.Delay(100).Wait();
                 _countWithoutInterlocked++;
              
             });

            Parallel.For(1, 1000, i =>
            {
                Task.Delay(100).Wait();
                Interlocked.Increment(ref _countWithInterlocked);
              
            });
            Console.WriteLine($"Value for counter with Interlocked is {_countWithInterlocked}");
            Console.WriteLine($"Value for counter without Interlocked is {_countWithoutInterlocked}");
        }
    }
}

