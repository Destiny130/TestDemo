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
            //new AsyncExecute().Execute();
            //new XmlWriteWithMemoryStream().Execute();
            //new ThreadTest().Start();
            new StreamTest().Start();

            #endregion

            #region Efficiency Test

            //new ToCharArrayAndElementAt().Execute();
            //new SubstringAndNewStringFromCharArray().Execute();
            //new ContainsAndIndexOf().Execute();
            //new BitwiseSumAndSystemSum().Execute();

            #endregion

            #region Third Party Library Test

            //new SvgToImage_Svg().Start();

            #endregion

            Console.ReadKey();
        }
    }
}
