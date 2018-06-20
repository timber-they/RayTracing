using System;

using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects.Plains
{
    public class Disk : Object
    {
        public double Radius { get; }
        public Vector Center { get; }
        public Vector Normal { get; }

        /// <inheritdoc />
        public Disk (Surface surface, double radius, Vector center, Vector normal) : base (surface)
        {
            Radius = radius;
            Center = center;
            Normal = normal;
        }

        /// <inheritdoc />
        public override double? Intersect (Ray ray)
        {
            var o = ray.Origin;
            var d = ray.Direction;
            var p = Center;
            var n = Normal;
            var divisor = d * n;
            if (Math.Abs (divisor) < 0.00001)
                return null;
            var dividend = (p - o) * n;
            var t = dividend / divisor;
            var point = ray.Get (t);

            return (point - Center).Abs () > Radius ? (double?) null : t;
        }

        /// <inheritdoc />
        public override Ray Reflect (Ray ray, double intensity, double? tEvaluated = null)
        {
            var t = tEvaluated ?? Intersect (ray) ?? -1;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (t == -1)
                return null;
            var y = ray.Get (t);
            return Reflect (ray, intensity, y);
        }

        /// <inheritdoc />
        public override Ray Reflect (Ray ray, double intensity, Vector y) =>
            throw new NotImplementedException ();

        /// <inheritdoc />
        public override (double, double)? Intersections (Ray ray) =>
            throw new InvalidOperationException ("There aren't two intersection points with a disk.");
    }
}