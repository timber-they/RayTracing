namespace RayTracing.Types.Properties
{
    public class Surface
    {
        public double ReflectionAmount { get; }
        public Colour Colour { get; }

        /// <inheritdoc />
        public Surface (double reflectionAmount, Colour colour)
        {
            ReflectionAmount = reflectionAmount;
            Colour = colour;
        }
    }
}