using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExtension
{
    class Program
    {
        static void Main(string[] args)
        {
            object x = 5;
            object y = 0;
            Console.WriteLine("X = {0}", x);
            Console.WriteLine("Y = {0}", y);
           y = y.SetValueObjectExtension(x);
           Console.WriteLine("Y = {0}", y);
        }
    }
}
