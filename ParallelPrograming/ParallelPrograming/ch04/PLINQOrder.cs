using System;
using System.Linq;

namespace ParallelPrograming.ch04
{
    public class PLINQOrder
    {
        public PLINQOrder()
        {
        }

        public void OrderAndUnOrder()
        {
            var range = Enumerable.Range(1, 10);
            var skipRange = Enumerable.Range(100, 1000);
            var defaultOrdered = range.AsParallel().Select(i => i).ToList();
            var printResult = new Action<int>((i) => Console.Write(i + "-"));

            Console.WriteLine("Default order:");
            defaultOrdered.ForEach(printResult);
            Console.WriteLine(" ");

            var ordered = range.AsParallel().AsOrdered().Select(i => i).ToList();
            Console.WriteLine("Ordered:");
            ordered.ForEach(printResult);
            Console.WriteLine(" ");

            var skipUnOrdered = skipRange.AsParallel().Take(100).Select(i => i).ToList();
            Console.WriteLine("Default ordered skip:");
            skipUnOrdered.ForEach(printResult);
            Console.WriteLine(" ");

            // search the first 100 item by order then handle this 100 items with unorder
            var skipOrdered = skipRange.AsParallel().AsOrdered().Take(100).AsUnordered().Select(i => i).ToList();
            Console.WriteLine("Ordered skip:");
            skipOrdered.ForEach(printResult);
        }
    }
}

