using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch07
{
    public class ThreadStaticSample
    {
        [ThreadStatic] //ThreadStatic always used to identity the local thread variable
        static int counter = 1;

        // ThreadLocal is the replacement of ThreadStatic
        static ThreadLocal<int> threadLocalCounter = new ThreadLocal<int>(() => 1);

        public static void TestThreadStaticVariable()
        {
            for (int i = 0; i < 10; i++)
            {
                // The problem is that only 1 task will assign the initial value 1 to counter, other task will assign default int value 0 to counter
                Task.Factory.StartNew(() => Console.WriteLine(counter));
            }
            Console.ReadLine();
        }

        public static void TestThreadLocal()
        {
            for (int i = 0; i < 10; i++)
            {
                // No initial value problem
                Task.Factory.StartNew(() => Console.WriteLine(threadLocalCounter.Value));
            }
            Console.ReadLine();
        }
    }
}

