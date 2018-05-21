using System;


namespace RayTracing.Types
{
    public class Vector
    {
        public double X1 { get; }
        public double X2 { get; }
        public double X3 { get; }

        /// <inheritdoc />
        public Vector (double x1, double x2, double x3)
        {
            X1 = x1;
            X2 = x2;
            X3 = x3;
        }

        public double Abs () => Math.Sqrt (X1 * X1 + X2 * X2 + X3 * X3);

        public double ScalarProduct (Vector b) => X1 * b.X1 + X2 * b.X2 + X3 * b.X3;

        public Vector VectorProduct (Vector b) =>
            new Vector (X2 * b.X3 - X3 * b.X2, X3 * b.X1 - X1 * b.X3, X1 * b.X2 - X2 * b.X1);

        public Vector Multiply (double b) => new Vector (X1 * b, X2 * b, X3 * b);

        public Vector Divide (double b) => new Vector(X1 / b, X2 / b, X3 / b);

        public double Square () => Abs () * Abs ();

        public Vector Add (Vector b) => new Vector (X1 + b.X1, X2 + b.X2, X3 + b.X3);

        public Vector Subtract (Vector b) => new Vector (X1 - b.X1, X2 - b.X2, X3 - b.X3);

        public static Vector operator + (Vector a, Vector b) => a.Add (b);

        public static Vector operator - (Vector a, Vector b) => a.Subtract (b);

        public static double operator * (Vector a, Vector b) => a.ScalarProduct (b);

        public static Vector operator * (Vector a, double b) => a.Multiply (b);

        public static Vector operator * (double a, Vector b) => b.Multiply (a);

        public static Vector operator / (Vector a, double b) => a.Divide (b);
    }
}