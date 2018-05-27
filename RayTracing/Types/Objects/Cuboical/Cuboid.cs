using System;
using System.Collections.Generic;
using System.Linq;

using RayTracing.Misc;
using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects.Cuboical
{
    public class Cuboid : Object
    {
        /// <summary>
        /// Top, Front, Left, Back, Right, Bottom
        /// </summary>
        private (Plain, Plain, Plain, Plain, Plain, Plain) Plains { get; }

        /// <inheritdoc />
        public Cuboid (Surface surface, (Plain, Plain, Plain, Plain, Plain, Plain) plains) : base (surface)
        {
            Plains = plains;
            CheckPlains ();
        }

        public Cuboid (Surface surface, Plain top, Plain front, Plain left, Plain back, Plain right, Plain bottom) :
            base (surface)
        {
            Plains = (top, front, left, back, right, bottom);
            CheckPlains ();
        }

        public Cuboid (
            Surface surface,
            Vector  hlh, Vector hhh, Vector lhh, Vector llh, Vector hll, Vector hhl, Vector lhl, Vector lll)
            : this (surface,
                    new Plain (surface, hlh, hhh, lhh, llh),
                    new Plain (surface, llh, lhh, lhl, lll),
                    new Plain (surface, hlh, llh, lll, hll),
                    new Plain (surface, hlh, hhh, hhl, hll),
                    new Plain (surface, lhh, hhh, hhl, lhl),
                    new Plain (surface, hll, hhl, lhl, lll)) {}

        private void CheckPlains ()
        {
            var (top, front, left, back, right, bottom) = GetNormals ();
            if (Math.Abs (top * front) > 0.001 ||
                Math.Abs (front * left) > 0.001 ||
                Math.Abs (left * back) > 0.001 ||
                Math.Abs (back * right) > 0.001 ||
                Math.Abs (right * bottom) > 0.001 ||
                Plains.ToList ().Any (plain => !Equals (plain.Surface, Surface)))
                throw new Exception ("Invalid plains");
        }

        private (Vector, Vector, Vector, Vector, Vector, Vector) GetNormals () =>
            (Plains.Item1.GetNormalVector (), Plains.Item2.GetNormalVector (), Plains.Item3.GetNormalVector (),
             Plains.Item4.GetNormalVector (), Plains.Item5.GetNormalVector (), Plains.Item6.GetNormalVector ());

        /// <inheritdoc />
        public override double? Intersect (Ray ray) => Plains.ToList ().Min (plain => plain.Intersect (ray));

        /// <inheritdoc />
        public override Ray Reflect (Ray ray, double intensity, double? tEvaluated = null)
        {
            var   minIntersectionDistance = double.MaxValue;
            Plain minIntersectionPlain    = null;
            foreach (var plain in Plains.ToList ())
            {
                var intersection = plain.Intersect (ray);
                if (intersection == null || intersection.Value > minIntersectionDistance)
                    continue;
                minIntersectionDistance = intersection.Value;
                minIntersectionPlain    = plain;
            }

            if (tEvaluated.HasValue && Math.Abs (tEvaluated.Value - minIntersectionDistance) > 0.00001)
                throw new Exception ($"False tEvaluated! ({tEvaluated.Value} vs {minIntersectionDistance})");

            return minIntersectionPlain?.Reflect (ray, intensity, minIntersectionDistance);
        }

        /// <inheritdoc />
        public override Ray Reflect (Ray ray, double intensity, Vector y)
        {
            var   minIntersectionDistance = double.MaxValue;
            Plain minIntersectionPlain    = null;
            foreach (var plain in Plains.ToList ())
            {
                var intersection = plain.Intersect (ray);
                if (intersection == null || intersection.Value > minIntersectionDistance)
                    continue;
                minIntersectionDistance = intersection.Value;
                minIntersectionPlain    = plain;
            }

            return minIntersectionPlain?.Reflect (ray, intensity, y);
        }

        /// <inheritdoc />
        public override (double, double)? Intersections (Ray ray)
        {
            var values = new List <double> ();
            foreach (var plain in Plains.ToList ())
            {
                var distance = plain.Intersect (ray);
                if (distance == null)
                    continue;
                values.Add (distance.Value);
            }

            switch (values.Count)
            {
                case 0:
                    return null;
                case 1:
                    return (values [0], values [0]);
                case 2:
                    return (values [0], values [1]);
                default:
                    throw new Exception ($"This isn't actually possible. Intersection count was {values.Count}");
            }
        }
    }
}