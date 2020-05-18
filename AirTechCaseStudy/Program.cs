using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace AirTechCaseStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Schedules schedules = new Schedules();
                Console.WriteLine(schedules);

                Orders orders = JsonConvert.DeserializeObject<Orders>(Encoding.ASCII.GetString(Resource.coding_assigment_orders), new OrdersConverter()); // Deserialize json file from resources to Orders

                foreach (Order order in orders)
                {
                    Schedule schedule = schedules.Where(x => x.ArrivalEquals(order) && x.AvailableCapacity())
                                                 .FirstOrDefault();
                    if (schedule != null)
                    {
                        schedule.AddCapacity();
                        order.AddFlight(schedule);
                    }
                    Console.WriteLine(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
