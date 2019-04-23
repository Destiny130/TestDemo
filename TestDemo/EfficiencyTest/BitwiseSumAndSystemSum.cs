using System;
using System.Diagnostics;

namespace TestDemo.EfficiencyTest
{
    public class BitwiseSumAndSystemSum
    {
        public void Execute()
        {
            Stopwatch watch = new Stopwatch();
            int loopCount = (int)1E6;

            watch.Start();
            for (int i = 0; i < loopCount; ++i)
            {
                int sum = i + i;
            }
            watch.Stop();
            Console.WriteLine($"System sum function spent:\t\t{watch.ElapsedMilliseconds:F3} milliseconds");

            watch.Restart();
            for (int i = 0; i < loopCount; ++i)
            {
                int sum = Sum_Iterative(i, i);
            }
            watch.Stop();
            Console.WriteLine($"Bitwise sum function(iterative) spent:\t{watch.ElapsedMilliseconds:F3} milliseconds");

            watch.Restart();
            for (int i = 0; i < loopCount; ++i)
            {
                int sum = Sum_Recursive(i, i);
            }
            watch.Stop();
            Console.WriteLine($"Bitwise sum function(recursive) spent:\t{watch.ElapsedMilliseconds:F3} milliseconds");
        }

        private int Sum_Iterative(int a, int b)
        {
            if (a == 0)
                return b;
            while (b != 0)
            {
                int carry = a & b;
                a = a ^ b;
                b = carry << 1;
            }
            return a;
        }

        private int Sum_Recursive(int a, int b)
        {
            return b == 0 ? a : Sum_Recursive(a ^ b, (a & b) << 1);
        }
    }
}
