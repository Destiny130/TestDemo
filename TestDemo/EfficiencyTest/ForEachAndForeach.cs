using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestDemo.EfficiencyTest
{
    public class ForEachAndForeach
    {
        public void Execute()
        {
            List<int> intList = new List<int>() { 1, -94859, 2, 43, 2, 1112, 3455, 34, 2, -39, 10 };
            List<string> strList = new List<string>() { "This is a normal length sentence.", "normal length", "is a ", "length", "ntence" };
            List<Tuple<int, string>> tupleList = new List<Tuple<int, string>>()
            {
                new Tuple<int, string>(1,"3848"),
                new Tuple<int, string>(499,"389004"),
                new Tuple<int, string>(99542,"389004"),
                new Tuple<int, string>(-596,"feold"),
                new Tuple<int, string>(-46299,"gjfjig"),
            };

            Stopwatch watch = new Stopwatch();
            string strTmp = String.Empty;
            int intTmp = 0;
            int loopCount = 1000000;

            watch.Start();
            for (int i = 0; i < loopCount; ++i)
            {
                foreach (string str in strList)
                {
                    strTmp = str;
                }
                foreach (int k in intList)
                {
                    intTmp = k;
                }
                foreach (Tuple<int, string> tuple in tupleList)
                {
                    strTmp = tuple.Item2;
                    intTmp = tuple.Item1;
                }
            }
            watch.Stop();
            Console.WriteLine($"foreach spent:\t{watch.ElapsedMilliseconds:F3} milliseconds");

            watch.Restart();
            for (int i = 0; i < loopCount; ++i)
            {
                strList.ForEach(str => strTmp = str);
                intList.ForEach(k => intTmp = k);
                tupleList.ForEach(tuple => { strTmp = tuple.Item2; intTmp = tuple.Item1; });
            }
            watch.Stop();
            Console.WriteLine($"ForEach spent:\t{watch.ElapsedMilliseconds:F3} milliseconds");

            Console.WriteLine("ForEach is about two times faster than foreach\n");
        }
    }
}
