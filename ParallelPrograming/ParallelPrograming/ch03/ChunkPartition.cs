using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelPrograming.ch03
{
    public class ChunkPartition
    {
        public ChunkPartition()
        {
        }

        public static void ChunkPartitionHandler()
        {
            var source = Enumerable.Range(1, 100).ToList();
            OrderablePartitioner<Tuple<int, int>> orderablePartitioner = Partitioner.Create(1, 100);
            Parallel.ForEach(orderablePartitioner, (range, state) =>
            {
                var startRange = range.Item1;
                var endRange = range.Item2;

                Console.WriteLine($"Range execution finished on task {Task.CurrentId} with range {startRange}-{endRange}");
            });

            Console.ReadLine();
        }
    }
}

