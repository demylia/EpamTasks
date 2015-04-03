using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalcMethods;

namespace UnitTestProject1
{
    [TestClass]
    public class CalculateBLTest
    {
        #region ValidNOD_TestMethods
        [TestMethod]
        public void ValidNOD_TestTwoIntNumbers()
        {
            //arrange
            int a = 3;
            int b = 9;

            //act
            int nod = CalculateBL.CalculateNOD(a, b);

            //assert
            Assert.AreEqual(3, nod,"Result isn't correct"); 
        }
          [TestMethod]
        public void ValidNOD_TestFirstZeroSecondInt()
        {
            //arrange
            int a = 0;
            int b = 9;

            //act
            int nod = CalculateBL.CalculateNOD(a, b);

            //assert
            Assert.AreEqual(b, nod, "Result isn't correct");
        }
          [TestMethod]
        public void ValidNOD_TestFirstIntSecondZero()
        {
            //arrange
            int a = 9;
            int b = 0;

            //act
            int nod = CalculateBL.CalculateNOD(a, b);

            //assert
            Assert.AreEqual(a, nod, "Result isn't correct");
        }
          [TestMethod]
        public void ValidNOD_TestFirstEqualSecond()
        {
            //arrange
            int a = 9;
            int b = 9;

            //act
            int nod = CalculateBL.CalculateNOD(a, b);

            //assert
            Assert.AreEqual(9, nod, "Result isn't correct");
        }
          [TestMethod]
          public void ValidNOD_TestFirstSecondEqualsOne()
          {
              //arrange
              int a = 1;
              int b = 1;

              //act
              int nod = CalculateBL.CalculateNOD(a, b);

              //assert
              Assert.AreEqual(1, nod, "Result isn't correct");
          }
        #endregion

        #region ValidNODParams_TestMethods
          [TestMethod]
          public void ValidNODParams_TestIntNumbers()
          {
              //arrange
              int[] arr = new int[]{3,9,9};
              
              //act
              int nod = CalculateBL.CalculateNOD(arr);

              //assert
              Assert.AreEqual(arr[0], nod, "Result isn't correct");
          }
         
          [TestMethod]
          public void ValidNODParams_TestNumberZero()
          {
              //arrange
              int[] arr = new int[] { 3, 9, 0};

              //act
              int nod = CalculateBL.CalculateNOD(arr);

              //assert
              Assert.AreEqual(arr[0], nod, "Result isn't correct");
          }
          [TestMethod]
          public void ValidNODParams_TestEqualNumbers()
          {
              //arrange
              int a = 9;
              int b = 9;
              int c = 9;
              //act
              int nod = CalculateBL.CalculateNOD(a, b, c);

              //assert
              Assert.AreEqual(9, nod, "Result isn't correct");
          }
          [TestMethod]
          public void ValidNODParams_TestNumberOne()
          {
              //arrange
              int a = 1;
              int b = 1;
              int c = 9;
              //act
              int nod = CalculateBL.CalculateNOD(a, b, c);

              //assert
              Assert.AreEqual(1, nod, "Result isn't correct");
          }
          #endregion

        #region ValidCalculateNODAndTime_TestMethod

          [TestMethod]

          public void ValidCalculateNODAndTime_TestIntNumbers()
          {
            //arrange
              int a = 5;
              int b = 10;
              double time;

            //act
              int nod = CalculateBL.CalculateNODAndTime(a, b, out time);

            //assert
              Assert.AreEqual(5, nod, "Result isn't correct");
          }



        #endregion

        #region ValidCalculateNODAndTimeStain_TestMethods

        [TestMethod]

          public void ValidCalculateNODAndTimeStain_TestNumbersZero()
          {
              //arrange
              int a = 0;
              int b = 0;
              double time;

              //act
              int nod = CalculateBL.CalculateNODAndTimeStain(a, b,out time);

              //assert
              Assert.AreEqual(0, nod, "Result isn't correct");

          }
        [TestMethod]
        public void ValidCalculateNODAndTimeStain_TestNumbersOne()
        {
            //arrange
            int a = 1;
            int b = 1;
            double time;

            //act
            int nod = CalculateBL.CalculateNODAndTimeStain(a, b, out time);

            //assert
            Assert.AreEqual(1, nod, "Result isn't correct");
        }
        [TestMethod]
        public void ValidCalculateNODAndTimeStain_TestEqualNumbers()
        {
            //arrange
            int a = 4;
            int b = 4;
            double time;

            //act
            int nod = CalculateBL.CalculateNODAndTimeStain(a, b, out time);

            //assert
            Assert.AreEqual(b, nod, "Result isn't correct");
        }
        [TestMethod]
        public void ValidCalculateNODAndTimeStain_TestIntNumbers()
        {
            //arrange
            int a = 4;
            int b = 8;
            double time;

            //act
            int nod = CalculateBL.CalculateNODAndTimeStain(a, b, out time);

            //assert
            Assert.AreEqual(a, nod, "Result isn't correct");
        }
        [TestMethod]
        public void ValidCalculateNODAndTimeStain_TestIntNumbers2()
        {
            //arrange
            int a = 8;
            int b = 4;
            double time;

            //act
            int nod = CalculateBL.CalculateNODAndTimeStain(a, b, out time);

            //assert
            Assert.AreEqual(b, nod, "Result isn't correct");
        }
        [TestMethod]
        public void ValidCalculateNODAndTimeStain_TestResultIsOne()
        {
            //arrange
            int a = 4;
            int b = 5;
            double time;

            //act
            int nod = CalculateBL.CalculateNODAndTimeStain(a, b, out time);

            //assert
            Assert.AreEqual(1, nod, "Result isn't correct");
        }
        [TestMethod]
        public void ValidCalculateNODAndTimeStain_TestResultIsOne2()
        {
            //arrange
            int a = 5;
            int b = 4;
            double time;

            //act
            int nod = CalculateBL.CalculateNODAndTimeStain(a, b, out time);

            //assert
            Assert.AreEqual(1, nod, "Result isn't correct");
        }
        [TestMethod]
        public void ValidCalculateNODAndTimeStain_TestOddNumbers()
        {
            //arrange
            int a = 7;
            int b = 3;
            double time;

            //act
            int nod = CalculateBL.CalculateNODAndTimeStain(a, b, out time);

            //assert
            Assert.AreEqual(1, nod, "Result isn't correct");
        }

        #endregion
    }
}
