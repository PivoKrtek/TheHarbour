using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace TheHabourProject
{
    public class Catamaran : Boat
    {
        public int NumberOfBedsInBoat { get; set; }

        public Catamaran()
        {
            Random random = new Random();
            IdentityNumber = Boat.GetIdentityNumber("K-");
            Weight = random.Next(1200, 8001);
            MaximumSpeed = Converter.KnotToKmPerHr(random.Next(1, 13));
            DaysLeftToStayInHarbour = 3;
            NumberOfBedsInBoat = random.Next(1, 5);
            PlaceAtWharf = 0;
            NumberOfWharfPlacesNeededAtHarbour = 3;
            BoatType = "Catamaran";
        }
        public Catamaran(string[] stringArrayFromFile)
        {
            IdentityNumber = stringArrayFromFile[0];
            Weight = int.Parse(stringArrayFromFile[1]);
            MaximumSpeed = double.Parse(stringArrayFromFile[2]);
            DaysLeftToStayInHarbour = int.Parse(stringArrayFromFile[3]);
            NumberOfBedsInBoat = int.Parse(stringArrayFromFile[4]);
            PlaceAtWharf = int.Parse(stringArrayFromFile[5]);
            NumberOfWharfPlacesNeededAtHarbour = 3;
            BoatType = "Catamaran";
        }

        

    }
}
