using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TheHabourProject
{
    public static class Harbour
    {
        public static List<DockPlace> WharfPlacesInHarbour { get; set; }

        public static List<Boat> BoatsInHarbour { get; set; }

        public static List<FreeDockPlace> FreeWharfPlacesInHarbour { get; set; }

        public static void CreateWharfPlacesInHArbour()
        {
            WharfPlacesInHarbour = new List<DockPlace>();
            for (int i = 1; i < 65; i++)
            {
                WharfPlacesInHarbour.Add(new DockPlace(i));
            }
        }
        
        public static void DecreaseDaysLeftForBoatsAtWharf()
        {
            foreach (var wharfPlace in Harbour.WharfPlacesInHarbour)
            {
                if (wharfPlace.DaysLeftForBoatInWharf > 0)
                {
                    wharfPlace.DaysLeftForBoatInWharf--;
                    if (wharfPlace.DaysLeftForBoatInWharf == 0)
                    {
                        wharfPlace.HalfPlaceFree = true;
                        wharfPlace.WharfPlaceFree = true;
                    }
                }
            }
        }
        public static List<Boat> DecreaseDaysLeftForBoatsInHarbour()
        {
            List<Boat> boatsInHarbour = new List<Boat>();
            foreach (var boat in Harbour.BoatsInHarbour)
            {
                if (boat.DaysLeftToStayInHarbour > 0)
                {
                    boat.DaysLeftToStayInHarbour--;

                }
                if (boat.DaysLeftToStayInHarbour != 0)
                {
                    boatsInHarbour.Add(boat);
                }
                else if (boat.DaysLeftToStayInHarbour == 0)
                {
                    if (boat.ImageBoat != null)
                    { boat.ImageBoat.Visibility = Visibility.Hidden; }
                }
            }
            return boatsInHarbour;
        }
        public static List<string> PrintHarbourInfo(int choice)
        {
            List<string> info = new List<string>();
            
            info.Add($"Plats\tBåttyp\t\tBåt-ID\tVikt\t Maxhastighet\tÖvrigt\n");
                       
            var b1 = Harbour.BoatsInHarbour
                    .OrderBy(b => b.PlaceAtWharf);

            
            if (choice == 1)
            {
                var b2 = b1
                      .Where(b => b.PlaceAtWharf < 33);
                foreach (var boat in b2)
                {
                    if (boat is SailingBoat)
                    { info.Add($"{boat.PlaceAtWharf}-{boat.PlaceAtWharf + boat.NumberOfWharfPlacesNeededAtHarbour-1}\tSegelbåt\t\t{boat.IdentityNumber}\t{boat.Weight} kg\t  {boat.MaximumSpeed} km/h  \t{((SailingBoat)boat).BoatLenghtInMeter} m lång\n"); }
                    else if (boat is Catamaran)
                    { info.Add($"{boat.PlaceAtWharf}-{boat.PlaceAtWharf + boat.NumberOfWharfPlacesNeededAtHarbour - 1}\tKatamaran\t{boat.IdentityNumber}\t{boat.Weight} kg\t  {boat.MaximumSpeed} km/h  \t{((Catamaran)boat).NumberOfBedsInBoat} sängar ombord\n"); }
                    else if (boat is CargoShip)
                    { info.Add($"{boat.PlaceAtWharf}-{boat.PlaceAtWharf + boat.NumberOfWharfPlacesNeededAtHarbour - 1}\tLastfartyg\t\t{boat.IdentityNumber}\t{boat.Weight} kg\t  {boat.MaximumSpeed} km/h  \t{((CargoShip)boat).NumberOfContainersOnTheShip} containers ombord\n"); }
                    else if (boat is RowingBoat)
                    { info.Add($"{boat.PlaceAtWharf}\tRoddbåt\t\t{boat.IdentityNumber}\t{boat.Weight} kg\t  {boat.MaximumSpeed} km/h  \tMax {((RowingBoat)boat).MaximumPassengers} st passagerare\n"); }
                    else if (boat is MotorBoat)
                    { info.Add($"{boat.PlaceAtWharf}\tMotorbåt\t\t{boat.IdentityNumber}\t{boat.Weight} kg\t  {boat.MaximumSpeed} km/h  \t{((MotorBoat)boat).NumberOfHorsepower} hästkrafter\n"); }
                }
            }
            else if (choice == 2)
            {
                var b2 = b1
                    .Where(b => b.PlaceAtWharf > 32);
                foreach (var boat in b2)
                {
                    if (boat is SailingBoat)
                    { info.Add($"{boat.PlaceAtWharf}-{boat.PlaceAtWharf + boat.NumberOfWharfPlacesNeededAtHarbour - 1}\tSegelbåt\t\t{boat.IdentityNumber}\t{boat.Weight} kg\t  {boat.MaximumSpeed} km/h  \t{((SailingBoat)boat).BoatLenghtInMeter} m lång\n"); }
                    else if (boat is Catamaran)
                    { info.Add($"{boat.PlaceAtWharf}-{boat.PlaceAtWharf + boat.NumberOfWharfPlacesNeededAtHarbour - 1}\tKatamaran\t{boat.IdentityNumber}\t{boat.Weight} kg\t  {boat.MaximumSpeed} km/h  \t{((Catamaran)boat).NumberOfBedsInBoat} {(((Catamaran)boat).NumberOfBedsInBoat > 0 ? "sängar" : "säng")} ombord\n"); }
                    else if (boat is CargoShip)
                    { info.Add($"{boat.PlaceAtWharf}-{boat.PlaceAtWharf + boat.NumberOfWharfPlacesNeededAtHarbour - 1}\tLastfartyg\t\t{boat.IdentityNumber}\t{boat.Weight} kg\t  {boat.MaximumSpeed} km/h  \t{((CargoShip)boat).NumberOfContainersOnTheShip} containers ombord\n"); }
                    else if (boat is RowingBoat)
                    { info.Add($"{boat.PlaceAtWharf}\tRoddbåt\t\t{boat.IdentityNumber}\t{boat.Weight} kg\t  {boat.MaximumSpeed} km/h  \tMax {((RowingBoat)boat).MaximumPassengers} st passagerare\n"); }
                    else if (boat is MotorBoat)
                    { info.Add($"{boat.PlaceAtWharf}\tMotorbåt\t\t{boat.IdentityNumber}\t{boat.Weight} kg\t  {boat.MaximumSpeed} km/h  \t{((MotorBoat)boat).NumberOfHorsepower} hästkrafter\n"); }
                }
            }

            return info;
        }
    }
}
