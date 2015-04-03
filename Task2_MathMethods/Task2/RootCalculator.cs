using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Task2
{
    class Calculator
    {
        public float CalculateRoot(int number,int accuracy)
        {
            Math.Pow(10, 12);
            return number;
        }
        public string ConvertToString(long number)
        {
            string str = "";
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (number > 0)
            {
                str = number % 2 + str;
                number = number / 2;
            }
            Thread.Sleep(1000);
            stopWatch.Stop();
            TimeSpan sp = stopWatch.Elapsed;
            Console.WriteLine("string {0}, time {1:00}",str,sp.Milliseconds/1000);
            stopWatch.Start();

            StringBuilder stringBuilder = new StringBuilder();
            while (number > 0)
            {
                stringBuilder.Insert(0,(number % 2));
                number = number / 2;
            }
            Console.WriteLine("string {0}, time {1:00}", stringBuilder, sp.Milliseconds/1000);
            Console.ReadKey();
            return str;
        }
    }
}
