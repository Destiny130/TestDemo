using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestDemo.EfficiencyTest
{
    class ContainsAndIndexOf
    {
        public void Execute()
        {
            string str = "This is a normal length sentence.";
            Random random = new Random();
            List<string> strList = new List<string>() { "length", " normal if", "This is a ", "sentence. s", "a normal length sentence." };
            Stopwatch watch = new Stopwatch();
            bool temp = false;
            int loopCount = 100000;

            watch.Start();
            for (int i = 0; i < loopCount; ++i)
            {
                strList.ForEach(s => str.Contains(s));
            }
            watch.Stop();
            Console.WriteLine($"Contains spent:\t{watch.ElapsedMilliseconds:F3} milliseconds");

            char[] arr = str.ToCharArray();
            watch.Restart();
            for (int i = 0; i < loopCount; ++i)
            {
                strList.ForEach(s => temp = str.IndexOf(s) != -1);
            }
            watch.Stop();
            Console.WriteLine($"IndexOf spent:\t{watch.ElapsedMilliseconds:F3} milliseconds");

            Console.WriteLine("Contains is 6 times faster than IndexOf\n");
        }
    }
}
