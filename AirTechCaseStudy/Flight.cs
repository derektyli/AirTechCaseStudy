namespace AirTechCaseStudy
{
    /// <summary>
    /// Abstract class that contains flight information
    /// </summary>
    public abstract class Flight
    {
        protected int FlightNumber { get; set; }
        protected string Departure { get; set; }
        protected string Arrival { get; set; }
        protected int Day { get; set; }

        public Flight(string arrival)
        {
            this.Arrival = arrival;
        }

        public Flight(int flightNumber, string departure, string arrival, int day)
        {
            this.FlightNumber = flightNumber;
            this.Departure = departure;
            this.Arrival = arrival;
            this.Day = day;
        }

        /// <summary>
        /// Check if both flights has the same arrival
        /// </summary>
        /// <param name="flight"></param>
        /// <returns>Boolean of whether the two flights has the same arrival</returns>
        public bool ArrivalEquals(Flight flight)
        {
            return this.Arrival == flight.Arrival;
        }

        /// <summary>
        /// Add all the flight information except for arrival into this object 
        /// </summary>
        /// <param name="flight"></param>
        public void AddFlight(Flight flight)
        {
            this.FlightNumber = flight.FlightNumber;
            this.Departure = flight.Departure;
            this.Day = flight.Day;
        }
    }
}
