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
            for (int i = 0; i < 5; ++i)
            {
                if (i == 0)
                {
                    task = Test();
                }
                Console.WriteLine($"{i.ToString()} in for loop. \t\t{DateTime.Now:HH:mm:ss}");
                Thread.Sleep(2000);
            }
            await task;
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

            await Task.Delay(17000);
            Console.WriteLine($"Async end. \t\t{DateTime.Now:HH:mm:ss}");
        }
    }
}
