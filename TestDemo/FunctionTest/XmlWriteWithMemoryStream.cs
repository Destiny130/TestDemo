using System;
using System.IO;
using System.Xml;
using TestDemo.Utility;

namespace TestDemo.FunctionTest
{
    public class XmlWriteWithMemoryStream
    {
        const string outputPath = "../../../Output/XmlWriter/";

        public XmlWriteWithMemoryStream()
        {
            UtilityTool.CreateFolder(outputPath);
        }

        public void Execute()
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (XmlWriter writer = XmlWriter.Create(ms))
                    {
                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("Content");
                        writer.WriteStartAttribute("id");
                        writer.WriteValue("iddd");
                        writer.Flush();
                        Console.WriteLine($"Memory usage: {GC.GetTotalMemory(false) / 1024} KB, this MemoryStream used {Math.Round((double)ms.Length, 4)} bytes, default capacity is {ms.Capacity} bytes");
                        Console.WriteLine($"Before re-located, MemoryStream position: {ms.Position}");
                        ms.Seek(7, SeekOrigin.Current);
                        Console.WriteLine($"After re-located, MemoryStream position: {ms.Position}");
                        ms.Position = 0;
                        writer.WriteStartElement("SecondContent");
                        writer.WriteStartAttribute("Name");
                        writer.WriteValue("Nammmme");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        //Flush again
                        writer.Flush();
                        Console.WriteLine($"Memory usage: {GC.GetTotalMemory(false) / 1024} KB, this MemoryStream used {Math.Round((double)ms.Length, 4)} bytes, default capacity is {ms.Capacity} bytes");
                        using (FileStream fs = new FileStream($"{outputPath}test {DateTime.Now:HHmmss}.xml", FileMode.OpenOrCreate))
                        {
                            ms.WriteTo(fs);
                            if (ms.CanWrite)
                            {
                                fs.Flush();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"XmlWriteWithMemoryStream Exception: {ex.Message}");
            }
        }
    }
}
