using RayTracing.Misc;
using RayTracing.Types.Objects.Interfaces;
using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects.Cuboical
{
    public class CubicalLightSource : Cube, ILightSource
    {
        /// <inheritdoc />
        public CubicalLightSource (
            Surface surface, (Plain, Plain, Plain, Plain, Plain, Plain) plains, double intensity = 1) : base (
            surface, plains)
            => Intensity = intensity;

        /// <inheritdoc />
        public CubicalLightSource (
            Surface surface, Plain top, Plain front, Plain left, Plain back, Plain right, Plain bottom,
            double  intensity = 1) : base (
            surface, top, front, left, back, right, bottom)
            => Intensity = intensity;

        /// <inheritdoc />
        public CubicalLightSource (
            Surface surface, Vector hlh, Vector hhh, Vector lhh, Vector llh, Vector hll, Vector hhl, Vector lhl,
            Vector  lll,     double intensity = 1) : base (surface, hlh, hhh, lhh, llh, hll, hhl, lhl, lll)
            => Intensity = intensity;

        /// <inheritdoc />
        public CubicalLightSource (Plain bottom, double intensity = 1) : base (bottom)
            => Intensity = intensity;

        /// <inheritdoc />
        public double Intensity { get; }

        /// <inheritdoc />
        public Vector GetDirection (Vector point) => (GetCenter () - point).Unit ();

        /// <inheritdoc />
        public double GetDistance (Vector point) => (GetCenter () - point).Abs ();

        /// <inheritdoc />
        public double GetRemainingIntensity (Vector point) => GetRemainingIntensity (GetDistance (point));

        /// <inheritdoc />
        public double GetRemainingIntensity (double distance) => Intensity / (4 * Constants.PI * distance);
    }
}