using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace AirTechCaseStudy
{
    /// <summary>
    /// A collection of Schedules
    /// </summary>
    public class Schedules : Collection<Schedule>
    {
        /// <summary>
        /// Parses the FlightSchedule.txt file in the solution to create a collection of schedules 
        /// </summary>
        public Schedules()
        {
            Regex dayRegex = new Regex(@"^Day (?<Day>\d+):$"); //Regex for the Day line
            Regex scheduleRegex = new Regex(@"^Flight (?<FlightNumber>\d+):[\w\s]*\((?<Departure>[a-zA-Z]{3})\)[\w\s]*\((?<Arrival>[a-zA-Z]{3})\)$"); // Regex for the Flight Schedule line

            string[] flightSchedules = Resource.FlightSchedule.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int day = default(int);
            for (int i = 0; i < flightSchedules.Length; i++)
            {
                if (i % 4 == 0) //The Day line is on every 4th line, so use the Day Regex
                {
                    Match m = dayRegex.Match(flightSchedules[i]);
                    if (m.Success)
                    {
                        day = int.Parse(m.Groups["Day"].Value); //Update day variable that will be used for the flights on that day
                    }
                }
                else // The Flight Schedule line is the remaining lines, so use the Flight Schedule Regex
                {
                    Match m = scheduleRegex.Match(flightSchedules[i]);
                    if (m.Success)
                    {
                        this.Add(new Schedule(int.Parse(m.Groups["FlightNumber"].Value), m.Groups["Departure"].Value, m.Groups["Arrival"].Value, day)); //Add flight schedule to the collection
                    }
                }
            }
        }

        /// <summary>
        /// Override ToString method to return a formatted string of all the scheduled flights
        /// </summary>
        /// <returns>A formatted string of all the scheduled flight details</returns>
        public override string ToString()
        {
            return string.Join(Environment.NewLine, this);

        }
    }

    /// <summary>
    /// A single scheduled flight details
    /// </summary>
    public class Schedule : Flight
    {
        private int Capacity { get; set; }

        public Schedule(int flightNumber, string departure, string arrival, int day) : base(flightNumber, departure, arrival, day) { }

        /// <summary>
        /// Checks whether the flight is carrying less than or equal to 20 orders
        /// </summary>
        /// <returns>Boolean whether the flight can carry another order</returns>
        public bool AvailableCapacity()
        {
            return this.Capacity < 20;
        }

        /// <summary>
        /// Add 1 order to the capacity
        /// </summary>
        public void AddCapacity()
        {
            this.Capacity++;
        }

        /// <summary>
        /// Override ToString method to return the formatted string of this schedule
        /// </summary>
        /// <returns>A formatted string of the schedule</returns>
        public override string ToString()
        {
            return $"Flight: {this.FlightNumber}, departure: {this.Departure}, arrival: {this.Arrival}, day: {this.Day}";
        }
    }
}
