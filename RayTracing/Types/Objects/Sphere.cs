using System;

using RayTracing.Misc;
using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects
{
    public class Sphere : Object
    {
        public double Radius { get; }
        public Vector Center { get; }

        /// <inheritdoc />
        public override double? Intersect (Ray ray)
        {
            var intersections = Intersections (ray);
            if (intersections == null)
                return null;
            var t = Math.Min (intersections.Value.Item1, intersections.Value.Item2);

            return t > 0 ? t : (double?) null;
        }

        public override (double, double)? Intersections (Ray ray)
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

            return (t1, t2);
        }

        /// <inheritdoc />
        public Sphere (Surface surface, double radius, Vector center) : base (surface)
        {
            Radius = radius;
            Center = center;
        }

        public override Ray Reflect (Ray ray, double lightIntensity, double? tEvaluated = null)
        {
            var t = tEvaluated ?? Intersect (ray) ?? -1;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (t == -1)
                return null;
            var y = ray.Get (t);
            return Reflect (ray, lightIntensity, y);
        }

        public override Ray Reflect (Ray ray, double lightIntensity, Vector y)
        {
            var c = Center;
            var d = ray.Direction;
            var n = (y - c) / (y - c).Abs ();
            var r = (d - 2 * (n * d) * n).Unit ();

            var colour = GetNewColour (ray, lightIntensity);

            return new Ray (y, r, colour, ray.IntensityLeft * Surface.ReflectionAmount);
        }
    }
}