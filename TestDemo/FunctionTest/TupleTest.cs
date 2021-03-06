﻿using System;
using System.Collections.Generic;

namespace TestDemo.FunctionTest
{
    public class TupleTest
    {
        public void Execute()
        {
            Tuple<List<int>, List<string>> tuple = new Tuple<List<int>, List<string>>(new List<int>() { 1, 2 }, new List<string>() { "1", "2" });
            //tuple.Item1 = new List<int>();  //Compile error
            List<int> intList = tuple.Item1;
            List<string> strList = tuple.Item2;
            intList[0] = 0;
            strList[0] = "0";
            intList.Add(3);
            strList.Add("3");
            Console.WriteLine($"Item1: {String.Join(", ", tuple.Item1)}");
            Console.WriteLine($"Item2: {String.Join(", ", tuple.Item2)}");

            Tuple<int, int> t1 = new Tuple<int, int>(1, 2);
            Tuple<int, int> t2 = new Tuple<int, int>(1, 2);
            Console.WriteLine($"int ==: {t1 == t2}, equals: {t1.Equals(t2)}");

            Tuple<string, string> t3 = new Tuple<string, string>("1", "2");
            Tuple<string, string> t4 = new Tuple<string, string>("1", "2");
            Console.WriteLine($"string ==: {t3 == t4}, equals: {t3.Equals(t4)}");

            Console.WriteLine("\n");
        }
    }
}
