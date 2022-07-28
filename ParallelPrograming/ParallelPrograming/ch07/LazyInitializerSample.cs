using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch07
{
    public class LazyInitializerSample
    {
        static Data _data;
        static bool _initiallized;
        static object _locker = new object();
        public static void UseLazyInitializer()
        {
            Parallel.For(0, 10, i =>
            {
                Console.WriteLine($"Task with id {Task.CurrentId}");
                LazyInitializer.EnsureInitialized(ref _data, ref _initiallized, ref _locker, FetchData);
            });

            Console.ReadLine();
        }

        private static Data FetchData()
        {
            Console.WriteLine($"Task with id {Task.CurrentId} is Initializing data");
            return new Data();
        }
    }
}

