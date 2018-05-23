using RayTracing.Types.Objects.Interfaces;
using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects
{
    public abstract class Object : IObject
    {
        /// <inheritdoc />
        protected Object (Surface surface)
        {
            Surface = surface;
        }

        public Surface Surface { get; }

        public abstract double? Intersect (Ray ray);
        public abstract Ray Reflect (Ray ray, double intensity, double? tEvaluated = null);
        public abstract Ray Reflect (Ray ray, double intensity, Vector y);
        public abstract (double, double)? Intersections (Ray ray);
    }
}