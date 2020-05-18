using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AirTechCaseStudy
{
    /// <summary>
    /// A collection of Order
    /// </summary>
    public class Orders : Collection<Order> { }

    /// <summary>
    /// A single order
    /// </summary>
    public class Order : Flight
    {
        private string Number { get; set; }

        public Order(string number, string arrival) : base(arrival)
        {
            this.Number = number;
        }

        /// <summary>
        /// Override ToString to return a formatted string of the order, if the order does not have a flight number then return not scheduled
        /// </summary>
        /// <returns>A formatted string of the order</returns>
         public override string ToString()
        {
            if (this.FlightNumber == 0)
            {
                return $"order: {this.Number}, flightNumber: not scheduled";
            }
            else
            {
                return $"order: {this.Number}, flightNumber: {this.FlightNumber}, departure: {this.Departure}, arrival: {this.Arrival}, day: {this.Day}";
            }
        }
    }

    /// <summary>
    /// Custom JsonConverter to convert the json file containing the orders
    /// </summary>
    public class OrdersConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Orders) == objectType;
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Reads the json file containing all the orders and parses the information to return as a collection of Order
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns>Orders object containing all the orders in the json file</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObj = JObject.Load(reader);
            Orders orders = new Orders();
            jObj.Children()
              .ToList()
              .ForEach(x => orders.Add(new Order(x.ToObject<JProperty>().Name, x.Children().FirstOrDefault()["destination"].ToObject<string>())));
            return orders;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
