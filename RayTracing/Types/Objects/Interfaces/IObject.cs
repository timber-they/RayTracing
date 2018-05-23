using RayTracing.Types.Properties;


namespace RayTracing.Types.Objects.Interfaces
{
    public interface IObject
    {
        Surface Surface { get; }

        double? Intersect (Ray ray);
        Ray Reflect (Ray ray, double intensity, double? tEvaluated = null);
        Ray Reflect (Ray ray, double intensity, Vector y);
        (double, double)? Intersections (Ray ray);
    }
}