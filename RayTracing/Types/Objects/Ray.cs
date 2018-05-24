using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects
{
    public class Ray : Line
    {
        /// <inheritdoc />
        public Ray (Vector origin, Vector direction, Colour colour = null, double lightLeft = 1) : base (
            origin, direction)
        {
            Colour    = colour ?? new Colour (0, 0, 0);
            IntensityLeft = lightLeft;
        }

        public Colour Colour    { get; set; }
        public double IntensityLeft { get; set; }

        public Vector Get (double d) => Origin + d * Direction;
    }
}