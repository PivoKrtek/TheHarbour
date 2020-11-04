using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheHabourProject
{
    public static class Counter
    {
        public static int NumberOfDeclinedBoatsSinceStartOfSimulation { get; set; }
        public static int NumberOfDeclinedBoatsEachDay { get; set; }
        public static int DaysSinceStartOfSimulation { get; set; }
        public static int NumberOfNewBoatsPerUdate { get; set; }
               
        public static int TotalWeight()
        {
            var b1 = Harbour.BoatsInHarbour
                .Select(b => b.Weight)
                .Sum();

            return b1;
        }
        public static double AverageKmPerHour()
        {
            var b1 = Harbour.BoatsInHarbour
                .Select(b => b.MaximumSpeed)
                .Sum();
            return Math.Round(b1 / Harbour.BoatsInHarbour.Count(), 1);
        }
        public static int GetNumberOfFreeSpots()
        {
            List<FreeWharfSpace> lista = FreeWharfSpace.ListOfFreeWharfPlacesInHarbour();
            var f1 = lista
                .Select(f => f.LenghtOfFreePlaces)
                .Sum();

            return f1;
        }
        public static int GetNumberOfBoatType(string firstLetterIdentityNumber)
        {
            var b1 = Harbour.BoatsInHarbour
                .Where(b => b.IdentityNumber.StartsWith(firstLetterIdentityNumber))
                .ToList();

            return b1.Count();
        }
        public static double DeclinedBoatsPerYear()
        {
            return Math.Round(((NumberOfDeclinedBoatsSinceStartOfSimulation / (double)DaysSinceStartOfSimulation) * 365), 2);
        }
    }
}
