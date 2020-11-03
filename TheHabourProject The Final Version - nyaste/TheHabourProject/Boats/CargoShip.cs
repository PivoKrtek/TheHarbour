using System;

namespace TheHabourProject
{
    public class CargoShip : Boat
    {
        public int NumberOfContainersOnTheShip { get; set; }

        public CargoShip()
        {
            Random random = new Random();
            IdentityNumber = Boat.GetIdentityNumber("L-");
            Weight = random.Next(3000, 20001);
            MaximumSpeed = Converter.KnotToKmPerHr(random.Next(1, 21));
            DaysLeftToStayInHarbour = 6;
            NumberOfContainersOnTheShip = random.Next(0, 501);
            PlaceAtWharf = 0;
            NumberOfWharfPlacesNeededAtHarbour = 4;
            BoatType = "Cargoship";
        }
        public CargoShip(string[] stringArrayFromFile)
        {
            IdentityNumber = stringArrayFromFile[0];
            Weight = int.Parse(stringArrayFromFile[1]);
            MaximumSpeed = double.Parse(stringArrayFromFile[2]);
            DaysLeftToStayInHarbour = int.Parse(stringArrayFromFile[3]);
            NumberOfContainersOnTheShip = int.Parse(stringArrayFromFile[4]);
            PlaceAtWharf = int.Parse(stringArrayFromFile[5]);
            NumberOfWharfPlacesNeededAtHarbour = 4;
            BoatType = "Cargoship";
        }

    }
}
