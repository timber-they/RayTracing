namespace RayTracing.Types.Observation
{
    public class Frame
    {
        public Vector TopLeftCorner     { get; private set; }
        public Vector BottomRightCorner { get; private set; }

        /// <inheritdoc />
        public Frame (Vector topLeftCorner, Vector bottomRightCorner)
        {
            TopLeftCorner     = topLeftCorner;
            BottomRightCorner = bottomRightCorner;
        }

        public Frame (Vector observatorPosition, double distance, Vector direction, double width, double height)
        {
            direction = direction.Unit ();
            var frameCenter = observatorPosition + distance * direction;
            var verticalDirection = new Vector (0, 0, 1);
            var horizontalDirection = direction.VectorProduct (verticalDirection).Unit ();
            var topLeft = frameCenter - width / 2 * horizontalDirection + height / 2 * verticalDirection;
            var bottomRight = topLeft + width * horizontalDirection - height * verticalDirection;

            TopLeftCorner = topLeft;
            BottomRightCorner = bottomRight;
        }

        public Vector GetPixelCenter (
            int pixelHorizontal, int pixelVertical, int pixelCountHorizontal, int pixelCountVertical)
        {
            var horizontalSize      = GetHorizontal ();
            var verticalSize        = GetVertical ();
            var pixelSizeHorizontal = horizontalSize / pixelCountHorizontal;
            var pixelSizeVertical   = verticalSize / pixelCountVertical; // Generally negative
            var pixelTopLeftCorner =
                TopLeftCorner + pixelSizeHorizontal * pixelHorizontal + pixelSizeVertical * pixelVertical;
            var pixelCenter = pixelTopLeftCorner + pixelSizeHorizontal / 2 + pixelSizeVertical / 2;

            return pixelCenter;
        }

        public Vector GetHorizontal ()
        {
            var diagonal = GetDiagonal ();
            return new Vector (diagonal.X1, diagonal.X2, 0);
        }

        /// <summary>
        /// Generally negative
        /// </summary>
        /// <returns></returns>
        public Vector GetVertical () => new Vector (0, 0, GetDiagonal ().X3);

        /// <summary>
        /// From top left to bottom right
        /// </summary>
        /// <returns></returns>
        public Vector GetDiagonal () => BottomRightCorner - TopLeftCorner;

        public double GetHeight () => TopLeftCorner.X3 - BottomRightCorner.X3;

        public double GetWidth () => GetHorizontal ().Abs ();
    }
}