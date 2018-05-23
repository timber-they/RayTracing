namespace RayTracing.Types.Objects.Interfaces
{
    public interface ILightSource : IObject
    {
        double Intensity { get; }
        Vector GetDirection (Vector point);
        double GetDistance (Vector point);
        double GetRemainingIntensity (Vector point);
        double GetRemainingIntensity (double distance);
    }
}