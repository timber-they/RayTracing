using System;

using RayTracing.Misc;
using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects.Cuboical
{
    public class Cube : Cuboid
    {
        /// <inheritdoc />
        public Cube (Surface surface, (Plain, Plain, Plain, Plain, Plain, Plain) plains) : base (surface, plains) {}

        /// <inheritdoc />
        public Cube (Surface surface, Plain top, Plain front, Plain left, Plain back, Plain right, Plain bottom) :
            base (surface, top, front, left, back, right, bottom) {}

        /// <inheritdoc />
        public Cube (
            Surface surface, Vector hlh, Vector hhh, Vector lhh, Vector llh, Vector hll, Vector hhl, Vector lhl,
            Vector  lll) : base (surface, hlh, hhh, lhh, llh, hll, hhl, lhl, lll) {}

        /// <inheritdoc />
        public Cube (Plain bottom) : base (
            bottom.Move ((bottom.Corners.Item2 - bottom.Corners.Item1).Abs () *
                         bottom.GetNormalVector ()), bottom)
            => CheckPlains ();

        /// <inheritdoc />
        protected sealed override void CheckPlains ()
        {
            base.CheckPlains ();
            var areas = Plains.Select (plain => plain.GetArea ());
            if (!areas.AllEqual ())
                throw new Exception ("Not a cube!");
        }

        public Vector GetCenter () =>
            Plains.Item1.Corners.Item1 + 0.5 * (Plains.Item6.Corners.Item3 - Plains.Item1.Corners.Item1);
    }
}