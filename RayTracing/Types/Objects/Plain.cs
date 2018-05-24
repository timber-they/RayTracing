using System;
using System.Diagnostics;
using System.Linq;

using RayTracing.Misc;
using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects
{
    public class Plain : Object
    {
        /// <summary>
        /// TopLeft, TopRight, BottomRight, BottomLeft
        /// </summary>
        public (Vector, Vector, Vector, Vector) Corners { get; }

        /// <inheritdoc />
        public Plain (Surface surface, (Vector, Vector, Vector, Vector) corners) : base (surface)
        {
            Corners = corners;
            CheckOrthogonality ();
        }

        public Plain (Surface surface, Vector topLeft, Vector topRight, Vector bottomRight, Vector bottomLeft) :
            base (surface)
        {
            Corners = (topLeft, topRight, bottomRight, bottomLeft);
            CheckOrthogonality ();
        }

        private void CheckOrthogonality ()
        {
            var edges = GetEdges ();
            if (Math.Abs (edges.Item1.Direction.ScalarProduct (edges.Item3.Direction)) > 0.00001 ||
                Math.Abs (edges.Item2.Direction.ScalarProduct (edges.Item4.Direction)) > 0.00001)
                throw new InvalidOperationException ("The edges have to be orthogonal.");
        }

        /// <inheritdoc />
        public override double? Intersect (Ray ray)
        {
            var o       = ray.Origin;
            var d       = ray.Direction;
            var p       = Corners.Item1;
            var n       = GetNormalVector ();
            var divisor = d * n;
            if (Math.Abs (divisor) < 0.00001)
                return null;
            var dividend = (p - o) * n;
            var t        = dividend / divisor;
            if (t < -0.0001) // TODO: Why is this neccessary?
                return null;
            var point    = ray.Get (t);

            var edges = GetEdges ();
            var maxHorizontalDistance =
                Math.Max (edges.Item1.GetDistance (point), edges.Item2.GetDistance (point));
            var maxVerticalDistance =
                Math.Max (edges.Item3.GetDistance (point), edges.Item4.GetDistance (point));
            var maxAllowedHorizontalDistance = (Corners.Item4 - Corners.Item1).Abs ();
            var maxAllowedVerticalDistance   = (Corners.Item2 - Corners.Item1).Abs ();

            return maxHorizontalDistance > maxAllowedHorizontalDistance ||
                   maxVerticalDistance > maxAllowedVerticalDistance
                       ? (double?) null
                       : t;
        }

        public Vector GetNormalVector ()
        {
            var (topLeft, topRight, _, bottomLeft) = Corners;
            var topLeftTopRight   = (topRight - topLeft).Unit ();
            var topLeftBottomLeft = (bottomLeft - topLeft).Unit ();
            var normal            = topLeftTopRight.VectorProduct (topLeftBottomLeft).Unit ();

            return normal;
        }

        /// <inheritdoc />
        public override Ray Reflect (Ray ray, double intensity, double? tEvaluated = null)
        {
            var t = tEvaluated ?? Intersect (ray) ?? -1;
            if (t == -1)
                return null;
            var intersectionPoint = ray.Get (t);
            return Reflect (ray, intensity, intersectionPoint);
        }

        /// <inheritdoc />
        public override Ray Reflect (Ray ray, double intensity, Vector intersectionPoint)
        {
            var normal             = GetNormalVector ();
            var currentDirection   = ray.Direction;
            var normalPart         = normal * (normal * currentDirection);
            var reflectedDirection = currentDirection - 2 * normalPart;
            var colour             = GetNewColour (ray, intensity);
            var newRay = new Ray (intersectionPoint, reflectedDirection, colour,
                                  ray.IntensityLeft * Surface.ReflectionAmount);

            return newRay;
        }

        /// <inheritdoc />
        public override (double, double)? Intersections (Ray ray)
        {
            var intersection = Intersect (ray);
            return intersection == null ? ((double, double)?) null : (intersection.Value, intersection.Value);
        }

        /// <summary>
        /// TopLeft -> TopRight
        /// BottomLeft -> BottomRight
        /// TopLeft -> BottomLeft
        /// TopRight -> BottomRight
        /// </summary>
        /// <returns></returns>
        public (Line, Line, Line, Line) GetEdges ()
        {
            var (topLeft, topRight, bottomRight, bottomLeft) = Corners;
            var tLtR = topRight - topLeft;
            var bLbR = bottomRight - bottomLeft;
            var tLbL = bottomLeft - topLeft;
            var tRbR = bottomRight - topRight;

            return
                (
                    new Line (topLeft, tLtR),
                    new Line (bottomLeft, bLbR),
                    new Line (topLeft, tLbL),
                    new Line (topRight, tRbR)
                );
        }
    }
}