using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPrograming.ch02
{
    public class TaskCancellation
    {
        public TaskCancellation()
        {
        }

        public static void CancelTaskViaPoll()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            var sumTaskViaTaskOfInt = new Task(() => LongRunningSum(token), token);
            sumTaskViaTaskOfInt.Start();

            // Wait user to enter key to cancel the long running task
            Console.ReadLine();
            cancellationTokenSource.Cancel();
        }

        private static void LongRunningSum(CancellationToken token)
        {
            for (int i = 0; i < 1000; i++)
            {
                // Sumulate long running
                Task.Delay(100);
                Console.WriteLine(i);
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
            }
        }


        public static void CancelTaskViaCallback()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            DownloadWithToken(token);
            // emulate do something to trigger the cancel...
            Task.Delay(2000);
            cancellationTokenSource.Cancel();
            Console.ReadLine();
        }

        private static void DownloadWithToken(CancellationToken token)
        {
            WebClient webClient = new WebClient();

            //register callback delegate
            //cancel will be triggered when user call token.Cancel()
            token.Register(webClient.CancelAsync);
            webClient.DownloadStringAsync(new Uri("https://weibo.com"));
            webClient.DownloadStringCompleted += (sender, e) =>
            {
                // make the downloading duration long than the task to trigger cancel
                Task.Delay(3000);
                if (!e.Cancelled)
                {
                    Console.WriteLine("Download Completed.");
                }
                else
                {
                    Console.WriteLine("Download Cancelled.");
                }
            };

        }

    }
}
