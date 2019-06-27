using System;
using System.Threading;

namespace TestDemo.FunctionTest
{
    public class TryCatchTest
    {
        public void Execute()
        {
            Test left = new Test(1, "1");
            Test right = new Test(2, "2");
            Console.WriteLine($"Thread.Id: {Thread.CurrentThread.ManagedThreadId}");

            //try
            //{
            //    Console.WriteLine($"left == right: {left == right}. Thread.Id: {Thread.CurrentThread.ManagedThreadId}");
            //}
            //catch (Exception ex)
            //{
            //    //If override method throw StackOverFlow exception, program will not go here.
            //    Console.WriteLine($"Compare exception: {ex.Message}. Thread.Id: {Thread.CurrentThread.ManagedThreadId}");
            //}

            //List<Test> overflowList = new List<Test>();
            //try
            //{
            //    for (int i = 0; i < Int32.MaxValue; ++i)
            //    {
            //        overflowList.Add(new Test(i, i.ToString()));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Create instance exception: {ex.Message}. Thread.Id: {Thread.CurrentThread.ManagedThreadId}");  //Out of memory, spent about 200M
            //}

            //Test nullInstance = null;
            //try
            //{
            //    int val = nullInstance.Id;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Null point exception: {ex.Message}. Thread.Id: {Thread.CurrentThread.ManagedThreadId}");  //Output normally
            //}

            int a = Int32.MaxValue, b = 0;
            try
            {
                int sum = Sum(a, b);
            }
            catch (Exception ex)
            {
                //If override method throw StackOverFlow exception, program will not go here.
                Console.WriteLine($"Sum overflow exception: {ex.Message}. Thread.Id: {Thread.CurrentThread.ManagedThreadId}");
            }
        }

        int Sum(int a, int b)
        {
            return a == 0 ? b : Sum(a - 1, b + 1);
        }
    }
}
