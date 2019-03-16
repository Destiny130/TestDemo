using System;
using System.Collections.Generic;

namespace TestDemo.FunctionTest
{
    public class ForeachTest
    {
        public void Execute()
        {
            string str = "1,2,3,4,5,6";
            foreach (int i in Read(str))
            {
                Console.Write($"{i}\t");
            }

            Console.WriteLine("\n");
            Console.WriteLine($"String.Join: {String.Join(", ", Read(str))}");
            Console.WriteLine("\n");
        }

        private List<int> Read(string str)
        {
            Console.WriteLine("Invoke me");
            return new List<int>() { 1, 2, 3, 4, 5 };
        }
    }
}
