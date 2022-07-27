using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch06
{
    public class BlockCollectionSample
    {
        public BlockCollectionSample()
        {
        }

        public static void UseBlockingCollection()
        {
            var blockingCollection = new BlockingCollection<int>(5);
            Task produceTask = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    blockingCollection.Add(i);
                }

                //When producer called CompleteAdding, comsumer will call IsCompleted to check if all item handled.
                blockingCollection.CompleteAdding();
            });

            Task consumerTask = Task.Factory.StartNew(() =>{
                while (!blockingCollection.IsCompleted)
                {
                    int item = blockingCollection.Take();
                    Console.WriteLine($"Item retrieved is {item}");
                }
            });

            Task.WaitAll(produceTask, consumerTask);

        }

        public static void UseMultipleBlockingCollection()
        {
            var blockingCollections = new BlockingCollection<int>[2];
            blockingCollections[0] = new BlockingCollection<int>(5);
            blockingCollections[1] = new BlockingCollection<int>(5);

            //producer1
            Task producerTask1 = Task.Factory.StartNew(() =>
            {
                for (int i = 1; i <= 5; i++)
                {
                    blockingCollections[0].Add(i);
                    Thread.Sleep(100);
                }
                blockingCollections[0].CompleteAdding();
            });

            //producer2
            Task producerTask2 = Task.Factory.StartNew(() =>
            {
                for (int i = 6; i <= 10; i++)
                {
                    blockingCollections[1].Add(i);
                    Thread.Sleep(100);
                }
                blockingCollections[1].CompleteAdding();
            });

            while (!blockingCollections[0].IsCompleted || !blockingCollections[1].IsCompleted)
            {
                int item;
                // Take item from producer1 or producer2, wait time is 1s
                BlockingCollection<int>.TryTakeFromAny(blockingCollections, out item, TimeSpan.FromSeconds(1));
                if(item!= default(int))
                {
                    Console.WriteLine($"Item fetched is {item}");
                }
            }

        }
    }
}

