using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Drawing;
using Svg;
using System.Drawing.Imaging;

namespace TestDemo.ThirdPartyLibraryTest
{
    public class SvgToImage
    {
        public void Execute()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"rect", "<svg width=\"100%\" height=\"100%\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\"> <rect x=\"20\" y=\"20\" width=\"250\" height=\"250\" style=\"fill:blue;stroke:pink;stroke-width:5; fill-opacity:0.1;stroke-opacity:0.9\"/> </svg>" },
                {"polyline", "<svg width=\"100%\" height=\"100%\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\"> <polyline points=\"0,0 0,20 20,20 20,40 40,40 40,60\" style=\"fill:white;stroke:red;stroke-width:2\"/> </svg>" },
                { "path", "<svg width=\"100%\" height=\"100%\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\"> <path d=\"M250 150 L150 350 L350 350 Z\" /> </svg>"},
                { "filters_gaussian", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"100%\" height=\"100%\" version=\"1.1\"> <defs> <filter id=\"Gaussian_Blur\"> <feGaussianBlur in=\"SourceGraphic\" stdDeviation=\"3\"/> </filter> </defs> <ellipse cx=\"200\" cy=\"150\" rx=\"70\" ry=\"40\" style=\"fill:#ff0000;stroke:#000000; stroke-width:2;filter:url(#Gaussian_Blur)\"/> </svg>"},
                { "grad_linear", "<svg width=\"100%\" height=\"100%\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\"> <defs> <linearGradient id=\"orange_red\" x1=\"0%\" y1=\"0%\" x2=\"100%\" y2=\"0%\"> <stop offset=\"0%\" style=\"stop-color:rgb(255,255,0); stop-opacity:1\"/> <stop offset=\"100%\" style=\"stop-color:rgb(255,0,0); stop-opacity:1\"/> </linearGradient> </defs> <ellipse cx=\"200\" cy=\"190\" rx=\"85\" ry=\"55\" style=\"fill:url(#orange_red)\"/> </svg>"},
                { "grad_radial", "<svg width=\"100%\" height=\"100%\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\"> <defs> <radialGradient id=\"grey_blue\" cx=\"50%\" cy=\"50%\" r=\"50%\" fx=\"50%\" fy=\"50%\"> <stop offset=\"0%\" style=\"stop-color:rgb(200,200,200); stop-opacity:0\"/> <stop offset=\"100%\" style=\"stop-color:rgb(0,0,255); stop-opacity:1\"/> </radialGradient> </defs> <ellipse cx=\"230\" cy=\"200\" rx=\"110\" ry=\"100\" style=\"fill:url(#grey_blue)\"/> </svg>"},
            };
            Single(dic);
            Console.WriteLine("End\n");
        }

        private void Single(Dictionary<string, string> dic)
        {
            foreach (var pair in dic)
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(pair.Value);
                    SvgDocument svgDoc = SvgDocument.Open(xmlDoc);

                    //var bitmap = svgDoc.Draw();

                    //If image is black, using follow method
                    SizeF size = svgDoc.GetDimensions();
                    Bitmap blank = new Bitmap((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
                    Console.WriteLine($"{pair.Key}, width: {(int)Math.Ceiling(size.Width)}, height: {(int)Math.Ceiling(size.Height)}");
                    Graphics g = Graphics.FromImage(blank);
                    g.Clear(Color.White);
                    g.DrawImage(svgDoc.Draw(), new PointF(0, 0));
                    Bitmap bitmap = new Bitmap(blank);
                    blank.Dispose();

                    bitmap.Save($"C:\\Debug\\test_{pair.Key}.jpg", ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"SvgToImage {pair.Key} exception: {ex.Message}");
                }
            }
        }
    }
}
