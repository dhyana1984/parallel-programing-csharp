using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ParallelPrograming.ch04
{
    public class WaitAllResult
    {
        public WaitAllResult()
        {
        }

        public void getWaitAllResult()
        {
            var watch = Stopwatch.StartNew();

            // Simulate to call API A
            Task<string> TaskA = Task.Factory.StartNew<string>(() =>
            {
                Task.Delay(2000).Wait(); // Simulate the network delay
                return "A";
            });

            // Simulate to call API B
            Task<string> TaskB = Task.Factory.StartNew<string>(() => {
                Task.Delay(3000).Wait(); // Simulate the network delay
                return "B";
            });

            Task.WaitAll(new Task<string>[] { TaskA, TaskB });

            Console.WriteLine($"TaskA result: {TaskA.Result}, TaskB result: {TaskB.Result}, finished in {watch.ElapsedMilliseconds} ms");
            watch.Stop();
        }
    }
}

