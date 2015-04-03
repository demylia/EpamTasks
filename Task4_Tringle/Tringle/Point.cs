using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figure
{
    public struct Point
    {
        public double x, y;
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
       /// <summary>
        ///  Overload operators "+"
       /// </summary>
       /// <param name="a"></param>
       /// <param name="b"></param>
       /// <returns></returns>

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }
        
    }
}
