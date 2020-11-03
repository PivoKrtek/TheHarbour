using System;
using System.Collections.Generic;
using System.Text;

namespace TheHabourProject
{
    public static class Converter
    {
        public static double KnotToKmPerHr(int knot)
        {
            return Math.Round((knot * (double)1.852), 1);
        }

        public static double FeetToMeter(int feet)
        {
            return Math.Round((feet * (double)0.3048), 1);
        }
    }
}
