using NUnit.Framework;

using RayTracing.Types;
using RayTracing.Types.Observation;


namespace Test.Observation
{
    [TestFixture]
    public class ObservatorTests
    {
        [Test]
        public void RayCount ()
        {
            var observator =
                new Observator (new Vector (1, 2, 3), new Frame (new Vector (2, 3, 4), new Vector (4, 4, 2)));
            var pixelsHorizontal = 10;
            var pixelsVertical = 10;
            var expected = 100;
            var actual = observator.GetRays (pixelsHorizontal, pixelsVertical).Count;

            Assert.AreEqual (expected, actual);
        }
    }
}