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
        public List <Object> Objects    { get; }
        public Observator    Observator { get; set; }

        public Matrix (List <Object> objects, List <LightSource> lightSources, Observator observator)
        {
            Objects = objects;
            Objects.AddRange (lightSources);
            Observator = observator;
        }

        public Bitmap GenerateBitmap (int depth, int pixelsHorizontal, int pixelsVertical)
        {
            if (Math.Abs (pixelsHorizontal / (double) pixelsVertical -
                          Observator.Frame.GetWidth () / Observator.Frame.GetHeight ()) >
                0.1)
                throw new InvalidOperationException ("Invalid proportions - they have to match the frame proportions.");
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

        public Bitmap GenerateBitmap (int depth, int pixelsHorizontal) => GenerateBitmap (
            depth, pixelsHorizontal,
            (int) (Observator.Frame.GetHeight () / Observator.Frame.GetWidth () * pixelsHorizontal));


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

                switch (minDistanceObject)
                {
                    case null:
                        return ray;
                    case LightSource lightSource:
                        ray.Colour = lightSource.Surface.Colour;
                        return ray;
                    default:
                        currentDepth = currentDepth - 1;
                        ray          = minDistanceObject.Reflect (ray, minDistance);
                        break;
                }
            }
        }
    }
}