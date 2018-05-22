using System.Drawing;


namespace RayTracing.Types.Properties
{
    public class Colour
    {
        public double Red   { get; }
        public double Green { get; }
        public double Blue  { get; }

        /// <inheritdoc />
        public Colour (double red, double green, double blue)
        {
            Red   = red;
            Green = green;
            Blue  = blue;
        }

        public static Colour operator + (Colour colour1, Colour colour2) =>
            new Colour (colour1.Red + colour2.Red, colour1.Green + colour2.Green, colour1.Blue + colour2.Blue);

        public static Colour operator * (Colour colour, double factor) =>
            new Colour (colour.Red * factor, colour.Green * factor, colour.Blue * factor);

        public static Colour operator * (double factor, Colour colour) =>
            new Colour (colour.Red * factor, colour.Green * factor, colour.Blue * factor);

        public Color ToColor () => Color.FromArgb ((int) Red, (int) Green, (int) Blue);
    }
}