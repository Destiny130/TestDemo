using System;
using TestDemo.FunctionTest;
using TestDemo.EfficiencyTest;
using TestDemo.ThirdPartyLibraryTest;
using System.Windows.Forms;

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
            //Application.Run(new AsyncForm());
            new AsyncExecute().Execute();

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
