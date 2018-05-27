using System;
using System.Collections.Generic;


namespace RayTracing.Misc
{
    public static class Extension
    {
        public static double Square (this double d) => d * d;

        public static List <T> ToList <T> (this Tuple <T> tuple) => new List <T> {tuple.Item1};

        public static List <T> ToList <T> (this Tuple <T, T> tuple) => new List <T> {tuple.Item1, tuple.Item2};

        public static List <T> ToList <T> (this Tuple <T, T, T> tuple) =>
            new List <T> {tuple.Item1, tuple.Item2, tuple.Item3};

        public static List <T> ToList <T> (this Tuple <T, T, T, T> tuple) =>
            new List <T> {tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4};

        public static List <T> ToList <T> (this Tuple <T, T, T, T, T> tuple) =>
            new List <T> {tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5};

        public static List <T> ToList <T> (this Tuple <T, T, T, T, T, T> tuple) =>
            new List <T> {tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6};

        public static List <T> ToList <T> (this (T, T) tuple) => new List <T> {tuple.Item1, tuple.Item2};

        public static List <T> ToList <T> (this (T, T, T) tuple) =>
            new List <T> {tuple.Item1, tuple.Item2, tuple.Item3};

        public static List <T> ToList <T> (this (T, T, T, T) tuple) =>
            new List <T> {tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4};

        public static List <T> ToList <T> (this (T, T, T, T, T) tuple) =>
            new List <T> {tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5};

        public static List <T> ToList <T> (this (T, T, T, T, T, T) tuple) =>
            new List <T> {tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6};
    }
}