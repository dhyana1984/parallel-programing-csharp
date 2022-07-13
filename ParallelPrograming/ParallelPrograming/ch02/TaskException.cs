using System;
using System.Threading.Tasks;

namespace ParallelPrograming.ch02
{
    public class TaskException
    {
        public TaskException()
        {
        }

        public static void HandleMultipleExceptionsInTasks()
        {
            var TaskA = Task.Factory.StartNew(() => throw new DivideByZeroException());
            var TaskB = Task.Factory.StartNew(() => throw new ArithmeticException());
            var TaskC = Task.Factory.StartNew(() => throw new NullReferenceException());

            try
            {
                Task.WaitAll(TaskA, TaskB, TaskC);
            }
            catch (AggregateException ex)
            {
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine(innerException.Message);
                }
            }
        }

        public static void HandlerMultipleExceptionsInCallback()
        {
            var TaskA = Task.Factory.StartNew(() => throw new DivideByZeroException());
            var TaskB = Task.Factory.StartNew(() => throw new ArithmeticException());
            var TaskC = Task.Factory.StartNew(() => throw new NullReferenceException());

            try
            {
                Task.WaitAll(TaskA, TaskB, TaskC);
            }
            catch (AggregateException ex)
            {
                // no need to loop on AggregateException
                ex.Handle(innerException => {
                    Console.WriteLine(innerException.Message);
                    // return true to tell the exception was correctly handled
                    return true;
                });
            }
        }
    }
}
