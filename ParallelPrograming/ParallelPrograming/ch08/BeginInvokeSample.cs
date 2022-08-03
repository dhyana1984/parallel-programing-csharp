using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch08
{
    public class BeginInvokeSample
    {
        private static void Log(string msg)
        {
            // Simulate save to db
            Thread.Sleep(2000);
            Console.WriteLine($"{msg} log done");
        }

        public static void UseBeginInvoke()
        {
            Console.WriteLine("Start program");
            var logAction = new Action(()=>Log("Log in BeginInvoke"));
            logAction.BeginInvoke(null, null); //Exception throwed: System.PlatformNotSupportedException: Operation is not supported on this platform.
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        public static void UseTaskInsteadOfBeginInvoke()
        {
            Console.WriteLine("Start program");
            var logAction = new Action(()=>Log("Log in Task"));
            Task.Factory.StartNew(logAction);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
    }
}

