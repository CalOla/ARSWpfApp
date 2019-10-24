using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ARSWpfApp.ServiceReference1;

namespace ARSWpfApp
{
    /// <summary>
    /// Interaction logic for CreateFlight.xaml
    /// </summary>
    public partial class CreateFlight : Page
    {
        private ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        public CreateFlight()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            InsertFlight flight = new InsertFlight();
            flight.FlightNumber = txtFlightNumber.Text;
            flight.ArrivalAirport = txtArrivalAirport.Text;
            flight.DepartureAirport = txtDepartureAirport.Text;
            flight.DepartureTime = txtDepartureTime.Text;
            flight.ArrivalTime = txtArrivalTime.Text;
            flight.DepartureDate = txtDepartureDate.Text;
            flight.FirstClassPrice = txtFirstClassPrice.Text;
            flight.EconomyClassPrice = txtEconomyClassPrice.Text;
            flight.SeatingCapacity = Convert.ToInt32(capacity.Text.ToString());

            string response = client.AddFlight(flight);

            infoLabel.Content = response;

            if (response == "Successfully Inserted")
            {
                this.NavigationService.Navigate(new AdminPage());
            }
        }
        
    }
}
