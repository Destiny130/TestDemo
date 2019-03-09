using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTuple
{
    class TupleTest
    {
        static void Main(string[] args)
        {
            Tuple<Test, Test> tuple = new Tuple<Test, Test>(new Test(1, "1"), new Test(2, "2"));
            //tuple.Item1 = new Test();  //Compile error
            Test t1 = tuple.Item1;
            t1.ID = 3;
            t1.Name = "3";
            Test t2 = tuple.Item2;
            t2 = new Test(4, "4");
            Console.WriteLine($"Item1: {tuple.Item1.ID}, {tuple.Item1.Name}");
            Console.WriteLine($"Item2: {tuple.Item2.ID}, {tuple.Item2.Name}");

            Console.ReadKey();
        }
    }

    class Test
    {
        public int ID;
        public string Name;

        public Test()
        {

        }

        public Test(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
