using System;
using System.Threading;

namespace ParallelPrograming.ch07
{
    public class Data
    {

    }

    public class DataWrapper
    {
        public DataWrapper()
        {
            CachedData = GetDataFromDatabase();
            Console.WriteLine("Object initialied");
        }

        public Data CachedData { get; set; }
        private Data GetDataFromDatabase()
        {
            // simulate delay
            Thread.Sleep(1000);
            return new Data();
        }
    }

    public class LazySample
    {
        public LazySample()
        {
        }

        public static void UseLazyByConstructor()
        {
            Console.WriteLine("Creating Lazy object");
            // lazy call DataWrapper constructor to get data
            Lazy<DataWrapper> lazyWrapper = new Lazy<DataWrapper>();
            Console.WriteLine("Lazy Object created");
            Console.WriteLine("Now we want to access data");
            var data = lazyWrapper.Value.CachedData;
            Console.WriteLine("Finising up");
        }

        public static void UseLazyByDelegate()
        {

            Console.WriteLine("Creating Lazy object");
            // Lazy call the method to get data
            Lazy<Data> lazyDataWrapper = new Lazy<Data>(GetDataFromDatabase);
            Console.WriteLine("Lazy Object created");
            Console.WriteLine("Now we want to access data");
            var data = lazyDataWrapper.Value;
            Console.WriteLine("Finising up");
        }

        private static Data GetDataFromDatabase()
        {
            Console.WriteLine("Fetching Data");
            // simulate delay
            Thread.Sleep(1000);
            return new Data();
        }

    }
}

