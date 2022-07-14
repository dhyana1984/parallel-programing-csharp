using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelPrograming.ch02
{
    public class ContinueWith
    {
        public ContinueWith()
        {
        }

        public async static void ContinueWhenAll()
        {
            int a = 2, b = 3;
            Task<int> taskA = Task.Factory.StartNew<int>(() => a * a);
            Task<int> taskB = Task.Factory.StartNew<int>(() => b * b);
            Task<int> taskC = Task.Factory.StartNew<int>(() => 2 * a * b);

            Func<Task, int> getResultFromTaskOfInt = (Task task) => (task as Task<int>).Result;  
            var sum = await Task.Factory.ContinueWhenAll<int>(new Task[] { taskA, taskB, taskC }, (tasks) =>tasks.Sum(getResultFromTaskOfInt));

            Console.WriteLine(sum);
        }
    }
}
