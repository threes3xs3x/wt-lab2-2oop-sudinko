using WtLab2.Interfaces;

namespace WtLab2.Models
{
    public class Passenger : BaseEntity
    {
        public string FullName { get; set; }
        public string PassportNumber { get; private set; }

        public Passenger(int id, string fullName, string passportNumber)
        {
            Id = id;
            FullName = fullName;
            PassportNumber = passportNumber;
            CreatedAt = DateTime.Now;
        }

        public void UpdatePassport(string newPassportNumber)
        {
            if (string.IsNullOrWhiteSpace(newPassportNumber))
            {
                Console.WriteLine("Ошибка: номер паспорта не может быть пустым.");
                return;
            }
            PassportNumber = newPassportNumber;
            Console.WriteLine("Паспорт обновлён.");
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Пассажир #{Id}: {FullName}, паспорт: {PassportNumber}, создан: {CreatedAt}");
        }
    }
}