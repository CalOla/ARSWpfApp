using ARSWpfApp.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

namespace ARSWpfApp
{
    /// <summary>
    /// Interaction logic for TenSeatsPage.xaml
    /// </summary>
    public partial class TenSeatsPage : Page
    {
        #region Private Members
        private string[] preselectedSeats;
        private int[] selectedSeat;
        private Flight flight;
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        private DataTable dataTable;
        #endregion
        public TenSeatsPage()
        {
            InitializeComponent();
        }

        public void SetUpNavigationHandler(NavigationService ns)
        {
            ns.LoadCompleted += NavigationService_LoadCompleted;
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            NavigationService.LoadCompleted -= NavigationService_LoadCompleted;
            flight = (Flight)e.ExtraData;
            lblFlightNumber.Content = "Flight Number: " + flight.FlightNumber;
            lblDepartureAirport.Content = "Departure Airport: " + flight.DepartureAirport;
            lblArrivalAirport.Content = "Arrival Airport: " + flight.ArrivalAirport;
            lblDepartureTime.Content = "Departure Time: " + flight.DepartureTime;
            lblArrivalTime.Content = "Arrival Time: " + flight.ArrivalTime;
            lblFirstClass.Content = "First Class Price: " + flight.FirstClassPrice;
            lblEconomyClass.Content = "Economy Class Price: " + flight.EconomyClassPrice;
            lblSeatingCapacity.Content = "Seating Capacity: " + flight.SeatingCapacity;

            SetPreselectedSeats();
        }

        private void SetPreselectedSeats()
        {
            dataTable = client.GetSelectedSeats(flight.FlightNumber);
            string seatNumbers = "";

            foreach (DataRow row in dataTable.Rows)
            {
                seatNumbers += row["SeatNumber"].ToString() + " ";
            }

            preselectedSeats = seatNumbers.Split(' ');

            for (var i = 0; i < preselectedSeats.Length - 1; i++)
            {
                int[] seatIndex = convertSeatNumber(preselectedSeats[i]);
                var button = (Button)GetChildren(Container, seatIndex[0], seatIndex[1]);
                button.Content = "X";
                button.Foreground = Brushes.Red;
            }
        }

        private int[] convertSeatNumber(string seatNumber)
        {
            int[] array1 = new int[2];
            array1[0] = Convert.ToInt32(seatNumber.Substring(0, seatNumber.Length - 1)) - 1;

            switch (seatNumber[seatNumber.Length - 1])
            {
                case 'A':
                case 'a':
                    array1[1] = 0;
                    break;
                case 'B':
                case 'b':
                    array1[1] = 1;
                    break;
                case 'C':
                case 'c':
                    array1[1] = 3;
                    break;
                case 'D':
                case 'd':
                    array1[1] = 4;
                    break;
            }
            return array1;
        }


        private static UIElement GetChildren(Grid grid, int row, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetRow(child) == row && Grid.GetColumn(child) == column)
                {
                    return child;
                }
            }
            return null;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            if (button.Content != null && button.Content.ToString() == "X")
            {
                return;
            }

            if (selectedSeat == null)
            {
                selectedSeat = new int[2] { row, column };
                button.Content = "X";
            }
            else
            {
                var previousButton = (Button)GetChildren(Container, selectedSeat[0], selectedSeat[1]);
                previousButton.Content = "";

                selectedSeat[0] = row;
                selectedSeat[1] = column;

                var newButton = (Button)GetChildren(Container, selectedSeat[0], selectedSeat[1]);
                newButton.Content = "X";
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSeat == null)
            {
                infoLabel.Background = Brushes.DarkGray;
                infoLabel.Content = "Please select a seat";
                return;
            }

            InsertSeat seat = new InsertSeat();
            seat.FlightNumber = flight.FlightNumber;
            seat.Username = flight.Username;
            seat.SeatNumber = getSeatNumber();
            client.AddSeat(seat);

            CustomerPage customerPage = new CustomerPage();
            customerPage.SetUpNavigationHandler(NavigationService);
            this.NavigationService.Navigate(customerPage, flight.Username);
        }

        private string getSeatNumber()
        {
            if (selectedSeat == null)
            {
                return "";
            }

            string seatColumn = "";

            switch (selectedSeat[1])
            {
                case 0:
                    seatColumn = "A";
                    break;
                case 1:
                    seatColumn = "B";
                    break;
                case 2:
                    seatColumn = "C";
                    break;
                case 3:
                    seatColumn = "D";
                    break;
            }
            int seatRow = selectedSeat[0] + 1;
            return seatRow + seatColumn;
        }
    }
}
