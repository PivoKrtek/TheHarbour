using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TheHabourProject
{
    public abstract class Boat
    {
        public string BoatType { get; set; }
        public string IdentityNumber { get; set; }
        public int Weight { get; set; }
        public double MaximumSpeed { get; set; }
        public int DaysLeftToStayInHarbour { get; set; }
        public int PlaceAtWharf { get; set; }
        public int NumberOfWharfPlacesNeededAtHarbour { get; set; }
        public Image ImageBoat { get; set; }
        public static Image CreateImage(string boatType)
        {
            Image boatImage = new Image();
            boatImage.Height = 75;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"Images\" + boatType + ".jpg");
            bitmap.EndInit();

            boatImage.Source = bitmap;
            return boatImage;
        }
        public virtual void PlaceBoatAtWharf(int bestWharfPlace)
        {
            foreach (var wharf in Harbour.WharfPlacesInHarbour)
            {
                if (wharf.Number == bestWharfPlace)
                {
                    wharf.WharfPlaceFree = false;
                    wharf.HalfPlaceFree = false;
                    wharf.DaysLeftForBoatInWharf = DaysLeftToStayInHarbour;
                    PlaceAtWharf = wharf.Number;
                }
                for (int i = 1; i < NumberOfWharfPlacesNeededAtHarbour; i++)
                {
                    if (wharf.Number == bestWharfPlace + i)
                    {
                        wharf.WharfPlaceFree = false;
                        wharf.HalfPlaceFree = false;
                        wharf.DaysLeftForBoatInWharf = DaysLeftToStayInHarbour;
                        break;
                    }
                }
            }
        }
        public virtual int CheckForBestPlaceInHarbour()
        {
            List<FreeWharfSpace> list = FreeWharfSpace.ListOfFreeWharfPlacesInHarbour();

            var q1 = list
                .Where(q => q.LenghtOfFreePlaces >= NumberOfWharfPlacesNeededAtHarbour)
                .OrderBy(q => (q.LenghtOfFreePlaces > NumberOfWharfPlacesNeededAtHarbour + 4 ? NumberOfWharfPlacesNeededAtHarbour + 5 : q.LenghtOfFreePlaces) + AsCloseSameDaysLeftInHarbour(q))
                .Select(q => StartOrEndPosition(q))
                .ToList();
                return q1[0];
        }
        public int StartOrEndPosition(FreeWharfSpace freeSpot)
        {
            double value = freeSpot.DaysLeftForBoatBeforeSpace / (double)DaysLeftToStayInHarbour;
            if (value < 1)
            {
                value = 2 - value;
            }
            double value2 = freeSpot.DaysLeftForBoatAfterSpace / (double)DaysLeftToStayInHarbour;
            if (value2 < 1)
            {
                value2 = 2 - value2;
            }
            if (value <= value2)
            { return freeSpot.StartPosition; }
            return freeSpot.StartPosition + freeSpot.LenghtOfFreePlaces - NumberOfWharfPlacesNeededAtHarbour;
        }
        public double AsCloseSameDaysLeftInHarbour(FreeWharfSpace freeSpot)
        {
            double value = freeSpot.DaysLeftForBoatBeforeSpace / (double)DaysLeftToStayInHarbour;
            if (value < 1)
            {
                value = 2 - value;
            }
            double value2 = freeSpot.DaysLeftForBoatAfterSpace / (double)DaysLeftToStayInHarbour;
            if (value2 < 1)
            {
                value2 = 2 - value2;
            }
            if (value == 1 && value2 == 1)
            { return 0.9; }
            else if (value <= value2)
            { return value; }
            return value2;
        }
        public virtual bool IsTherePLaceForBoatInHarbour()
        {
            List<FreeWharfSpace> list = FreeWharfSpace.ListOfFreeWharfPlacesInHarbour();
            var l1 = list
                .Where(l => l.LenghtOfFreePlaces >= NumberOfWharfPlacesNeededAtHarbour)
                .ToList();

            return l1.Count() > 0;
        }
        public static string GetIdentityNumber(string initialLetter)
        {
            Random random = new Random();
            string identityNumber = initialLetter;
            for (int i = 0; i < 3; i++)
            {
                int number = random.Next(0, 26);
                identityNumber += (Alphabet)number;
            }
            return identityNumber;
        }
        public static List<Boat> CreateNewBoats(int numberOfBoats)
        {
            Random random = new Random();
            List<Boat> boats = new List<Boat>();
            for (int i = 0; i < numberOfBoats; i++)
            {
                int whichBoat = random.Next(1, 6);
                switch (whichBoat)
                {
                    case 1:
                        boats.Add(new RowingBoat());
                        break;
                    case 2:
                        boats.Add(new MotorBoat());
                        break;
                    case 3:
                        boats.Add(new SailingBoat());
                        break;
                    case 4:
                        boats.Add(new Catamaran());
                        break;
                    case 5:
                        boats.Add(new CargoShip());
                        break;
                    default:
                        break;
                }
            }
            return boats;
        }

    }
}
