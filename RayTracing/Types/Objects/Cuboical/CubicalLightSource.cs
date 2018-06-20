using System;
using System.Collections.Generic;

using RayTracing.Misc;
using RayTracing.Types.Objects.Interfaces;
using RayTracing.Types.Objects.Plains;
using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects.Cuboical
{
    public class CubicalLightSource : Cube, ILightSource
    {
        /// <inheritdoc />
        public CubicalLightSource (
            Surface surface, (Plain, Plain, Plain, Plain, Plain, Plain) plains, double intensity = 1) : base (
            surface, plains)
            => Intensity = intensity;

        /// <inheritdoc />
        public CubicalLightSource (
            Surface surface, Plain top, Plain front, Plain left, Plain back, Plain right, Plain bottom,
            double  intensity = 1) : base (
            surface, top, front, left, back, right, bottom)
            => Intensity = intensity;

        /// <inheritdoc />
        public CubicalLightSource (
            Surface surface, Vector hlh, Vector hhh, Vector lhh, Vector llh, Vector hll, Vector hhl, Vector lhl,
            Vector  lll,     double intensity = 1) : base (surface, hlh, hhh, lhh, llh, hll, hhl, lhl, lll)
            => Intensity = intensity;

        /// <inheritdoc />
        public CubicalLightSource (Plain bottom, double intensity = 1) : base (bottom)
            => Intensity = intensity;

        /// <inheritdoc />
        public double Intensity { get; }

        /// <inheritdoc />
        public Vector GetDirection (Vector point) => (GetCenter () - point).Unit ();

        /// <inheritdoc />
        public double GetDistance (Vector point) => (GetCenter () - point).Abs ();

        /// <inheritdoc />
        public double GetRemainingIntensity (Vector point) => GetRemainingIntensity (GetDistance (point));

        /// <inheritdoc />
        public double GetRemainingIntensity (double distance) => Intensity / (4 * Constants.PI * distance);

        /// <summary>
        /// Generates a cube of lightsources out of smaller ones
        /// </summary>
        /// <param name="bottom"></param>
        /// <param name="count">The onedimensional count</param>
        /// <param name="intensity"></param>
        /// <returns></returns>
        public static List <CubicalLightSource> GenerateLightSources (Plain bottom, int count, double intensity = 1)
        {
            if (count <= 1)
                return new List <CubicalLightSource> {new CubicalLightSource (bottom, intensity)};

            var xChunk  = (bottom.Corners.Item2 - bottom.Corners.Item1) / count;
            var yChunk  = (bottom.Corners.Item4 - bottom.Corners.Item1) / count;
            var zChunk  = bottom.GetNormalVector () * xChunk.Abs ();
            var corner1 = bottom.Corners.Item1;

            var fin = new List <CubicalLightSource> ();
            for (var x = 0; x < count; x++)
                for (var y = 0; y < count; y++)
                    for (var z = 0; z < count; z++)
                    {
                        if (x.Between (0, count - 1) && y.Between (0, count - 1) && z.Between (0, count - 1))
                            continue;
                        var corner = corner1 + x * xChunk + y * yChunk + z * zChunk;
                        var lightSource = new CubicalLightSource (new Plain (bottom.Surface,
                                                                             corner,
                                                                             corner + xChunk,
                                                                             corner + xChunk - yChunk,
                                                                             corner - yChunk), intensity / count);

                        fin.Add (lightSource);
                    }

            if (fin.Count != count.Cube () - (count - 2).Cube ())
                throw new Exception ("Invalid resulting count");

            return fin;
        }
    }
}