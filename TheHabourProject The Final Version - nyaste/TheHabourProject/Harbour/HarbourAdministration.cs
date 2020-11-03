using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;

namespace TheHabourProject
{
    public class HarbourAdministration
    {
        public static List<Boat> DeclinedBoats { get; set; }
        public static List<Boat> BoatsComingToHarbour { get; set; }
        
        public static List<string> StringInfo()
        {            
            List<string> info = new List<String>();
                        
            info.Add($"DAG {Counter.DaysSinceStartOfSimulation}. Idag kom det {Counter.NumberOfNewBoatsPerUdate} nya båtar till hamnen.\n");
            info.Add($"I hamnen ligger nu:\n- Roddbåtar \t{Counter.GetNumberOfBoatType("R")} st\n- Motorbåtar \t{Counter.GetNumberOfBoatType("M")} st\n- Segelbåtar \t{Counter.GetNumberOfBoatType("S")} st\n- Katamaraner \t{Counter.GetNumberOfBoatType("K")} st\n- Lastfartyg \t{Counter.GetNumberOfBoatType("L")} st\n");
            info.Add($"Summan av alla båtars vikt: {Counter.TotalWeight()} kg.\n");
            info.Add($"Medelhastighet för alla båtar: {Counter.AverageKmPerHour()} km/h.\n");
            info.Add($"Antal hela lediga platser: {Counter.GetNumberOfFreeSpots()} st\nAntal lediga halvplatser (för roddbåt): {(FreeDockPlace.WhereIsHalfPlace() > 0 ? "1" : "0")} st.\n");
            info.Add($"Antal avvisade båtar för dagen: {Counter.NumberOfDeclinedBoatsEachDay} st\n");
            info.Add($"Antal avvisade båtar sedan simuleringens start: {Counter.NumberOfDeclinedBoatsSinceStartOfSimulation} st\nAvvisade båtar i genomsnitt per år: {Counter.DeclinedBoatsPerYear()} st/år.\n");
            if (Counter.NumberOfDeclinedBoatsEachDay > 0)
            {
                string s = "Avvisade båtar: ";
                foreach (var boat in HarbourAdministration.DeclinedBoats)
                {
                    s += $"{boat.IdentityNumber} ";
                }
                info.Add(s);
            }
            return info;
        }
        
        public static string[] ReadInformationFromFile()
        {
            string[] textlinesFromFile = File.ReadAllLines("theharbour.txt");

            return textlinesFromFile;
        }

        public static void CreatePreviousHarbourAndBoats(string[] data)
        {
            List<DockPlace> wharfPlacesInHarbour = new List<DockPlace>();
            for (int i = 0; i < 64; i++)
            {
                string[] splittedString = data[i].Split(';');
                wharfPlacesInHarbour.Add(new DockPlace(splittedString));
            }
            Harbour.WharfPlacesInHarbour = wharfPlacesInHarbour;

            string[] splitString = data[64].Split(';');
            Counter.DaysSinceStartOfSimulation = int.Parse(splitString[0]);
            Counter.NumberOfDeclinedBoatsSinceStartOfSimulation = int.Parse(splitString[1]);

            List<Boat> boatsInHarbour = new List<Boat>();
            for (int i = 65; i < data.Length; i++)
            {
                string[] splittedString = data[i].Split(';');
                if (splittedString[0].StartsWith('R'))
                { boatsInHarbour.Add(new RowingBoat(splittedString)); }
                else if (splittedString[0].StartsWith('M'))
                { boatsInHarbour.Add(new MotorBoat(splittedString)); }
                else if (splittedString[0].StartsWith('S'))
                { boatsInHarbour.Add(new SailingBoat(splittedString)); }
                else if (splittedString[0].StartsWith('K'))
                { boatsInHarbour.Add(new Catamaran(splittedString)); }
                else if (splittedString[0].StartsWith('L'))
                { boatsInHarbour.Add(new CargoShip(splittedString)); }
            }
            Harbour.BoatsInHarbour = boatsInHarbour;
        }

        public static void WriteInformationToFile(List<DockPlace> listWharf, List<Boat> boatsInHarbour)
        {
            
            StreamWriter sw = File.CreateText("theharbour.txt");
            foreach (var wharf in listWharf)
            {
                sw.WriteLine($"{wharf.Number};{wharf.HalfPlaceFree};{wharf.WharfPlaceFree};{wharf.DaysLeftForBoatInWharf}");
            }
            sw.WriteLine($"{Counter.DaysSinceStartOfSimulation};{Counter.NumberOfDeclinedBoatsSinceStartOfSimulation}");
            foreach (var boat in boatsInHarbour)
            {
                string s = $"{boat.IdentityNumber};{boat.Weight};{boat.MaxKmPerHour};{boat.DaysLeftToStayInHarbour};";
                if (boat is RowingBoat)
                { s += $"{((RowingBoat)boat).MaximumPassengers};{((RowingBoat)boat).PlaceAtWharf}"; }
                else if (boat is MotorBoat)
                { s += $"{((MotorBoat)boat).NumberOfHorsepower};{((MotorBoat)boat).PlaceAtWharf}"; }
                else if (boat is SailingBoat)
                { s += $"{((SailingBoat)boat).BoatLenghtInMeter};{((SailingBoat)boat).PlaceAtWharf}"; }
                else if (boat is Catamaran)
                { s += $"{((Catamaran)boat).NumberOfBedsInBoat};{((Catamaran)boat).PlaceAtWharf}"; }
                else if (boat is CargoShip)
                { s += $"{((CargoShip)boat).NumberOfContainersOnTheShip};{((CargoShip)boat).PlaceAtWharf}"; }

                sw.WriteLine(s);
            }
            sw.Close();
        }
        
    }
}
