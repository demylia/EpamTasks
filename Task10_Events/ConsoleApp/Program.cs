using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Timer;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Set timer");
            double time;
            double.TryParse(Console.ReadLine(), out time);
            Console.WriteLine("Set step");
            double step;
            double.TryParse(Console.ReadLine(), out step);

            ClassA clA = new ClassA();
            ClassB clB = new ClassB();
            Clock clock = new Clock(time,step);

            clA.StartTimer(clock);
            clB.GetTick(clock);
            clock.Start(clock.Time, clock.Step);

            //Clock ut = new Clock((uint)time *10);
            //Console.WriteLine("You set {0}", time);
            //ut.TimeEnd += ut_TimeEnd;
            //ut.TimeEnd += delegate { Console.WriteLine("Time end"); };
            //ut.TimeEnd += () => { Thread.Sleep(2000); Console.WriteLine("I repeat you - time end"); };

          
            //    ut.InvokeTimeEnd(ut.Time);
            Console.ReadKey();
            

        }

        static void ut_TimeEnd(object obj, EventArgs e)
        {
            Console.WriteLine();
        }
    }
}
