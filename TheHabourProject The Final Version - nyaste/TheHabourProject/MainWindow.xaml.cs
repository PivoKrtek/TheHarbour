using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheHabourProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Counter.NumberOfDeclinedBoatsEachDay = 0;
            Counter.NumberOfDeclinedBoatsSinceStartOfSimulation = 0;
            Counter.NumberOfNewBoatsPerUdate = 5;
            //if file path doesent exist hide button
            if (!File.Exists("theharbour.txt"))
            {
                PreviousSimulation.Visibility = Visibility.Hidden;
            }
            HarbourAdministration.BoatsComingToHarbour = new List<Boat>();
            Harbour.CreateWharfPlacesInHArbour();
            Harbour.BoatsInHarbour = new List<Boat>();
        }

        private void Click_NewSimulation(object sender, RoutedEventArgs e)
        {
            if (File.Exists("theharbour.txt"))
            {
                PreviousSimulation.Visibility = Visibility.Visible;
            }
            
            foreach (var boat in Harbour.BoatsInHarbour)
            {
                boat.ImageBoat.Visibility = Visibility.Hidden;
            }
            InformationHarbour.Text = "";
            NorthHarbour.Text = "";
            SouthHarbour.Text = "";
                        
            Counter.DaysSinceStartOfSimulation = 0;
            Counter.NumberOfDeclinedBoatsSinceStartOfSimulation = 0;

            Harbour.CreateWharfPlacesInHArbour();
            Harbour.BoatsInHarbour = new List<Boat>();
        }
        private void Click_ContinuePreviousSimulation(object sender, RoutedEventArgs e)
        {
            HarbourAdministration.CreatePreviousHarbourAndBoats(HarbourAdministration.ReadInformationFromFile());
            CreateAndDisplayPicturesExeptForRowingBoat();
            CreateDisplayRowingBoatPictures();
            NumberOfBoatsPerDay.Text = Counter.NumberOfNewBoatsPerUdate.ToString();
            PrintInfoHarbour();
            PrintInfoNorthHarbour();
            PrintInfoSouthHarbor();
        }

        private void Click_OneDay(object sender, RoutedEventArgs e)
        {
            RunProgram(1);
        }

        private void Click_365Days(object sender, RoutedEventArgs e)
        {
            RunProgram(365);
        }

        private void Click_1000Days(object sender, RoutedEventArgs e)
        {
            RunProgram(1000);
        }

        private void RunProgram(int numberOfDays)
        {
            int count = 0;
            while (count < numberOfDays)
            {
                Counter.NumberOfDeclinedBoatsEachDay = 0;
                Harbour.DecreaseDaysLeftForBoatsAtWharf();
                Harbour.BoatsInHarbour = Harbour.DecreaseDaysLeftForBoatsInHarbour();
                HarbourAdministration.BoatsComingToHarbour = Boat.CreateNewBoats(Counter.NumberOfNewBoatsPerUdate);
                HarbourAdministration.DeclinedBoats = new List<Boat>();

                foreach (var boat in HarbourAdministration.BoatsComingToHarbour)
                {
                    bool placeFree = boat.IsTherePLaceForBoatInHarbour();
                    if (placeFree)
                    {
                        int bestPlace = boat.CheckForBestPlaceInHarbour();
                        boat.PlaceBoatAtWharf(bestPlace);
                        Harbour.BoatsInHarbour.Add(boat);
                    }
                    else
                    {
                        HarbourAdministration.DeclinedBoats.Add(boat);
                        Counter.NumberOfDeclinedBoatsEachDay++;
                    }
                }
                Counter.NumberOfDeclinedBoatsSinceStartOfSimulation += Counter.NumberOfDeclinedBoatsEachDay;
                Counter.DaysSinceStartOfSimulation++;
                if (Counter.DaysSinceStartOfSimulation > 0)
                { PreviousSimulation.Visibility = Visibility.Hidden; }
                count++;

            }
            CreateAndDisplayPicturesExeptForRowingBoat();
            CreateDisplayRowingBoatPictures();
            PrintInfoHarbour();
            PrintInfoNorthHarbour();
            PrintInfoSouthHarbor();
                        
            HarbourAdministration.WriteInformationToFile(Harbour.WharfPlacesInHarbour, Harbour.BoatsInHarbour);
        }

        public void CreateDisplayRowingBoatPictures()
        {
            int setTop = 0;
            double setLeft = 0;
            var r1 = Harbour.BoatsInHarbour
                .Where(r => r.BoatType == "Rowingboat")
                .GroupBy(r => r.PlaceAtWharf);

            foreach (var wharfPlace in r1)
            {
                int count = 0;
                foreach (var boat in wharfPlace)
                {
                    boat.ImageBoat = Boat.CreateImage(boat.BoatType);
                    if (count == 0)
                    {
                        if (boat.PlaceAtWharf > 32)
                        {
                            setTop = 170;
                            setLeft = Math.Round(25 + (boat.PlaceAtWharf - 33) * (double)40.8, 0);
                        }
                        else
                        {
                            setTop = 70;
                            setLeft = Math.Round(25 + (boat.PlaceAtWharf - 1) * (double)40.8, 0);
                        }
                    }
                    if (count == 1)
                    {
                        if (boat.PlaceAtWharf > 32)
                        {
                            setTop = 170;
                            setLeft = Math.Round(45.4 + (boat.PlaceAtWharf - 33) * (double)40.7, 0);
                        }
                        else
                        {
                            setTop = 70;
                            setLeft = Math.Round(45.4 + (boat.PlaceAtWharf - 1) * (double)40.7, 0);
                        }
                    }
                    count++;
                    Canvas2.Children.Add(boat.ImageBoat);
                    Canvas.SetLeft(boat.ImageBoat, setLeft);
                    Canvas.SetTop(boat.ImageBoat, setTop);
                }
            }
        }
        public void DisplayImage(Boat boat)
        {
            double setLeft = 0;
            int setTop = 0;
                        
            if (boat.PlaceAtWharf > 32)
            {
                setTop = 170;
                setLeft = Math.Round(25 + (boat.PlaceAtWharf - 33) * (double)40.8, 0);
            }
            else { setTop = 70;
                setLeft = Math.Round(25 + (boat.PlaceAtWharf - 1) * (double)40.8, 0);
            }
              
            Canvas2.Children.Add(boat.ImageBoat);
            Canvas.SetLeft(boat.ImageBoat, setLeft);
            Canvas.SetTop(boat.ImageBoat, setTop);
        }
        
        public void CreateAndDisplayPicturesExeptForRowingBoat()
        {
            foreach (var boat in Harbour.BoatsInHarbour)
            {
                if (boat.ImageBoat == null && boat.BoatType != "Rowingboat")
                { boat.ImageBoat = Boat.CreateImage(boat.BoatType);
                    DisplayImage(boat);
                }
            }
        }
        public void PrintInfoHarbour()
        {
            List<String> info = HarbourAdministration.StringInfo();
            InformationHarbour.Text = "";
            
            foreach (var item in info)
            {
                InformationHarbour.Text += item;
            }
        }
        public void PrintInfoNorthHarbour()
        {
            NorthHarbour.Text = "";
            foreach (var item in Harbour.PrintHarbourInfo(1))
            {
                NorthHarbour.Text += item;
            }
        }
        public void PrintInfoSouthHarbor()
        {
            SouthHarbour.Text = "";
            foreach (var item in Harbour.PrintHarbourInfo(2))
            {
                SouthHarbour.Text += item;
            }
        }

        private void NumberChange(object sender, TextChangedEventArgs e)
        {

            bool input = int.TryParse(NumberOfBoatsPerDay.Text, out int numberOfBoats);
            if (input)
            { Counter.NumberOfNewBoatsPerUdate = numberOfBoats; }
            else
            { Counter.NumberOfNewBoatsPerUdate = 5;
                NumberOfBoatsPerDay.Text = "5";
            }

        }
    }
}
