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
                    // Send signal every 1 second, then 1 thread will unblock
                    autoResetEvent.Set();
                }
            });

            int sum = 0;
            // start a parallel for loop to update sum
            Parallel.For(1, 10, (i) =>
            {
                Console.WriteLine($"Task with id {Task.CurrentId} waiting for signal to enter");
                //when current task receive the signal, then excute the sum logic and other thread will keep blocking
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
                    // set the manualResetEvent as initial false to simulate disconnect network
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
                    manualResetEvent.Set();
                }
            });

            for (int i = 0; i < 3; i++)
            {
                Parallel.For(0, 5, (j) =>
                 {
                     Console.WriteLine($"Task with id {Task.CurrentId} waiting for network to be up");
                     //All the block thread will be unblocked here
                     manualResetEvent.WaitOne();
                     Console.WriteLine($"Tasl with id {Task.CurrentId} making server call");
                     Console.WriteLine($"{Task.CurrentId} is calling server...");
                 });
                Thread.Sleep(2000);
            }
        }
    }
}

