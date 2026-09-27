using WtLab2.Models;
using WtLab2.Interfaces;
using WtLab2.Services;
using System.Threading.Tasks;

Console.WriteLine("=== Авиакомпания: Система бронирования билетов ===");
Console.WriteLine();
Console.WriteLine("Выберите операцию:");
Console.WriteLine("1 - Просмотреть рейсы");
Console.WriteLine("2 - Забронировать билет");
Console.WriteLine("3 - Просмотреть пассажиров");
Console.WriteLine("0 - Выход");

Passenger p1 = new Passenger(1, "Иванов Иван Иванович", "AB1234567");
Passenger p2 = new Passenger(2, "Петрова Мария Сергеевна", "CD7654321");

p1.PrintInfo();
p2.PrintInfo();

p1.UpdatePassport("XY9999999");
p1.PrintInfo();

p1.UpdatePassport("");

BaseEntity[] entities = new BaseEntity[]
{
    new Passenger(3, "Сидоров Пётр Алексеевич", "EF1112223"),
    new Flight(1, "BE101", "Минск - Москва"),
    new Flight(2, "BE202", "Минск - Варшава")
};

Console.WriteLine();
Console.WriteLine("=== Список всех сущностей (полиморфизм) ===");
foreach (var entity in entities)
{
    entity.PrintInfo();
}

Console.WriteLine();
Console.WriteLine("=== Работа сервиса пассажиров ===");

IItemService<Passenger> passengerService = new PassengerService();

passengerService.Add(p1);
passengerService.Add(p2);

Passenger? found1 = passengerService.GetById(1);
Console.WriteLine($"Поиск по Id=1: {found1?.FullName}");

PassengerService concreteService = (PassengerService)passengerService;
Passenger? found2 = concreteService.GetById("CD7654321");
Console.WriteLine($"Поиск по паспорту: {found2?.FullName}");

foreach (var p in passengerService.GetAll())
{
    p.PrintInfo();
}

Console.WriteLine();
Console.WriteLine("=== Блок 2: список пассажиров (List<T>) ===");

List<Passenger> allPassengers = new List<Passenger>
{
    new Passenger(1, "Иванов Иван Иванович", "AB1234567"),
    new Passenger(2, "Петрова Мария Сергеевна", "CD7654321"),
    new Passenger(3, "Сидоров Пётр Алексеевич", "EF1112223"),
    new Passenger(4, "Кузнецова Анна Викторовна", "GH4445556"),
    new Passenger(5, "Смирнов Олег Дмитриевич", "IJ7778889"),
    new Passenger(6, "Новикова Дарья Игоревна", "KL0001112")
};

foreach (var passenger in allPassengers)
{
    passenger.PrintInfo();
}

Console.WriteLine();
Console.WriteLine("=== Блок 2: словарь пассажиров (Dictionary) ===");

Dictionary<int, Passenger> passengerDict = new Dictionary<int, Passenger>();
foreach (var passenger in allPassengers)
{
    passengerDict.Add(passenger.Id, passenger);
}

if (passengerDict.TryGetValue(3, out Passenger? foundPassenger))
{
    Console.WriteLine($"Найден по Id=3: {foundPassenger.FullName}");
}
else
{
    Console.WriteLine("Пассажир с Id=3 не найден.");
}

if (passengerDict.TryGetValue(99, out Passenger? notFoundPassenger))
{
    Console.WriteLine($"Найден по Id=99: {notFoundPassenger.FullName}");
}
else
{
    Console.WriteLine("Пассажир с Id=99 не найден.");
}

Console.WriteLine();
Console.WriteLine("=== Блок 2: LINQ-запросы ===");

// Фильтрация: пассажиры, чьё имя начинается на "П"... возьмём с чётным Id для примера
var evenIdPassengers = allPassengers.Where(p => p.Id % 2 == 0).ToList();
Console.WriteLine("Пассажиры с чётным Id:");
foreach (var p in evenIdPassengers)
{
    Console.WriteLine($"- {p.FullName}");
}

// Сортировка по имени
var sortedByName = allPassengers.OrderBy(p => p.FullName).ToList();
Console.WriteLine();
Console.WriteLine("Пассажиры по алфавиту:");
foreach (var p in sortedByName)
{
    Console.WriteLine($"- {p.FullName}");
}

// Проекция: только имена
var names = allPassengers.Select(p => p.FullName).ToList();
Console.WriteLine();
Console.WriteLine("Только имена (Select):");
foreach (var name in names)
{
    Console.WriteLine($"- {name}");
}

// Агрегатные операции
int totalCount = allPassengers.Count();
int maxId = allPassengers.Max(p => p.Id);
int minId = allPassengers.Min(p => p.Id);
double avgId = allPassengers.Average(p => p.Id);

Console.WriteLine();
Console.WriteLine($"Всего пассажиров: {totalCount}, макс Id: {maxId}, мин Id: {minId}, средний Id: {avgId:F1}");

// Группировка: по первой букве имени
var grouped = allPassengers.GroupBy(p => p.FullName[0]);
Console.WriteLine();
Console.WriteLine("Группировка по первой букве имени:");
foreach (var group in grouped)
{
    Console.WriteLine($"Буква '{group.Key}': {group.Count()} чел.");
}

Console.WriteLine();
Console.WriteLine("=== Блок 3: асинхронная загрузка данных ===");

await LoadFlightsAsync();

async Task LoadFlightsAsync()
{
    Console.WriteLine("Начинаем загрузку рейсов...");
    await Task.Delay(2000); // имитация долгой операции (например, запрос к серверу)
    Console.WriteLine("Рейсы успешно загружены!");
}

Console.WriteLine();
Console.WriteLine("=== Блок 3: асинхронный метод с результатом ===");

List<Flight> loadedFlights = await LoadFlightListAsync();
Console.WriteLine($"Получено рейсов: {loadedFlights.Count}");
foreach (var flight in loadedFlights)
{
    flight.PrintInfo();
}

async Task<List<Flight>> LoadFlightListAsync()
{
    Console.WriteLine("Запрашиваем список рейсов с сервера...");
    await Task.Delay(1500);
    return new List<Flight>
    {
        new Flight(10, "BE303", "Минск - Париж"),
        new Flight(11, "BE404", "Минск - Берлин")
    };
}

Console.WriteLine();
Console.WriteLine("=== Блок 3: параллельное выполнение и обработка ошибок ===");

var stopwatch = System.Diagnostics.Stopwatch.StartNew();

Task task1 = SimulateOperationAsync("Проверка билета #1", 1000);
Task task2 = SimulateOperationAsync("Проверка билета #2", 1500);
Task task3 = SimulateOperationAsync("Проверка билета #3", 2000);

await Task.WhenAll(task1, task2, task3);

stopwatch.Stop();
Console.WriteLine($"Все проверки завершены за {stopwatch.ElapsedMilliseconds} мс (а не за 4500 мс — они шли параллельно!)");

try
{
    await RiskyOperationAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Поймана ошибка: {ex.Message}");
}

async Task SimulateOperationAsync(string name, int delayMs)
{
    Console.WriteLine($"{name}: начало");
    await Task.Delay(delayMs);
    Console.WriteLine($"{name}: завершено");
}

async Task RiskyOperationAsync()
{
    Console.WriteLine("Выполняем рискованную операцию...");
    await Task.Delay(500);
    throw new InvalidOperationException("Билет уже забронирован другим пассажиром!");
}