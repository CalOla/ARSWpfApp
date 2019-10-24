using System;
using System.Collections.Generic;
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
using System.Data;

namespace ARSWpfApp
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        private DataTable dataTable;
        private string username;
        public CustomerPage()
        {
            InitializeComponent();
            setAvailableFlights();
        }

        public void SetUpNavigationHandler(NavigationService ns)
        {
            ns.LoadCompleted += NavigationService_LoadCompleted;
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            NavigationService.LoadCompleted -= NavigationService_LoadCompleted;
            username = (string)e.ExtraData;
            setMyFlights();
        }

        private void setAvailableFlights()
        {
            client = new ServiceReference1.Service1Client();
            dataTable = client.GetAllFlights();
            AllFlightsGrid.ItemsSource = dataTable.DefaultView;
        }

        private void setMyFlights()
        {
            client = new ServiceReference1.Service1Client();
            DataTable myFlightDataTable = client.GetMyFlights(username);
            MyFlightsGrid.ItemsSource = myFlightDataTable.DefaultView;
        }

        private void btnBookFlight_Click(object sender, RoutedEventArgs e)
        {
            int currentRowIndex = AllFlightsGrid.Items.IndexOf(AllFlightsGrid.CurrentItem);
            int i = 0;
            int numberOfRows = AllFlightsGrid.Items.Count;
            Flight[] flight = new Flight[numberOfRows];
            foreach (DataRow row in dataTable.Rows)
            {
                flight[i] = new Flight();
                flight[i].FlightNumber = row["FlightNumber"].ToString();
                flight[i].DepartureAirport = row["DepartureAirport"].ToString();
                flight[i].ArrivalAirport = row["ArrivalAirport"].ToString();
                flight[i].DepartureTime = row["DepartureTime"].ToString();
                flight[i].ArrivalTime = row["ArrivalTime"].ToString();
                flight[i].FirstClassPrice = row["FirstClassPrice"].ToString();
                flight[i].EconomyClassPrice = row["EconomyClassPrice"].ToString();
                flight[i].SeatingCapacity = row["SeatingCapacity"].ToString();
                flight[i].Username = username;
                i++;
            }
            if(flight[currentRowIndex].SeatingCapacity == "20")
            {
                TenSeatsPage tenSeatsPage = new TenSeatsPage();
                tenSeatsPage.SetUpNavigationHandler(NavigationService);
                this.NavigationService.Navigate(tenSeatsPage, flight[currentRowIndex]);
            }
            else if(flight[currentRowIndex].SeatingCapacity == "30")
            {
                _30SeatsPage thirtySeatsPage = new _30SeatsPage();
                thirtySeatsPage.SetUpNavigationHandler(NavigationService);
                this.NavigationService.Navigate(thirtySeatsPage, flight[currentRowIndex]);
            }
        }
    }
}
