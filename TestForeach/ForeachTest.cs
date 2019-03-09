using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForeach
{
    class ForeachTest
    {
        static void Main(string[] args)
        {
            string str = "1,2,3,4,5,6";
            foreach (int i in Read(str))
                Console.Write($"{i}\t");
            Console.ReadKey();
        }

        static List<int> Read(string str)
        {
            Console.Write("Invoke me\n");
            return new List<int>() { 1, 2, 3, 4, 5 };
        }
    }
}
