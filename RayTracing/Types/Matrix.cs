using System.Collections.Generic;

using RayTracing.Types.Objects;


namespace RayTracing.Types
{
    public class Matrix
    {
        public List <Object> Objects { get; }
        public List <Vector> LightSources { get; }

        public Matrix (List <Object> objects, List <Vector> lightSources)
        {
            Objects = objects;
            LightSources = lightSources;
        }
    }
}