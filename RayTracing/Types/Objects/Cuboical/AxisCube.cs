using RayTracing.Types.Objects.Plains;
using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects.Cuboical
{
    public class AxisCube : Cube
    {
        /// <inheritdoc />
        public AxisCube (Surface surface, (Plain, Plain, Plain, Plain, Plain, Plain) plains) : base (surface, plains) {}

        /// <inheritdoc />
        public AxisCube (Surface surface, Plain top, Plain front, Plain left, Plain back, Plain right, Plain bottom) :
            base (surface, top, front, left, back, right, bottom) {}

        /// <inheritdoc />
        public AxisCube (
            Surface surface, Vector hlh, Vector hhh, Vector lhh, Vector llh, Vector hll, Vector hhl, Vector lhl,
            Vector  lll) : base (surface, hlh, hhh, lhh, llh, hll, hhl, lhl, lll) {}
    }
}