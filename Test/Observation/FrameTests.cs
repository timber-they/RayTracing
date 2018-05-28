using NUnit.Framework;

using RayTracing.Types;
using RayTracing.Types.Observation;


namespace Test.Observation
{
    [TestFixture]
    public class FrameTests
    {
        private readonly Frame _frame = new Frame (new Vector (1, 2, 3), new Vector (10, 0, 1));

        [Test]
        public void DiagonalBar ()
        {
            var expected = new Vector (9, -2, -2);
            var actual   = _frame.GetDiagonal ();

            Assert.AreEqual (expected, actual);
        }

        [Test]
        public void VerticalBar ()
        {
            var expected = new Vector (0, 0, -2);
            var actual   = _frame.GetVertical ();

            Assert.AreEqual (expected, actual);
        }

        [Test]
        public void HorizontalBar ()
        {
            var expected = new Vector (9, -2, 0);
            var actual   = _frame.GetHorizontal ();

            Assert.AreEqual (expected, actual);
        }

        [Test]
        public void GetPixelCenter ()
        {
            var pixelCountHorizontal = 10;
            var pixelCountVertical = 5;
            var pixelHorizontal = 3;
            var pixelVertical = 3;
            var expected = new Vector (4.15, 1.3, 1.6);
            var actual =
                _frame.GetPixelCenter (pixelHorizontal, pixelVertical, pixelCountHorizontal, pixelCountVertical);

            Assert.AreEqual (expected, actual);
        }
    }
}