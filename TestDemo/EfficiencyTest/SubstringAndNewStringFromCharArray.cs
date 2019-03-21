using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestDemo.EfficiencyTest
{
    public class SubstringAndNewStringFromCharArray
    {
        public void Execute()
        {
            string str = "This is a normal length sentence.";
            Random random = new Random();
            List<int> randomLengthList = new List<int>();
            for (int i = 0; i < str.Length; ++i)
            {
                randomLengthList.Add(random.Next(1, str.Length));
            }
            Stopwatch watch = new Stopwatch();
            string temp = String.Empty;
            int loopCount = 1000000;

            watch.Start();
            for (int i = 0; i < loopCount; ++i)
            {
                randomLengthList.ForEach(r => temp = str.Substring(0, r));
            }
            watch.Stop();
            Console.WriteLine($"Substring spent:\t{watch.ElapsedMilliseconds:F3} milliseconds");

            char[] arr = str.ToCharArray();
            watch.Restart();
            for (int i = 0; i < loopCount; ++i)
            {
                randomLengthList.ForEach(r => temp = new String(arr, 0, r));
            }
            watch.Stop();
            Console.WriteLine($"New string spent:\t{watch.ElapsedMilliseconds:F3} milliseconds");

            Console.WriteLine("New string is a bit faster than Substring\n");
        }
    }
}
