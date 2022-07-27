using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch06
{
    public class ConcurrentDictionarySample
    {
        public ConcurrentDictionarySample()
        {
        }

        public static void UseConcurrentDictionary()
        {
            var concurrentDictionary = new ConcurrentDictionary<int, string>();
            Task producerTask1 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100);
                    concurrentDictionary.TryAdd(i, (i * i).ToString());
                }
            });

            Task producerTask2 = Task.Factory.StartNew(() =>
            {
                for (int i = 10; i < 25; i++)
                {
                    concurrentDictionary.TryAdd(i, (i * i).ToString());
                }
            });

            Task producerTask3 = Task.Factory.StartNew(() =>
            {
                for (int i = 15; i < 20; i++)
                {
                    concurrentDictionary.AddOrUpdate(i, (i * i).ToString(),(key,value)=> (key *key).ToString());
                }
            });

            Task.WaitAll(producerTask1, producerTask2);
            Console.WriteLine($"Keys are {string.Join(",", concurrentDictionary.Keys.Select(c=>c.ToString()).ToArray())}");
        }
    }
}

