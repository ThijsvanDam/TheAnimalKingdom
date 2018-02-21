using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdomUT
{
    [TestClass]
    public class TestVector2D
    {
        [TestMethod]
        public void TestLength()
        {
            Vector2D pytha = new Vector2D(3, 4);

            Assert.AreEqual(5, pytha.Length());
        }

        [TestMethod]
        public void TestLengthWithMinus()
        {
            Vector2D pytha = new Vector2D(-3, -4);

            Assert.AreEqual(5, pytha.Length());
        }

        [TestMethod]
        public void TestPlus()
        {
            Vector2D first = new Vector2D(1, 4);
            Vector2D second = new Vector2D(6, 4);

            first.Add(second);

            Assert.AreEqual(7, first.X);
            Assert.AreEqual(8, first.Y);
        }
        [TestMethod]
        public void TestPlusStrangeValues()
        {
            Vector2D first = new Vector2D(1, 4.7284);
            Vector2D second = new Vector2D(-6, 4.614);

            first.Add(second);

            Assert.AreEqual(-5, first.X);
            Assert.AreEqual(9.3424, first.Y);
        }

        [TestMethod]
        public void TestSubstract()
        {
            Vector2D first = new Vector2D(1, 4);
            Vector2D second = new Vector2D(6, 4);

            first.Substract(second);

            Assert.AreEqual(-5, first.X);
            Assert.AreEqual(0, first.Y);
        }

        [TestMethod]
        public void TestSubstractWithStrangeValues()
        {
            Vector2D first = new Vector2D(-1, -4);
            Vector2D second = new Vector2D(-6, 4);

            first.Substract(second);

            Assert.AreEqual(5, first.X);
            Assert.AreEqual(-8, first.Y);
        }

        [TestMethod]
        public void TestNormalize()
        {
            Vector2D first = new Vector2D(3, 4);

            first.Normalize();

            Assert.AreEqual(3.0 / 5.0, first.X);
            Assert.AreEqual(4.0 / 5.0, first.Y);
        }

        [TestMethod]
        public void TestTruncate()
        {
            Vector2D first = new Vector2D(3, 4);

            first.Truncate(4.0);

            Assert.AreEqual((3.0/5.0) * 4.0, first.X);
            Assert.AreEqual((4.0/5.0) * 4.0, first.Y);
        }

        [TestMethod]
        public void TestDivision()
        {
            Vector2D vector = new Vector2D(4.0, 6.0);

            vector.Divide(2.0);

            Assert.AreEqual(2.0, vector.X);
            Assert.AreEqual(3.0, vector.Y);
        }

        [TestMethod]
        public void TestMultiplication()
        {
            Vector2D vector = new Vector2D(4.0, 6.0);

            vector.Multiply(2.0);

            Assert.AreEqual(8.0, vector.X);
            Assert.AreEqual(12.0, vector.Y);
        }
    }
}
