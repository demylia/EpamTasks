using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figure
{
    public struct Side
    {
        public Point a, b;
        public double Length 
        { 
            get { return Math.Sqrt(Math.Pow((b.x - a.x), 2) + Math.Pow((b.y - a.y), 2)); } 
        }

        public Side(Point a, Point b)
        {
            this.a = a;
            this.b = b;
        }
        /// <summary>
        ///  Overload operators "+" и ">" 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double operator +(Side a, Side b)
        {
            return a.Length + b.Length;
        }
        //sim Yes
        public static double operator +(double a, Side b)
        {
            return a + b.Length;
        }
        public static double operator +(Side a, double b)
        {
            return a.Length + b;
        }
        //sim Yes
        public static double operator -(double a, Side b)
        {
            return a -b.Length;
        }
        public static double operator -(Side a, double b)
        {
            return a.Length - b;
        }
        public static bool operator >(Side a, Side b)
        {
            return (a.Length > b.Length);
        }
        public static bool operator <(Side a, Side b)
        {
            return (a.Length < b.Length);
        }
        //sim
        public static bool operator >(double a, Side b)
        {
            return (a > b.Length);
        }
        public static bool operator >(Side a, double b)
        {
            return (a.Length > b);
        }
        //sim Yes
        public static bool operator <(double a, Side b)
        {
            return (a < b.Length);
        }
        public static bool operator <(Side a, double b)
        {
            return (a.Length < b);
        }
    }
}
