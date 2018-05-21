using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects
{
    public abstract class Object
    {
        /// <inheritdoc />
        protected Object (Vector center, Surface surface)
        {
            Center = center;
            Surface = surface;
        }

        public Vector Center { get; }
        public Surface Surface { get; }

        public abstract double? Intersect (Ray ray);
    }
}