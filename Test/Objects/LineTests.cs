using System;

using NUnit.Framework;

using RayTracing.Types;


namespace Test.Objects
{
    [TestFixture]
    public class LineTests
    {
        [Test]
        public void DistanceTest ()
        {
            var line = new Line (new Vector (0, 0, 0), new Vector (1, 1, 0));
            var point = new Vector (1, -1, 0);
            var expected = Math.Sqrt (2);
            var actual = line.GetDistance (point);

            Assert.AreEqual(expected, actual, 0.000001);
        }
    }
}