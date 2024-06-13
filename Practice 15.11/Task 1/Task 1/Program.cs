using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_1
{
    internal class Program
    {
        internal static async Task Main()
        {
            int count = 10;
            await PrintAsyncReports(count);
            Console.ReadKey();
        }


        private static void GetThreadReport(string message, int time)
        {
            //Random rnd = new Random();
            //int time = rnd.Next(0, 1000);
            Thread.Sleep(time);
            Thread.CurrentThread.Name = message;
            Console.WriteLine(
                $"Ожидание составило {time} мс, " +
                $"Имя потока: {Thread.CurrentThread.Name};"
                );
        }

        private static async Task PrintAsyncReports(int messagesCount)
        {
            List<Task> tasks = new List<Task>();
            Parallel.For(1, messagesCount, (i, state) =>
            {
                string message = $"{i} поток";
                int time = 100 - i;
                tasks.Add(Task.Run(() => GetThreadReport(message, time)));
            });
            await Task.WhenAll(tasks);
        }

    }
}
