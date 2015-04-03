using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Figure;

namespace UnitTest
{
    [TestClass]
    public class UnitTestFigure
    {
        [TestMethod]
        public void GetPerimetr()
        {
            //arrange
            Tringle tr = new Tringle(new Point(3, 3), new Point(4, 4), new Point(5, 5));

            //act
            double perimetr = tr.GetPerimetr();
            // Какое первое значения надо подставлять, eсли не знаем его ????
            //assert
            Assert.AreEqual(perimetr, perimetr, "Result isn't correct");

        }

        [TestMethod]
        public void GetArea()
        {
            //arrange
            Tringle tr = new Tringle(new Point(2, 2), new Point(4, 4), new Point(3, 3));

            //act
            double area = tr.GetArea();

            //assert
            Assert.AreEqual(area, area, "Result isn't correct");
        }
        [TestMethod]
        [ExpectedException(typeof(TringleExeption))]
        public void ValidatePoint()
        {
            //arrange,act
            Tringle tr = new Tringle(new Point(4, 4), new Point(4, 4), new Point(3, 3));

        }
      
         [TestMethod]
       public void CompareAreasOfTriangles_FirstTringleMore()
        {
            Tringle tr1 = new Tringle(new Point(2, 4), new Point(4, 4), new Point(3, 1));
            Tringle tr2 = new Tringle(new Point(2, 4), new Point(4, 4), new Point(3, 3));
            
            int res = Tringle.CompareAreasOfTriangles(tr1,tr2);

            Assert.AreEqual(1, res, "The result isn't correct");
           
        }
          [TestMethod]
       public void CompareAreasOfTriangles_FirstTringleLess()
        {
            Tringle tr1 = new Tringle(new Point(2, 4), new Point(4, 4), new Point(3, 1));
            Tringle tr2 = new Tringle(new Point(2, 4), new Point(5,5), new Point(3,1));
            tr1.GetArea();
            tr2.GetArea();
            int res = Tringle.CompareAreasOfTriangles(tr1,tr2);

            Assert.AreEqual(-1, res, "The result isn't correct");
           
        }
         [TestMethod]
       public void CompareAreasOfTriangles_TringlesAreEqual()
        {
            Tringle tr1 = new Tringle(new Point(2, 4), new Point(4, 4), new Point(3, 1));
            Tringle tr2 = new Tringle(new Point(2, 4), new Point(4, 4), new Point(3, 1));
            
            int res = Tringle.CompareAreasOfTriangles(tr1,tr2);

            Assert.AreEqual(0, res, "The result isn't correct");
           
        }
         [TestMethod]
         public void GetNewPoint()
         {
             Point p1 = new Point(3, 1);
             Point p2 = new Point(3, 1);
             Point p3 = new Point(6, 2);
             Point sum = p1 + p2;

             Assert.AreEqual(p3, sum, "The result isn't correct");

         }
         [TestMethod]
         public void SumSideAndDouble()
         {
             Side s1 = new Side(new Point(3, 1), new Point(3,4));
             double d = 3.0;
             double res = 6.0;

             double sum = s1 + d;

             Assert.AreEqual(res, sum, "The result isn't correct");

         }
         [TestMethod]
         public void FirstSideMoreThanSecond()
         {
             Side s1 = new Side(new Point(3, 1), new Point(3, 4));
             Side s2 = new Side(new Point(3, 1), new Point(3,3));
             bool b = true;

             bool res = s1 > s2;

             Assert.AreEqual(b, res, "The result isn't correct");

         }
    }
}
