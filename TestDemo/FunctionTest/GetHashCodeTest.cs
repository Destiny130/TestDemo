using System;
using System.Collections.Generic;

namespace TestDemo.FunctionTest
{
    public class GetHashCodeTest
    {
        public void Execute()
        {
            Console.WriteLine($"Type\t\t\t==\t\tEquals\t\tHashCode\n");

            int i = 1;
            int j = 1;
            Console.WriteLine($"Int32\t\t\t{i == j}\t\t{i.Equals(j)}\t\t{i.GetHashCode() == j.GetHashCode()}\n");

            string s1 = "aa";
            string s2 = "aa";
            Console.WriteLine($"String\t\t\t{s1 == s2}\t\t{s1.Equals(s2)}\t\t{s1.GetHashCode() == s2.GetHashCode()}\n");

            Tuple<int, int> t1 = new Tuple<int, int>(1, 2);
            Tuple<int, int> t2 = new Tuple<int, int>(1, 2);
            Console.WriteLine($"Tuple<int>\t\t{t1 == t2}\t\t{t1.Equals(t2)}\t\t{t1.GetHashCode() == t2.GetHashCode()}\n");

            Tuple<string, string> t3 = new Tuple<string, string>("1", "2");
            Tuple<string, string> t4 = new Tuple<string, string>("1", "2");
            Console.WriteLine($"Tuple<string>\t\t{t3 == t4}\t\t{t3.Equals(t4)}\t\t{t3.GetHashCode() == t4.GetHashCode()}\n");

            int[] a1 = { 1, 2, 3 };
            int[] a2 = { 1, 2, 3 };
            Console.WriteLine($"Array<int>\t\t{a1 == a2}\t\t{a1.Equals(a2)}\t\t{a1.GetHashCode() == a2.GetHashCode()}\n");

            List<int> l1 = new List<int>() { 1, 2, 3 };
            List<int> l2 = new List<int>() { 1, 2, 3 };
            Console.WriteLine($"List<int>\t\t{l1 == l2}\t\t{l1.Equals(l2)}\t\t{l1.GetHashCode() == l2.GetHashCode()}\n");

            Console.WriteLine("\n");
        }
    }
}
