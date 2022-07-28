using System;
namespace ParallelPrograming.ch07
{
    public class LazyException
    {
        static int counter = 0;
        public Data CachedData { get; set; }
        static Data GetDataFromDatabase()
        {
            if (counter == 0)
            {
                Console.WriteLine("Throw exception");
                throw new Exception("Some Error has occurred");
            }
            else
            {
                return new Data();
            }
        }

        public static void CallLazyWithException()
        {
            Console.WriteLine("Creatint Lazy object");
            // if we pass System.Threading.LazyThreadSafetyMode.PublicationOnly here, Value will not cached
            Lazy<Data> lazyDataWrapper = new Lazy<Data>(GetDataFromDatabase, System.Threading.LazyThreadSafetyMode.None);
            Console.WriteLine("Lazy object created");
            Console.WriteLine("Now we want to access data");
            Data data = null;
            try
            {
                data = lazyDataWrapper.Value;
                Console.WriteLine("Data Fetched on attempt 1");
            }
            catch(Exception)
            {
                Console.WriteLine("Exception 1");
            }
            try
            {
                counter++;
                data = lazyDataWrapper.Value; // error will be thrown here as value was cached, counter still equal to 1
                Console.WriteLine("Data Fetched on attempt 2");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception 2");
            }
            Console.WriteLine("Finishing up");
        }
    }
}

