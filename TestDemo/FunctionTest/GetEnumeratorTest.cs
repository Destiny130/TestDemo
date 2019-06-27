using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TestDemo.FunctionTest
{
    public class GetEnumeratorTest
    {
        public void Execute()
        {
            List<string> list = new List<string>() { "1", "2", "3", "4" };
            PrintLine($"Join: {String.Join(", ", list)}");
            List<string>.Enumerator stringEnum = list.GetEnumerator();  //This enumeratot will store a list version
            //for (int i = 0; i < list.Count; ++i)
            //{
            //    list[i] = (i * 10).ToString();
            //}
            //If uncomment out above modify for loop sentences, below MoveNext() method will throw an exception. Because for loop modify list members value which changed list version
            while (stringEnum.MoveNext())
            {
                Print($"{stringEnum.Current}\t");
            }

            PrintLine(Environment.NewLine);
            List<Test> origClassList = new List<Test>() { new Test(1, "a"), new Test(2, "b") };
            List<Test> newClassList = new List<Test>(origClassList);  //This is a shallow copy
            PrintLine($"Join: {String.Join("; ", newClassList)}");
            origClassList[0].Id = 0;
            origClassList[0].Name = " ";
            PrintLine($"Join: {String.Join("; ", newClassList)}");

            List<int> origIntList = new List<int>() { 1, 2, 3 };
            List<int> newIntList = new List<int>(origIntList);  //This is a deep copy, not because new List(IEnumerable<T> collection) is a deep copy method, but because its members' type is System.Int32.
            PrintLine($"Join: {String.Join(", ", newIntList)}");
            origIntList[0] = 0;
            PrintLine($"Join: {String.Join(", ", newIntList)}");

            PrintLine(Environment.NewLine);
            int[] origIntArr = new int[10];
            PrintLine($"Join: {String.Join(", ", origIntArr)}");
            IEnumerator intArrEnum = origIntArr.GetEnumerator();
            //while (intArrEnum.MoveNext())
            //{
            //    Print($"{intArrEnum.Current}\t");
            //}
            //intArrEnum.Reset();
            //Print(Environment.NewLine);
            for (int i = 2; i < 6; ++i)
            {
                origIntArr[i] = i;
            }
            while (intArrEnum.MoveNext())
            {
                Print($"{intArrEnum.Current}\t");
            }

            PrintLine(Environment.NewLine);
        }

        Action<string> Print = str => Console.Write(str);
        Action<string> PrintLine = str => Console.WriteLine(str);
    }

    class Test
    {
        public int Id;
        public string Name;

        private Test()
        {

        }

        public Test(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static bool operator ==(Test left, Test right)
        {
            //int threadId = Thread.CurrentThread.ManagedThreadId;
            //return ReferenceEquals(left, right);
            return (left != null && right != null) ? (left.Id == right.Id && left.Name == right.Name) : false;
        }

        public static bool operator !=(Test left, Test right)
        {
            //int threadId = Thread.CurrentThread.ManagedThreadId;
            return !(left == right);
        }

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
        }
    }
}
