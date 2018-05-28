using System;
using System.Collections.Generic;

using RayTracing.Misc;
using RayTracing.Types.Objects.Interfaces;
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
        /// <param name="count">Has to be the cubical of something</param>
        /// <param name="intensity"></param>
        /// <returns></returns>
        public static List <CubicalLightSource> GenerateLightSources (Plain bottom, int count, double intensity = 1)
        {
            var oneDimensionalCount = Math.Pow (count, 1.0 / 3);
            if (Math.Abs (
                    oneDimensionalCount - (int) Math.Round (oneDimensionalCount, 0, MidpointRounding.AwayFromZero)) >
                0.000001)
                throw new Exception ("Count has to be the cubical of something");
            var xChunk  = (bottom.Corners.Item2 - bottom.Corners.Item1) / oneDimensionalCount;
            var yChunk  = (bottom.Corners.Item4 - bottom.Corners.Item1) / oneDimensionalCount;
            var zChunk  = bottom.GetNormalVector () * xChunk.Abs ();
            var corner1 = bottom.Corners.Item1;

            var fin = new List <CubicalLightSource> ();
            for (var x = 0; x < oneDimensionalCount; x++)
                for (var y = 0; y < oneDimensionalCount; y++)
                    for (var z = 0; z < oneDimensionalCount; z++)
                    {
                        var corner = corner1 + x * xChunk + y * yChunk + z * zChunk;
                        var lightSource = new CubicalLightSource (new Plain (bottom.Surface,
                                                                             corner,
                                                                             corner + xChunk,
                                                                             corner + xChunk - yChunk,
                                                                             corner - yChunk), intensity / count);

                        fin.Add (lightSource);
                    }

            return fin;
        }
    }
}