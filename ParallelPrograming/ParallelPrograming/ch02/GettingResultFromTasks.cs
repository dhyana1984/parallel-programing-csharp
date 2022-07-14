using System;
using System.Threading.Tasks;

namespace ParallelPrograming.ch02
{
    public class GettingResultFromTasks
    {
        public GettingResultFromTasks()
        {
        }

        public static void GetResultsFromTasks()
        {
            // Create and run task by new Task<>
            var sumTaskViaTaskOfInt = new Task<int>(() => Sum(5));
            sumTaskViaTaskOfInt.Start();
            Console.WriteLine($"Result from sumTaskViaTaskOfInt is {sumTaskViaTaskOfInt.Result}");

            // Create and run task by Factory.StarNew<>
            var sumTaskFactory = Task.Factory.StartNew<int>(() => Sum(5));
            Console.WriteLine($"Result from sumTaskFactory is {sumTaskFactory.Result}");

            // Create and run task by Tasl.FromResult<>
            // The parameter for FromResult is not a callback
            var sumTaskViaTaskResult = Task.FromResult<int>(Sum(5));
            Console.WriteLine($"Result from sumTaskViaTaskResult is {sumTaskViaTaskResult.Result}");

            Console.ReadLine();
        }

        private static int Sum(int n)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += i;
            }
            return sum;
        }
    }
}
