using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace TheHabourProject
{
    public class SailingBoat : Boat
    {
        public double BoatLenghtInMeter { get; set; }
        public SailingBoat()
        {
            Random random = new Random();
            IdentityNumber = Boat.GetIdentityNumber("S-");
            Weight = random.Next(800, 6001);
            MaximumSpeed = Converter.KnotToKmPerHr(random.Next(1, 13));
            DaysLeftToStayInHarbour = 4;
            BoatLenghtInMeter = Converter.FeetToMeter(random.Next(10, 61));
            PlaceAtWharf = 0;
            NumberOfWharfPlacesNeededAtHarbour = 2;
            BoatType = "Sailingboat";
        }
        public SailingBoat(string[] stringArrayFromFile)
        {
            IdentityNumber = stringArrayFromFile[0];
            Weight = int.Parse(stringArrayFromFile[1]);
            MaximumSpeed = double.Parse(stringArrayFromFile[2]);
            DaysLeftToStayInHarbour = int.Parse(stringArrayFromFile[3]);
            BoatLenghtInMeter = double.Parse(stringArrayFromFile[4]);
            PlaceAtWharf = int.Parse(stringArrayFromFile[5]);
            NumberOfWharfPlacesNeededAtHarbour = 2;
            BoatType = "Sailingboat";
           
        }
                
    }
}
