using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARSWpfApp
{
    class Flight
    {
        string flightNumber = string.Empty;
        string departureAirport = string.Empty;
        string arrivalAirport = string.Empty;
        string departureTime = string.Empty;
        string arrivalTime = string.Empty;
        string firstClassPrice = string.Empty;
        string economyClassPrice = string.Empty;
        string seatingCapacity = string.Empty;
        string username = string.Empty;

        public string FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }

        public string DepartureAirport
        {
            get { return departureAirport; }
            set { departureAirport = value; }
        }

        public string ArrivalAirport
        {
            get { return arrivalAirport; }
            set { arrivalAirport = value; }
        }

        public string DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }

        public string ArrivalTime
        {
            get { return arrivalTime; }
            set { arrivalTime = value; }
        }

        public string FirstClassPrice
        {
            get { return firstClassPrice; }
            set { firstClassPrice = value; }
        }

        public string EconomyClassPrice
        {
            get { return economyClassPrice; }
            set { economyClassPrice = value; }
        }

        public string SeatingCapacity
        {
            get { return seatingCapacity; }
            set { seatingCapacity = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
    }
}
