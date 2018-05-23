using RayTracing.Misc;
using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects
{
    public class LightSource : Sphere
    {
        /// <inheritdoc />
        public LightSource (double radius, Vector center, Surface surface, double intensity = 1) :
            base (radius, center, surface) => Intensity = intensity;

        public LightSource (double radius, Vector center, Colour colour, double intensity = 1) :
            base (radius, center, new Surface (0, colour)) => Intensity = intensity;

        /// <summary>
        /// Default value is 1
        /// </summary>
        public double Intensity { get; private set; }

        public Vector GetDirection (Vector point) => (Center - point).Unit ();

        public double GetDistance (Vector point) => (Center - point).Abs ();

        public double GetRemainingIntensity (Vector point) => GetRemainingIntensity (GetDistance (point));

        public double GetRemainingIntensity (double distance) => Intensity / (4 * Constants.PI * distance);
    }
}