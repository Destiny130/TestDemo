using System;
using TestDemo.FunctionTest;

namespace TestDemo
{
    class Execute
    {
        static void Main(string[] args)
        {
            ForeachTest.Execute();
            TupleTest.Execute();


            Console.ReadKey();
        }
    }
}
