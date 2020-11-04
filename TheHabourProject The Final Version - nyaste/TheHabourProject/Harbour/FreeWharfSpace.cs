using System.Collections.Generic;
using System.Linq;

namespace TheHabourProject
{
    public class FreeWharfSpace
    {
        public int StartPosition { get; set; }
        public int LenghtOfFreePlaces { get; set; }
        public int DaysLeftForBoatBeforeSpace { get; set; }
        public int DaysLeftForBoatAfterSpace { get; set; }

        public FreeWharfSpace(int start, int lenght, int daysLeftBefore, int daysLeftAfter)
        {
            StartPosition = start;
            LenghtOfFreePlaces = lenght;
            DaysLeftForBoatBeforeSpace = daysLeftBefore;
            DaysLeftForBoatAfterSpace = daysLeftAfter;
        }
        public static int WhereIsHalfPlace()
        {
            var w1 = Harbour.WharfPlacesInHarbour
            .Where(w => !w.WharfPlaceFree && w.HalfPlaceFree)
            .Select(w => w.Number)
            .ToList();

            if (w1.Count > 0)
            {
                return w1[0];
            }
            else
            {
                return 0;
            }
        }
        public static List<FreeWharfSpace> ListOfFreeWharfPlacesInHarbour()
        {
            List<FreeWharfSpace> lista = new List<FreeWharfSpace>();

            for (int i = 1; i < Harbour.WharfPlacesInHarbour.Count + 1;)
            {
                int count = 0;
                if (Harbour.WharfPlacesInHarbour[i - 1].WharfPlaceFree)
                {
                    count++;
                    
                    if (Harbour.WharfPlacesInHarbour[i - 1].Number != 32)
                    {
                        for (int j = i; j < Harbour.WharfPlacesInHarbour.Count; j++)
                        {
                            if (Harbour.WharfPlacesInHarbour[j].WharfPlaceFree)
                            { count++; }
                            else { break; }
                            if (Harbour.WharfPlacesInHarbour[j].Number == 32)
                            {
                                break;
                            }
                        }
                    }
                    
                    lista.Add(new FreeWharfSpace(i, count, DaysLeftBefore(i - 1), DaysLeftAfter(i + count)));
                }
                if (count > 0)
                { i += count; }
                else
                {
                    i++;
                }
            }
            return lista;
        }
        public static int DaysLeftBefore(int number)
        {
            if (number == 0)
            { return 1; }
            else if (number == 32)
            { return 4; }

            return Harbour.WharfPlacesInHarbour[number - 1].DaysLeftForBoatInWharf;
        }
        public static int DaysLeftAfter(int number)
        {
            if (number == 33)
            { return 4; }
            else if (number == 65)
            { return 4; }

            return Harbour.WharfPlacesInHarbour[number - 1].DaysLeftForBoatInWharf;

        }
    }
}
