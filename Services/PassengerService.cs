using WtLab2.Interfaces;
using WtLab2.Models;

namespace WtLab2.Services
{
    public class PassengerService : IItemService<Passenger>
    {
        protected List<Passenger> passengers = new List<Passenger>();

        public void Add(Passenger item)
        {
            passengers.Add(item);
            Console.WriteLine($"Добавлен пассажир: {item.FullName}");
        }

        public List<Passenger> GetAll()
        {
            return passengers;
        }

        public Passenger? GetById(int id)
        {
            return passengers.FirstOrDefault(p => p.Id == id);
        }

        // Перегрузка метода: поиск по номеру паспорта
        public Passenger? GetById(string passportNumber)
        {
            return passengers.FirstOrDefault(p => p.PassportNumber == passportNumber);
        }
    }
}