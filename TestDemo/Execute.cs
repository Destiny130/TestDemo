using System;
using TestDemo.FunctionTest;
using TestDemo.EfficiencyTest;
using TestDemo.ThirdPartyLibraryTest;

namespace TestDemo
{
    class Execute
    {
        static void Main(string[] args)
        {
            //new ForeachTest().Execute();
            //new TupleTest().Execute();
            //new ToCharArrayAndElementAt().Execute();
            //new SvgToImage_Svg().Start();
            new HtmlToPdf_HtmlRenderer().Start();

            Console.ReadKey();
        }
    }
}
