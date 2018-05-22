using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects
{
    public class Ray
    {
        /// <inheritdoc />
        public Ray (Vector origin, Vector direction, Colour colour = null)
        {
            Origin    = origin;
            Direction = direction;
            Colour    = colour ?? new Colour (0, 0, 0);
        }

        public Vector Origin    { get; private set; }
        public Vector Direction { get; private set; }
        public Colour Colour    { get; set; }

        public Vector Get (double d) => Origin + d * Direction;
    }
}