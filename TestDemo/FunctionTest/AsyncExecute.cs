using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestDemo.FunctionTest
{
    public class AsyncExecute
    {
        public async void Execute()
        {
            Task task = null;
            Task task2 = null;
            for (int i = 0; i < 5; ++i)
            {
                if (i == 0)
                {
                    task = Task.Run(() => Test());
                    task2 = Task.Run(() => Test2());
                }
                Console.WriteLine($"{i.ToString()} in for loop. \t\t{DateTime.Now:HH:mm:ss}");
                Thread.Sleep(2000);
            }
            await Task.WhenAll(task, task2);
            Console.WriteLine($"Execute end. \t\t{DateTime.Now:HH:mm:ss}");
        }

        async Task Test()
        {
            Console.WriteLine($"Async start. \t\t{DateTime.Now:HH:mm:ss}");
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine($"{i.ToString()} in async for loop.\t{DateTime.Now:HH:mm:ss}");
                Thread.Sleep(2000);
            }

            await Task.Delay(10);
            Console.WriteLine($"Async end. \t\t{DateTime.Now:HH:mm:ss}");
        }

        async Task Test2()
        {
            Console.WriteLine($"Async2 start. \t\t{DateTime.Now:HH:mm:ss}");
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine($"{i.ToString()} in async2 for loop.\t{DateTime.Now:HH:mm:ss}");
                Thread.Sleep(2000);
            }

            await Task.Delay(10);
            Console.WriteLine($"Async2 end. \t\t{DateTime.Now:HH:mm:ss}");
        }
    }
}
