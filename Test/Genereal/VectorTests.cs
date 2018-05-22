using System;

using NUnit.Framework;

using RayTracing.Types;


namespace Test.Genereal
{
    [TestFixture]
    public class VectorTests
    {
        [Test]
        public void Equals ()
        {
            var vector1 = new Vector (1, 2, 3);
            var vector2 = new Vector (1, 2, 3);

            Assert.True (vector1.Equals (vector2));
            Assert.True (vector2.Equals (vector1));
            Assert.True (Equals (vector1, vector2));
            Assert.False (vector1 != vector2);
            Assert.True (vector1 == vector2);
            Assert.AreEqual (vector1, vector2);
        }

        [Test]
        public void EqualsFraction ()
        {
            var vector1 = new Vector (1.2, 3.4, 5.6);
            var vector2 = new Vector (1.2, 3.4, 5.6);

            Assert.True (vector1.Equals (vector2));
            Assert.True (vector2.Equals (vector1));
            Assert.True (Equals (vector1, vector2));
            Assert.False (vector1 != vector2);
            Assert.True (vector1 == vector2);
            Assert.AreEqual (vector1, vector2);
        }

        [Test]
        public void NotEquals ()
        {
            var vector1 = new Vector (1, 2, 3);
            var vector2 = new Vector (2, 2, 3);

            Assert.False (vector1.Equals (vector2));
            Assert.False (vector2.Equals (vector1));
            Assert.False (Equals (vector1, vector2));
            Assert.True (vector1 != vector2);
            Assert.False (vector1 == vector2);
            Assert.AreNotEqual (vector1, vector2);
        }

        [Test]
        public void Abs ()
        {
            var vector   = new Vector (1, 2, 3);
            var expected = Math.Sqrt (14);
            var actual   = vector.Abs ();

            Assert.AreEqual (expected, actual, 0.000001);
        }

        [Test]
        public void ScalarProduct ()
        {
            var vector1  = new Vector (1, 2, 3);
            var vector2  = new Vector (4, 5, 6);
            var expected = 32.0;
            var actual   = vector1.ScalarProduct (vector2);

            Assert.AreEqual (expected, actual, 0.000000001);
        }

        [Test]
        public void VectorProduct ()
        {
            var vector1  = new Vector (1, 2, 3);
            var vector2  = new Vector (4, 5, 6);
            var expected = new Vector (-3, 6, -3);
            var actual   = vector1.VectorProduct (vector2);

            Assert.AreEqual (expected, actual);
        }

        [Test]
        public void Multiply ()
        {
            var vector   = new Vector (1, 2, 3);
            var factor   = 1.5;
            var expected = new Vector (1.5, 3, 4.5);
            var actual   = vector.Multiply (factor);

            Assert.AreEqual (expected, actual);
        }

        [Test]
        public void Divide ()
        {
            var vector   = new Vector (1, 2, 3);
            var factor   = 2;
            var expected = new Vector (0.5, 1, 1.5);
            var actual   = vector.Divide (factor);

            Assert.AreEqual (expected, actual);
        }

        [Test]
        public void Square ()
        {
            var vector   = new Vector (1, 2, 3);
            var expected = 14;
            var actual   = vector.Square ();

            Assert.AreEqual (expected, actual, 0.0000001);
        }

        [Test]
        public void Add ()
        {
            var vector1  = new Vector (1, 2, 3);
            var vector2  = new Vector (4, 5, 6);
            var expected = new Vector (5, 7, 9);
            var actual   = vector1.Add (vector2);

            Assert.AreEqual (expected, actual);
        }

        [Test]
        public void Subtract ()
        {
            var vector1  = new Vector (1, 2, 3);
            var vector2  = new Vector (4, 5, 6);
            var expected = new Vector (-3, -3, -3);
            var actual   = vector1.Subtract (vector2);

            Assert.AreEqual (expected, actual);
        }

        [Test]
        public void Unit ()
        {
            var vector    = new Vector (-1, 10, 2.5);
            var expectedN = 0.09656090991705351576763252374848;
            var expected  = new Vector (-1 * expectedN, 10 * expectedN, 2.5 * expectedN);
            var actual    = vector.Unit ();

            Assert.AreEqual (expected, actual);
            Assert.AreEqual (actual.Abs (), 1.0, 0.0000001);
        }
    }
}