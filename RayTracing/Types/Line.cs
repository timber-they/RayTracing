namespace RayTracing.Types
{
    public class Line
    {
        public Vector Origin { get; }
        public Vector Direction { get; }

        /// <inheritdoc />
        public Line (Vector origin, Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public double GetDistance (Vector point)
        {
            var a = point;
            var b = Origin;
            var ba = a - b;
            var bc = Direction;
            var distance = ba.VectorProduct (bc).Abs () / bc.Abs ();

            return distance;
        }

        /// <inheritdoc />
        public override string ToString () => $"{Origin} + t * {Direction}";
    }
}