using System;
using System.Threading;

namespace TestDemo.FunctionTest.Help
{
    public class LockMyself
    {
        public LockMyself()
        {
            new Thread(() =>
            {
                while (true)
                {
                    lock (this)
                    {
                        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} enter {nameof(LockMyself)} sharing area");
                        Thread.Sleep(3 * 1000);
                    }
                }
            }).Start();
        }
    }
}
