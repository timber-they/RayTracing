using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects
{
    public class LightSource : Sphere
    {
        /// <inheritdoc />
        public LightSource (double radius, Vector center, Surface surface) : base (radius, center, surface) {}

        public LightSource (double radius, Vector center, Colour colour) : base (
            radius, center, new Surface (0, colour)) {}
    }
}