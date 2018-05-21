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

            var determant = (2 * (d * (s - c))).Square () - (s - c).Square () + r.Square ();
            if (determant < 0)
                return null;
            var t1 = -(d * (s - c)) + Math.Sqrt (determant);
            var t2 = -(d * (s - c)) - Math.Sqrt (determant);
            var t = Math.Min (t1, t2);

            return t > 0 ? t : (double?) null;
        }

        /// <inheritdoc />
        public Sphere (double radius, Vector center, Surface surface) : base (center, surface) => Radius = radius;

        public Ray Reflect (Ray ray, double t)
        {
            var y = ray.Get (t);
            var c = Center;
            var d = ray.Direction;
            var n = (y - c) / (y - c).Abs ();
            var r = d - 2 * (n * d) * n;

            return new Ray (y, r);
        }
    }
}