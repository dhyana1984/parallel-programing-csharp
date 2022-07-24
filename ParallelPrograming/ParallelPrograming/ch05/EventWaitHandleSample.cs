using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch04
{
    public class EventWaitHandleSample
    {
        public EventWaitHandleSample()
        {
        }

        public static void AutResetEventSample()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false); // initial as false to block all thread from start

            Task singallingTask = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    // set singnal as sending status
                    autoResetEvent.Set();
                }
            });

            int sum = 0;
            // start a parallel for loop to update sum
            Parallel.For(1, 10, (i) =>
            {
                Console.WriteLine($"Task with id {Task.CurrentId} waiting for signal to enter");
                // when current task receive the signal, only 1 excute the sum logic and other thread will keep blocking
                // the status will be set as un-sending automatically
                autoResetEvent.WaitOne();
                sum += 1;
                Console.WriteLine($"Task with id {Task.CurrentId} received signal to enter, the result of sum is {sum}");
            });
        }

        public static void ManualResetEventSample()
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false); // initial as false to block all thread from start

            // To simulate to get network off every 2 seconds
            Task signalOffTask = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Network is down");
                    // set the signal as un-sending status
                    manualResetEvent.Reset();
                }
            });

            //To simulate to get network on every 5 seconds
            Task signalOnTask = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    Console.WriteLine("Network is up");
                    // set singnal as sending status
                    manualResetEvent.Set();
                }
            });

            for (int i = 0; i < 3; i++)
            {
                Parallel.For(0, 5, (j) =>
                 {
                     Console.WriteLine($"Task with id {Task.CurrentId} waiting for network to be up");
                     //All the block task will be unblocked here
                     //The status of manualResetEvent will not be set as un-sending untill manualResetEvent.Reset() called
                     //AutoResetEvent.Set() = ManualResetEvent.Set()+ManualResetEvent.Reset()
                     manualResetEvent.WaitOne();
                     Console.WriteLine($"Tasl with id {Task.CurrentId} making server call");
                     Console.WriteLine($"{Task.CurrentId} is calling server...");
                 });
                Thread.Sleep(2000);
            }
        }
    }
}

