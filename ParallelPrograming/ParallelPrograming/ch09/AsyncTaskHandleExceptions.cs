using System;
using System.Threading.Tasks;

namespace ParallelPrograming.ch09
{
    public class AsyncTaskHandleExceptions
    {
        private static Task DoSomethingFaulty()
        {
            Task.Delay(2000);
            throw new Exception("This is custom exception");
        }

        public async static Task AsyncReturningTaskExample()
        {
            var task = DoSomethingFaulty(); // status of task is Faulted without exception thrown
            Console.WriteLine("This should not execute");
            try
            {
                task.ContinueWith(s =>
                {
                    Console.WriteLine(s);
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public async static Task FixedAsyncReturningTaskExample()
        {
            var task = DoSomethingFaulty(); // status of task is Faulted without exception thrown
            Console.WriteLine("This should not execute");
        }

        public async static Task CallAsyncWithoutAwaitFromInsideTryCatch()
        {
            try
            {
                var task = DoSomethingFaulty();
                Console.WriteLine("This should not execute");
                task.ContinueWith(s =>
                {
                    Console.WriteLine(s);
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}

