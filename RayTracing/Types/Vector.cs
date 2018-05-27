using System;

using RayTracing.Misc;


namespace RayTracing.Types
{
    public class Vector
    {
        public double X1 { get; }
        public double X2 { get; }
        public double X3 { get; }

        /// <inheritdoc />
        public Vector (double x1, double x2, double x3, bool unitVector = false)
        {
            if (!unitVector)
            {
                X1 = x1;
                X2 = x2;
                X3 = x3;
                return;
            }

            var n = Math.Sqrt (1 / (x1.Square () + x2.Square () + x3.Square ()));
            X1 = n * x1;
            X2 = n * x2;
            X3 = n * x3;
        }

        public double Abs () => Math.Sqrt (X1 * X1 + X2 * X2 + X3 * X3);

        public double ScalarProduct (Vector b) => X1 * b.X1 + X2 * b.X2 + X3 * b.X3;

        public Vector VectorProduct (Vector b) =>
            new Vector (X2 * b.X3 - X3 * b.X2, X3 * b.X1 - X1 * b.X3, X1 * b.X2 - X2 * b.X1);

        public Vector Multiply (double b) => new Vector (X1 * b, X2 * b, X3 * b);

        public Vector Divide (double b) => new Vector (X1 / b, X2 / b, X3 / b);

        public double Square () => X1 * X1 + X2 * X2 + X3 * X3;

        public Vector Add (Vector b) => new Vector (X1 + b.X1, X2 + b.X2, X3 + b.X3);

        public Vector Subtract (Vector b) => new Vector (X1 - b.X1, X2 - b.X2, X3 - b.X3);

        public Vector Unit () => new Vector (X1, X2, X3, true);

        public static Vector operator + (Vector a, Vector b) => a.Add (b);

        public static Vector operator - (Vector a, Vector b) => a.Subtract (b);

        public static double operator * (Vector a, Vector b) => a.ScalarProduct (b);

        public static Vector operator * (Vector a, double b) => a.Multiply (b);

        public static Vector operator * (double a, Vector b) => b.Multiply (a);

        public static Vector operator / (Vector a, double b) => a.Divide (b);

        public static bool operator == (Vector a, Vector b) => a?.Equals (b) ?? false;

        public static bool operator != (Vector a, Vector b) => !a?.Equals (b) ?? true;

        /// <inheritdoc />
        public override bool Equals (object obj) => obj is Vector vector && Equals (vector);

        protected bool Equals (Vector other) => Math.Abs (X1 - other.X1) < 0.0000000001 &&
                                                Math.Abs (X2 - other.X2) < 0.0000000001 &&
                                                Math.Abs (X3 - other.X3) < 0.0000000001;

        /// <inheritdoc />
        public override int GetHashCode ()
        {
            unchecked
            {
                var hashCode = Math.Round (X1, 5, MidpointRounding.AwayFromZero).GetHashCode ();
                hashCode = (hashCode * 397) ^ Math.Round (X2, 5, MidpointRounding.AwayFromZero).GetHashCode ();
                hashCode = (hashCode * 397) ^ Math.Round (X3, 5, MidpointRounding.AwayFromZero).GetHashCode ();
                return hashCode;
            }
        }

        /// <inheritdoc />
        public override string ToString () => $"{X1} / {X2} / {X3}";

        public static Vector Null () => new Vector (0, 0, 0);
    }
}