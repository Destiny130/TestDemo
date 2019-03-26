using System;
using System.Collections.Generic;

namespace TestDemo.FunctionTest
{
    public class GetEnumeratorTest
    {
        public void Execute()
        {
            List<string> list = new List<string>() { "1", "2", "3", "4" };
            Console.WriteLine($"Orig list: {String.Join(", ", list)}");
            List<string>.Enumerator enumerator = list.GetEnumerator();
            //for (int i = 0; i < list.Count; ++i)
            //{
            //    list[i] = (i * 10).ToString();
            //}
            Console.WriteLine("After alter");
            Console.WriteLine($"New list: {String.Join(", ", list)}");
            //Console.WriteLine($"Enumerator list: {String.Join(", ", enumerator)}");
            while (enumerator.MoveNext())
            {
                Console.Write($"{enumerator.Current}\t");
            }

            Console.WriteLine("\n");
        }
    }
}
