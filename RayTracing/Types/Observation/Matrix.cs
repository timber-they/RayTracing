using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using RayTracing.Types.Objects;

using Object = RayTracing.Types.Objects.Object;


namespace RayTracing.Types.Observation
{
    public class Matrix
    {
        public List <Object> Objects      { get; }
        public List <Vector> LightSources { get; }
        public Observator    Observator   { get; set; }

        public Matrix (List <Object> objects, List <Vector> lightSources, Observator observator)
        {
            Objects      = objects;
            LightSources = lightSources;
            Observator   = observator;
        }

        public Bitmap GenerateBitmap (int depth, int pixelsHorizontal, int pixelsVertical)
        {
            var initalRays = Observator.GetRays (pixelsHorizontal, pixelsVertical);
            var bmp        = new Bitmap (pixelsHorizontal, pixelsVertical);

            for (var i = 0; i < initalRays.Count; i++)
            {
                var colouredRay = GenerateColouredRay (depth, initalRays [i]);
                var x           = i % pixelsHorizontal;
                var y           = i / pixelsHorizontal;
                bmp.SetPixel (x, y, colouredRay.Colour.ToColor ());
            }

            return bmp;
        }


        private Ray GenerateColouredRay (int currentDepth, Ray ray)
        {
            while (true)
            {
                if (currentDepth <= 0)
                    return ray;

                var    minDistance       = double.MaxValue;
                Object minDistanceObject = null;
                foreach (var o in Objects)
                {
                    var distance = o.Intersect (ray);
                    if (distance == null || distance >= minDistance)
                        continue;
                    minDistance       = distance.Value;
                    minDistanceObject = o;
                }

                if (minDistanceObject == null)
                    return ray;

                currentDepth = currentDepth - 1;
                ray          = minDistanceObject.Reflect (ray, minDistance);
            }
        }
    }
}