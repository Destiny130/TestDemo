using System;
using TestDemo.FunctionTest;
using TestDemo.EfficiencyTest;

namespace TestDemo
{
    class Execute
    {
        static void Main(string[] args)
        {
            //new ForeachTest().Execute();
            //new TupleTest().Execute();
            new ToCharArrayAndElementAt().Execute();

            Console.ReadKey();
        }
    }
}
