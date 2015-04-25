using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using task09;

namespace UnitTest
{
    [TestClass]
    public class TestNotePad
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod1()
        {
            NotePad notePad = new NotePad("ghghj");
        }

        [TestMethod]
        public void TestMethod2()
        {
            NotePad notePad = new NotePad("E:\\Coords.txt");
            Assert.AreEqual("some text", notePad.getAllText());
        }

        [TestMethod]
        public void TestMethod3()
        {
            NotePad notePad = new NotePad("E:\\Coords.txt");
            notePad.ChangeText(4, 5, 0, "new ");
            string str = notePad.getAllText();
            Assert.AreEqual("some new text", notePad.getAllText());
        }

        [TestMethod]
        public void TestMethod4()
        {
            NotePad notePad = new NotePad("E:\\Coords.txt");
            notePad.ChangeText(4, 9, 0, " new");
            string str = notePad.getAllText();
            Assert.AreEqual("some text new", notePad.getAllText());
        }

        [TestMethod]
        public void TestMethod5()
        {
            NotePad notePad = new NotePad("E:\\Coords.txt");
            notePad.ChangeText(4, 0, 0, "new ");
            string str = notePad.getAllText();
            Assert.AreEqual("new some text", notePad.getAllText());
        }

        [TestMethod]
        public void TestMethod6()
        {
            NotePad notePad = new NotePad("E:\\Coords.txt");
            notePad.ChangeText(0, 0, 5);
            string str = notePad.getAllText();
            Assert.AreEqual("text", notePad.getAllText());
        }

        [TestMethod]
        public void TestMethod7()
        {
            NotePad notePad = new NotePad("E:\\Coords.txt");
            notePad.ChangeText(0, 4, 5);
            string str = notePad.getAllText();
            Assert.AreEqual("some", notePad.getAllText());
        }

        [TestMethod]
        public void TestMethod8()
        {
            NotePad notePad = new NotePad("E:\\Coords.txt");
            notePad.ChangeText(0, 4, 1);
            string str = notePad.getAllText();
            Assert.AreEqual("sometext", notePad.getAllText());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod9()
        {
            NotePad notePad = new NotePad("E:\\Coords.txt");
            string tmp = null;
            notePad.ChangeText(1, 4, 0, tmp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod10()
        {
            NotePad notePad = new NotePad("E:\\Coords.txt");
            notePad.ChangeText(1, 4, 1, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod11()
        {
            NotePad notePad = new NotePad("E:\\Coords.txt");
            notePad.ChangeText(1, -4, 0, "");
        }
    }
}
