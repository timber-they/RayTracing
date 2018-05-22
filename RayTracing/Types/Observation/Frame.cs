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

        public Vector GetPixelCenter (
            int pixelHorizontal, int pixelVertical, int pixelCountHorizontal, int pixelCountVertical)
        {
            var horizontalSize      = GetHorizontal ();
            var verticalSize        = GetVertical ();
            var pixelSizeHorizontal = horizontalSize / pixelCountHorizontal;
            var pixelSizeVertical   = verticalSize / pixelCountVertical;
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

        public Vector GetVertical () => new Vector (0, 0, GetDiagonal ().X3);

        public Vector GetDiagonal () => BottomRightCorner - TopLeftCorner;
    }
}