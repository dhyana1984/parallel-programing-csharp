using System;
using System.Threading.Tasks;

namespace ParallelPrograming.ch09
{
    public class ConfigureAwaitSample
    {
        private static async Task DelayAsync()
        {
            await Task.Delay(2000);
        }

        public static async void DeadLock()
        {
            // Here will be a deadloack in GUI or ASP.Net Application because SynchronizationContext is dpendency by both
            // But it will not be dead lock in Console Application as it's using thread pool instead of SynchronizationContext
            //var task = DelayAsync();

            //Use ConfigureAwait to fix above problem
            var task = DelayAsync().ConfigureAwait(false);
            await task;
        }
    }
}

