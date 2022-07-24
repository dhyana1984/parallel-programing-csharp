using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch05
{
    public class SpinLockSample
    {
        static SpinLock _spinLock = new SpinLock();
        static List<int> _itemList = new List<int>();
        public SpinLockSample()
        {
        }

        public static void UseSpinLock()
        {
            var lockTaken = false;
            Parallel.For(1, 5, i =>
            {
                try
                {
                    Console.WriteLine($"Task {Task.CurrentId} waiting for spin lock");
                    _spinLock.Enter(ref lockTaken);
                    _itemList.Add(i);
                    Console.WriteLine($"Task {Task.CurrentId} update list");
                }
                finally
                {
                    if (lockTaken)
                    {
                        _spinLock.Exit(false);
                        Console.WriteLine($"Task {Task.CurrentId} existing update");
                    }
                }
            });
        }
    }
}

