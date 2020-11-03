using System;
using System.Collections.Generic;
using System.Linq;

namespace TheHabourProject
{
    public class RowingBoat : Boat
    {
        public int MaximumPassengers { get; set; }

        public RowingBoat()
        {
            Random random = new Random();
            IdentityNumber = Boat.GetIdentityNumber("R-");
            Weight = random.Next(100, 301);
            MaxKmPerHour = Converter.KnotToKmPerHr(random.Next(1, 4));
            DaysLeftToStayInHarbour = 1;
            MaximumPassengers = random.Next(1, 7);
            PlaceAtWharf = 0;
            NumberOfWharfPlacesNeededAtHarbour = 1;
            BoatType = "Rowingboat";
        }
        public RowingBoat(string[] stringArrayFromFile)
        {
            IdentityNumber = stringArrayFromFile[0];
            Weight = int.Parse(stringArrayFromFile[1]);
            MaxKmPerHour = double.Parse(stringArrayFromFile[2]);
            DaysLeftToStayInHarbour = int.Parse(stringArrayFromFile[3]);
            MaximumPassengers = int.Parse(stringArrayFromFile[4]);
            PlaceAtWharf = int.Parse(stringArrayFromFile[5]);
            NumberOfWharfPlacesNeededAtHarbour = 1;
            BoatType = "Rowingboat";
        }

        public override void PlaceBoatAtWharf(int bestWharfPlace)
        {
            foreach (var wharf in Harbour.WharfPlacesInHarbour)
            {
                if (wharf.Number == bestWharfPlace)
                {
                    if (wharf.HalfPlaceFree && wharf.WharfPlaceFree)
                    {
                        wharf.WharfPlaceFree = false;
                    }
                    else
                    {
                        wharf.HalfPlaceFree = false;
                    }
                    PlaceAtWharf = wharf.Number;
                    wharf.DaysLeftForBoatInWharf = DaysLeftToStayInHarbour;

                }
            }

        }
        public override bool IsTherePLaceForBoatInHarbour()
        {
            int halfPlaceSpot = FreeDockPlace.WhereIsHalfPlace();
            if (halfPlaceSpot > 0)
            { return true; }
            else
            {
                var l1 = Harbour.FreeWharfPlacesInHarbour
                .Where(l => l.LenghtOfFreePlaces >= NumberOfWharfPlacesNeededAtHarbour)
                .ToList();

                return l1.Count() > 0;
            }
        }

        public override int CheckForBestPlaceInHarbour()
        {
            int halfPlaceSpot = FreeDockPlace.WhereIsHalfPlace();
            if (halfPlaceSpot > 0)
            { return halfPlaceSpot; }
            else
            {
                List<FreeDockPlace> list = FreeDockPlace.ListOfFreeWharfPlacesInHarbour();

                var q1 = list
                .Where(q => q.LenghtOfFreePlaces >= NumberOfWharfPlacesNeededAtHarbour)
                .OrderBy(q => (q.LenghtOfFreePlaces > NumberOfWharfPlacesNeededAtHarbour + 4 ? NumberOfWharfPlacesNeededAtHarbour + 5 : q.LenghtOfFreePlaces) + AsCloseSameDaysLeftInHarbour(q))
                .Select(q => StartOrEndPosition(q))
                .ToList();
                return q1[0];

            }
        }
    }
}

