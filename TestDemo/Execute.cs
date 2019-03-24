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
            #region Function Test

            //new ForeachTest().Execute();
            //new TupleTest().Execute();
            //new GetHashCodeTest().Execute();

            #endregion

            #region Efficiency Test

            //new ToCharArrayAndElementAt().Execute();
            //new SubstringAndNewStringFromCharArray().Execute();
            //new ContainsAndIndexOf().Execute();

            #endregion

            #region Third Party Library Test

            //new SvgToImage().Execute();

            #endregion

            Console.ReadKey();
        }
    }
}
