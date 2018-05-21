namespace RayTracing.Types.Objects
{
    public class Ray
    {
        /// <inheritdoc />
        public Ray (Vector origin, Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector Origin { get; private set; }
        public Vector Direction { get; private set; }

        public Vector Get (double d) => Origin + d * Direction;
    }
}