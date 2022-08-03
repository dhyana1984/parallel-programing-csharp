using System;
using ParallelPrograming.ch02;
using ParallelPrograming.ch03;
using ParallelPrograming.ch04;
using ParallelPrograming.ch05;
using ParallelPrograming.ch06;
using ParallelPrograming.ch07;
using ParallelPrograming.ch08;
using ParallelPrograming.ch09;

namespace ParallelPrograming
{
    class Program
    {
        static  void Main(string[] args)
        {
            /*** ch02 ***/
            //GettingResultFromTasks.GetResultsFromTasks();
            //TaskCancellation.CancelTaskViaPoll();
            //TaskCancellation.CancelTaskViaCallback();
            //TaskException.HandleMultipleExceptionsInTasks();
            //TaskException.HandlerMultipleExceptionsInCallback();
            //ContinueWith.ContinueWhenAll();


            /*** ch03 ***/
            //ChunkPartition.ChunkPartitionHandler();
            //ParallelLoop.ParallelInvokeMethod();
            //ParallelLoop.ParallelForMethod();
            //ParallelLoop.ParallelForEachMethod();
            //ParallelLoop.ParallelDegreeOfParallelism();
            //ParallelLoop.ParallelBreak();
            //ParallelLoop.ParallelStop();
            //ParallelLoop.ParallelSumTask();

            /*** ch04 ***/
            //PLINQOrder plinqOrder = new PLINQOrder();
            //WaitAllResult waitAllResult = new WaitAllResult();
            //plinqOrder.OrderAndUnOrder();
            //waitAllResult.getWaitAllResult();

            /**** ch05 ****/
            //EventWaitHandleSample.AutResetEventSample();
            //EventWaitHandleSample.ManualResetEventSample();
            //InterlockedSample.UseInterlocked();
            //LockSample.UseLock();
            //MutexAndSemaphore.UseMutex();
            //MutexAndSemaphore.UseSemaphore();
            //SpinLockSample.UseSpinLock();

            /**** ch06 ****/
            //ConcurrentQueueStackSample.UseConcurrentQueue();
            //ConcurrentQueueStackSample.UseConcurrentStack();
            //ConcurrentBagSample.UseConcurrentBag();
            //BlockCollectionSample.UseBlockingCollection();
            //BlockCollectionSample.UseMultipleBlockingCollection();
            //ConcurrentDictionarySample.UseConcurrentDictionary();

            /**** ch07 ****/
            //LazySample.UseLazyByConstructor();
            //LazySample.UseLazyByDelegate();
            //LazyException.CallLazyWithException();
            //ThreadStaticSample.TestThreadStaticVariable();
            //ThreadStaticSample.TestThreadLocal();
            //LazyInitializerSample.UseLazyInitializer();

            /**** ch08 ****/
            //BeginInvokeSample.UseBeginInvoke();// Exception throwed: System.PlatformNotSupportedException: Operation is not supported on this platform.
            //BeginInvokeSample.UseTaskInsteadOfBeginInvoke();

            /**** ch09 ****/

            //Scenario1, without try catch or await
            //var task = AsyncTaskHandleExceptions.AsyncReturningTaskExample();// status of task is Faulted without exception thrown

            //Scenario1 fixed, with IsFaulted and without await
            //var task = AsyncTaskHandleExceptions.FixedAsyncReturningTaskExample();
            //if (task.IsFaulted) // should check IsFaulted here
            //{
            //    Console.WriteLine(task.Exception.Flatten().Message.ToString());
            //}

            //Scenario2, with try catch inside and without await
            //Best practise is wrap task inside try catch
            //var task = AsyncTaskHandleExceptions.CallAsyncWithoutAwaitFromInsideTryCatch();

            //Scenario3, use await outside of try catch, the same as Scenario1

            //Scenario4, return avoid
            AsyncTaskHandleExceptions.CallAsyncWithoutAwaitReturnAvoid();


            Console.WriteLine("In main method after calling method");
            Console.ReadLine();
        }
    }
}
