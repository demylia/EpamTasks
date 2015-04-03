using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MathClass;

namespace MySpace
{
    class Program
    { 
        static void Main(string[] args)
        {
            int value, degree;
        
            Console.Write("Type a number: ");
            int.TryParse(Console.ReadLine(), out value);
            Console.Write("Type a degree: ");
            int.TryParse(Console.ReadLine(), out degree);
            Console.WriteLine("Root of number: " + Calculate.CalculateRoot(value,degree));
            Console.WriteLine("Binary representation of number: " + Calculate.ConvertToBinary(value));

            Console.ReadKey();

        }
    }
}
