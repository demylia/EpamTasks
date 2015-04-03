using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Figure
{
    /// <summary>
    /// Own class for Exceptions
    /// </summary>
    public class TringleExeption: Exception
    {
        public TringleExeption(string ex)
            : base(ex)
        {

        }
    }
    
    public class Tringle
    {
        /// <summary>
        /// Fields and properties of the tringle
        /// </summary>
        private Side sideAB, sideBC, sideAC;
        public Point a, b, c;
        static string name = "Tringle";
        static int countTringles;

      
        public Side SideAB
        {
            get { return sideAB; }
            
            set
            {
                if (value.a.x == value.b.x && value.a.y == value.b.y)
                    throw new TringleExeption(Resource.ex1);
                CheckExistingOfTringle(value, SideBC, SideAC); 
                sideAB = value;
            }
        }

        public Side SideBC
        {
            get { return sideBC; }
            set
            {
                if (value.a.x == value.b.x && value.a.y == value.b.y)
                    throw new TringleExeption(Resource.ex1);
               CheckExistingOfTringle(value, SideBC, SideAC); 
                sideBC = value;
            }
        }
        public Side SideAC
        {
            get { return sideAC; }
            set
            {
                if (value.a.x == value.b.x && value.a.y == value.b.y)
                    throw new TringleExeption(Resource.ex1);
                CheckExistingOfTringle(value, SideBC, SideAC); 
                sideAC = value;
            }
        }
        /// <summary>
        /// Checking the sides of the tringle for correctness
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        private static void CheckExistingOfTringle(Side a, Side b, Side c)
        {
            if ((a + b) < c || (b + c) < a || (a + c) < b)
                throw new TringleExeption(Resource.ex2);
        }
        
       /// <summary>
       /// Create two constructor for the tringle
       /// </summary>
       /// <param name="a"></param>
       /// <param name="b"></param>
       /// <param name="c"></param>
        public Tringle(Point a, Point b, Point c)
        {
            this.a = a;
            this.b = b;
            this.c = c;

            sideAB = new Side(a, b);
            sideBC = new Side(b, c);
            sideAC = new Side(a, c);

            CheckExistingOfTringle(sideAB, sideBC, sideAC);
            Tringle.countTringles++;
        }
        public Tringle(Side a,Side b,Side c)
        {
            CheckExistingOfTringle(a, b, c);
            SideAB = a;
            SideBC = b;
            SideAC = c;

            Tringle.countTringles++;
        }

        
        /// <summary>
        /// Methods for calculating the perimetr of the tringle
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public double GetPerimetr()
        {
            return sideAB + SideAC + SideBC;
        }
        /// <summary>
        /// Method for calculating the area of the tringle
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public double GetArea()
        {
            var p = (SideAB + SideAC + SideBC) / 2;

            return Math.Sqrt(p*(p - SideAB)*(p - SideAC)*(p - SideBC));
        }
        /// <summary>
        /// Compare of tringles
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static int CompareAreasOfTriangles(Tringle tr1, Tringle tr2)
        {
            if (tr1 == null || tr2 == null)
                throw new ArgumentNullException("ex");
            var area1 = tr1.GetArea();
            var area2 = tr2.GetArea();
            if (area1 > area2)
                return 1;
            if (area1 == area2)
                return 0;
            else
                return -1;
          
        }
    }
}
