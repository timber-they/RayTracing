using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Schema;

using RayTracing.Types.Objects;
using RayTracing.Types.Objects.Interfaces;


namespace RayTracing.Types.Observation
{
    public class Matrix
    {
        public List <IObject>      Objects      { get; }
        public List <ILightSource> LightSources { get; }
        public Observator          Observator   { get; set; }


        public Matrix (Observator observator, params IObject [] objects)
        {
            LightSources = objects.OfType <ILightSource> ().ToList ();
            Objects      = objects.ToList ().Except (LightSources).ToList ();
            Observator   = observator;
        }

        public Bitmap GenerateBitmap (
            int depth, int pixelsHorizontal, int pixelsVertical, Action <int> updateProgressAction)
        {
            if (Math.Abs (pixelsHorizontal / (double) pixelsVertical -
                          Observator.Frame.GetWidth () / Observator.Frame.GetHeight ()) >
                0.1)
                throw new InvalidOperationException ("Invalid proportions - they have to match the frame proportions.");
            var initalRays = Observator.GetRays (pixelsHorizontal, pixelsVertical);
            var bmp        = new Bitmap (pixelsHorizontal, pixelsVertical);

            var progress = 0;
            updateProgressAction (progress);

            initalRays.Select ((ray, i) => (ray, i)).ToList ().AsParallel ().ForAll (tuple =>
            {
                var ray = tuple.Item1;
                var i   = tuple.Item2;

                var colouredRay = GenerateColouredRay (depth, ray);
                var x           = i % pixelsHorizontal;
                var y           = i / pixelsHorizontal;
                lock (bmp)
                {
                    bmp.SetPixel (x, y, colouredRay.Colour.ToColor ());
                    progress++;
                    if (progress % 10 == 0)
                        updateProgressAction (progress);
                }
            });

            return bmp;
        }

        public Bitmap GenerateBitmap (int depth, int pixelsHorizontal, Action <int> updateProgressAction) =>
            GenerateBitmap (depth, pixelsHorizontal,
                            (int) (Observator.Frame.GetHeight () / Observator.Frame.GetWidth () * pixelsHorizontal),
                            updateProgressAction);


        private Ray GenerateColouredRay (int currentDepth, Ray ray)
        {
            IObject lastObject = null;
            while (true)
            {
                if (currentDepth <= 0)
                    return ray;

                var     minDistance       = double.MaxValue;
                IObject minDistanceObject = null;
                foreach (var o in Objects.Concat (LightSources))
                {
                    var distance = o.Intersect (ray);
                    if (distance == null || distance >= minDistance || Equals (o, lastObject))
                        continue;
                    minDistance       = distance.Value;
                    minDistanceObject = o;
                }

                lastObject = minDistanceObject;

                switch (minDistanceObject)
                {
                    case null:
                        return ray;
                    case ILightSource lightSource:
                        ray.Colour = lightSource.Surface.Colour;
                        return ray;
                    default:
                        currentDepth--;
                        var point     = ray.Get (minDistance);
                        var intensity = GetIntensity (point);
                        ray = minDistanceObject.Reflect (ray, intensity, point);
                        break;
                }
            }
        }

        private double GetIntensity (Vector point)
        {
            var intensity = 0.0;
            foreach (var lightSource in LightSources)
            {
                var direction             = lightSource.GetDirection (point);
                var distanceToLightSource = lightSource.GetDistance (point);

                var ray = new Ray (point, direction);

                foreach (var o in Objects)
                {
                    var distanceToObject = o.Intersections (ray);
                    if (distanceToObject == null)
                        continue;
                    var pointsOnRay = (ray.Get (distanceToObject.Value.Item1),
                                       ray.Get (distanceToObject.Value.Item2));
                    var actualDistances = ((pointsOnRay.Item1 - point).Abs (), (pointsOnRay.Item2 - point).Abs ());
                    if ((actualDistances.Item1 >= distanceToLightSource || distanceToObject.Value.Item1 <= 0.001) &&
                        (actualDistances.Item2 >= distanceToLightSource || distanceToObject.Value.Item2 <= 0.001))
                        continue;
                    goto cont;
                }

                var remainingIntensity = lightSource.GetRemainingIntensity (distanceToLightSource);
                intensity += remainingIntensity;

                cont:;
            }

            return intensity > 1.0 ? 1.0 : intensity < 0.15 ? 0.15 : intensity;
        }
    }
}