using System;
using System.Threading;

namespace TestDemo.FunctionTest
{
    public class StringInterningTest
    {
        const string lockedStr = "abcd";

        public void Execute()
        {
            new Thread(() =>
            {
                Console.WriteLine("Lock thread start");
                lock (lockedStr)
                {
                    Thread.Sleep(3 * 1000);
                }
                Console.WriteLine($"After lock. Thread Id: {Thread.CurrentThread.ManagedThreadId.ToString()}");
            }).Start();

            Thread.Sleep(1 * 1000);
            string abc = "abcd";
            lock (abc)
            {
                Console.WriteLine($"After main lock. Thread Id: {Thread.CurrentThread.ManagedThreadId.ToString()}");
            }
        }
    }
}
