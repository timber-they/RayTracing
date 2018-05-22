using System.Collections.Generic;

using RayTracing.Types.Objects;


namespace RayTracing.Types.Observation
{
    public class Observator
    {
        /// <inheritdoc />
        public Observator (Vector location, Frame frame)
        {
            Location = location;
            Frame    = frame;
        }

        public Vector Location { get; set; }
        public Frame  Frame    { get; set; }

        public List <Ray> GetRays (int pixelsHorizontal, int pixelsVertical)
        {
            var rays = new List <Ray> ();

            for (var vertical = 0; vertical < pixelsVertical; vertical++)
                for (var horizontal = 0; horizontal < pixelsHorizontal; horizontal++)
                {
                    var pixelCenter = Frame.GetPixelCenter (horizontal, vertical, pixelsHorizontal, pixelsVertical);
                    var direction   = (pixelCenter - Location).Unit ();
                    var ray         = new Ray (Location, direction);

                    rays.Add (ray);
                }

            return rays;
        }
    }
}