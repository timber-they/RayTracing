namespace RayTracing.Types.Properties
{
    public class Surface
    {
        public double ReflectionAmount { get; }
        public Colour Colour           { get; }

        /// <inheritdoc />
        public Surface (double reflectionAmount, Colour colour)
        {
            ReflectionAmount = reflectionAmount;
            Colour           = colour;
        }

        /// <inheritdoc />
        public override bool Equals (object obj) => obj is Surface surface && Equals (surface);

        protected bool Equals (Surface other) =>
            ReflectionAmount.Equals (other.ReflectionAmount) && Equals (Colour, other.Colour);

        /// <inheritdoc />
        public override int GetHashCode ()
        {
            unchecked
            {
                return (ReflectionAmount.GetHashCode () * 397) ^ (Colour != null ? Colour.GetHashCode () : 0);
            }
        }
    }
}