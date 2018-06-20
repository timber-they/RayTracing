using System;

using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects.Plains
{
    public class AxisPlain : Plain
    {
        /// <inheritdoc />
        public AxisPlain (Surface surface, (Vector, Vector, Vector, Vector) corners) : base (surface, corners) {}

        /// <inheritdoc />
        public AxisPlain (Surface surface, Vector topLeft, Vector topRight, Vector bottomRight, Vector bottomLeft) :
            base (surface, topLeft, topRight, bottomRight, bottomLeft) {}

        public AxisPlain (Surface surface, Vector bottomLeft, Vector topRight) : base (surface)
        {
            var direction = topRight - bottomLeft;

            var normal = Math.Abs (direction.X1) < 0.0001 ? new Vector (1, 0, 0) : // X2 / X3 - Ebene
                         Math.Abs (direction.X2) < 0.0001 ? new Vector (0, 1, 0) : // X1 / X3 - Ebene
                         Math.Abs (direction.X3) < 0.0001 ? new Vector (0, 0, 1) : // X1 / X2 - Ebene
                                                            throw new Exception ("Invalid plain");
        }
    }
}