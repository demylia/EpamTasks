using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathObjects;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidateCalculatePolynome_TestNumberOne()
        {
            //arrange
            int x = 1;
            Polynome m = new Polynome(new int[]{1,2,3});
            //act
            double res = m.CalculatePolynome(x);
            //assert
            Assert.AreEqual(6, res, "Result isn't correct");
        }
        [TestMethod]
        public void ValidateCalculatePolynome_TestNumberZero()
        {
            //arrange
            int x = 0;
            Polynome m = new Polynome(new int[] { 1, 2, 3 });
            //act
            double res = m.CalculatePolynome(x);
            //assert
            Assert.AreEqual(0, res, "Result isn't correct");
        }
        [TestMethod]
        public void ValidateCalculatePolynome_TestNumberMoreOne()
        {
            //arrange
            int x = 2;
            Polynome m = new Polynome(new int[] { 1, 2, 3 });
            //act
            double res = m.CalculatePolynome(x);
            //assert
            Assert.AreEqual(17, res, "Result isn't correct");
        }
         [TestMethod]
        public void ValidateOperatorPLus_Test()
        {
            //arrange
            Polynome m1 = new Polynome(new int[] { 1, 2, 3 });
            Polynome m2 = new Polynome(new int[] { 1, 2, 3 });
            Polynome m3 = new Polynome(new int[] { 2, 4, 6 });
            //act
            var res = m1 + m2;
            //assert
            Assert.AreEqual(m3, res, "Result isn't correct");
         
        }
       
    }
}
