namespace RayTracing.Types.Properties
{
    public class Colour
    {
        public double Red { get; }
        public double Green { get; }
        public double Blue { get; }

        /// <inheritdoc />
        public Colour (double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}