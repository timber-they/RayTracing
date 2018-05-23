using System;

using RayTracing.Misc;
using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects
{
    public class Sphere : Object
    {
        public double Radius { get; }

        /// <inheritdoc />
        public override double? Intersect (Ray ray)
        {
            var d = ray.Direction;
            var s = ray.Origin;
            var c = Center;
            var r = Radius;

            var determant = (d * (s - c)).Square () - (s - c).Square () + r.Square ();
            if (determant < 0)
                return null;
            var t1 = -(d * (s - c)) + Math.Sqrt (determant);
            var t2 = -(d * (s - c)) - Math.Sqrt (determant);
            var t  = Math.Min (t1, t2);

            return t > 0 ? t : (double?) null;
        }

        /// <inheritdoc />
        public Sphere (double radius, Vector center, Surface surface) : base (center, surface) => Radius = radius;

        public override Ray Reflect (Ray ray, double? tEvaluated = null)
        {
            var t = tEvaluated ?? Intersect (ray) ?? -1;
            if (t == -1)
                return null;
            var y = ray.Get (t);
            var c = Center;
            var d = ray.Direction;
            var n = (y - c) / (y - c).Abs ();
            var r = (d - 2 * (n * d) * n).Unit ();

            var colour = ray.Colour + (1 - Surface.ReflectionAmount) * Surface.Colour * ray.IntensityLeft;

            return new Ray (y, r, colour, ray.IntensityLeft * Surface.ReflectionAmount);
        }
    }
}