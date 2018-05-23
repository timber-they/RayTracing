using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects
{
    public class Ray
    {
        /// <inheritdoc />
        public Ray (Vector origin, Vector direction, Colour colour = null, double lightLeft = 1)
        {
            Origin    = origin;
            Direction = direction;
            Colour    = colour ?? new Colour (0, 0, 0);
            IntensityLeft = 1;
        }

        public Vector Origin    { get; private set; }
        public Vector Direction { get; private set; }
        public Colour Colour    { get; set; }
        public double IntensityLeft { get; set; }

        public Vector Get (double d) => Origin + d * Direction;
    }
}