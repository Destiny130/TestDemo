using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using TestDemo.Utility;

namespace TestDemo.ThirdPartyLibraryTest
{
    public class HtmlToPdf_HtmlRenderer
    {
        const string outputPath = "../../../Output/HtmlToPdf HtmlRenderer/";

        public HtmlToPdf_HtmlRenderer()
        {
            UtilityTool.CreateFolder(outputPath);
        }

        public void Start()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"demo1", "../../../Input/Htmls for HtmlToPdf/demo1.html" },
                //{"", "" },
                //{"", "" },
                //{"", "" },
            };
            Execute(dic);
            Console.WriteLine("End\n");
        }

        private void Execute(Dictionary<string, string> dic)
        {
            foreach (var pair in dic)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(pair.Value))
                    {
                        byte[] fileContent = PdfConvert(sr.ReadToEnd());
                        File.WriteAllBytes($"{outputPath}test_{pair.Key}.pdf", fileContent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{nameof(HtmlToPdf_HtmlRenderer)} {pair.Key} exception: {ex.Message}");
                }
            }
        }

        private byte[] PdfConvert(string htmlStr)
        {
            byte[] result = null;
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = PdfGenerator.GeneratePdf(htmlStr, PdfSharp.PageSize.A4);
                pdf.Save(ms);
                result = ms.ToArray();
            }
            return result;
        }
    }
}
