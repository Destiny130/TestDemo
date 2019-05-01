using System;
using System.Threading;

namespace TestDemo.FunctionTest.Help
{
    public class LockAnotherClass
    {
        public LockAnotherClass()
        {
            LockMyself lockMyself = new LockMyself();
            //Thread.Sleep(100);
            lock (lockMyself)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} Now I locked LockMyself");
                Timer timer = new Timer((obj) => Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}"), this, 0, 1000);
                Thread.Sleep(10 * 1000);
            }
        }
    }
}
