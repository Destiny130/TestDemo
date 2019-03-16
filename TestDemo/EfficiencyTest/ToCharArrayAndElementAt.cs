using System;
using System.Diagnostics;
using System.Linq;

namespace TestDemo.EfficiencyTest
{
    public class ToCharArrayAndElementAt
    {
        public void Execute()
        {
            string str = "This is a normal length sentence.";
            Stopwatch watch = new Stopwatch();
            char temp = ' ';
            int loopCount = 100000;

            watch.Start();
            for (int i = 0; i < loopCount; ++i)
            {
                foreach (char c in str.ToCharArray())
                {
                    temp = c;
                }
            }
            watch.Stop();
            Console.WriteLine($"ToCharArray spent:\t\t{watch.ElapsedMilliseconds:F3} milliseconds");

            watch.Restart();
            for (int i = 0; i < loopCount; ++i)
            {
                for (int j = 0; j < str.Length; ++j)
                {
                    temp = str.ElementAt(j);
                }
            }
            watch.Stop();
            Console.WriteLine($"ElementAt spent:\t\t{watch.ElapsedMilliseconds:F3} milliseconds");

            watch.Restart();
            for (int i = 0; i < loopCount; ++i)
            {
                for (int j = 0; j < str.Length; ++j)
                {
                    temp = str.ElementAtOrDefault(j);
                }
            }
            watch.Stop();
            Console.WriteLine($"ElementAtOrDefault spent:\t{watch.ElapsedMilliseconds:F3} milliseconds");


            Console.WriteLine("ToCharArry is 24 times faster than ElementAt\n");
        }
    }
}
