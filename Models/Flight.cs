using WtLab2.Interfaces;

namespace WtLab2.Models
{
    public class Flight : BaseEntity
    {
        public string FlightNumber { get; set; }
        public string Destination { get; set; }

        public Flight(int id, string flightNumber, string destination)
        {
            Id = id;
            FlightNumber = flightNumber;
            Destination = destination;
            CreatedAt = DateTime.Now;
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Рейс #{Id}: {FlightNumber} → {Destination}, создан: {CreatedAt}");
        }
    }
}