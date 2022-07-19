using System;
using ParallelPrograming.ch02;
using ParallelPrograming.ch03;

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
            ParallelLoop.ParallelSumTask();
        }
    }
}
