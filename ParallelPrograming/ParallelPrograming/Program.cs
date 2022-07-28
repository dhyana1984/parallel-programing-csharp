using System;
using ParallelPrograming.ch02;
using ParallelPrograming.ch03;
using ParallelPrograming.ch04;
using ParallelPrograming.ch05;
using ParallelPrograming.ch06;
using ParallelPrograming.ch07;

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
            LazyInitializerSample.UseLazyInitializer();
        }
    }
}
