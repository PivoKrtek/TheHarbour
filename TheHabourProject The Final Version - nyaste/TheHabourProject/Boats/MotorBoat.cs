using System;
using System.Collections.Generic;
using System.Text;

namespace TheHabourProject
{
    public class MotorBoat : Boat
    {
        public int NumberOfHorsepower { get; set; }

        public MotorBoat()
        {
            Random random = new Random();
            IdentityNumber = Boat.GetIdentityNumber("M-");
            Weight = random.Next(200, 3001);
            MaximumSpeed = Converter.KnotToKmPerHr(random.Next(1, 61));
            DaysLeftToStayInHarbour = 3;
            NumberOfHorsepower = random.Next(10, 1001);
            PlaceAtWharf = 0;
            NumberOfWharfPlacesNeededAtHarbour = 1;
            BoatType = "Motorboat";
        }
        public MotorBoat(string[] stringArrayFromFile)
        {
            IdentityNumber = stringArrayFromFile[0];
            Weight = int.Parse(stringArrayFromFile[1]);
            MaximumSpeed = double.Parse(stringArrayFromFile[2]);
            DaysLeftToStayInHarbour = int.Parse(stringArrayFromFile[3]);
            NumberOfHorsepower = int.Parse(stringArrayFromFile[4]);
            PlaceAtWharf = int.Parse(stringArrayFromFile[5]);
            NumberOfWharfPlacesNeededAtHarbour = 1;
            BoatType = "Motorboat";
        }

    }
}
