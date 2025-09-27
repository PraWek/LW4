using System;
using System.Text.Json;

/// <summary>
/// Класс, представляющий автомобиль
/// </summary>
public class Car
{
    /// <summary>
    /// Название автомобиля
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Год выпуска автомобиля
    /// </summary>
    public int ProductionYear { get; set; }

    /// <summary>
    /// Максимальная скорость автомобиля (км/ч)
    /// </summary>
    public int MaxSpeed { get; set; }

    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public Car() { }

    /// <summary>
    /// Конструктор с параметрами
    /// </summary>
    /// <param name="name">Название автомобиля</param>
    /// <param name="productionYear">Год выпуска</param>
    /// <param name="maxSpeed">Максимальная скорость</param>
    public Car(string name, int productionYear, int maxSpeed)
    {
        Name = name;
        ProductionYear = productionYear;
        MaxSpeed = maxSpeed;
    }

    /// <summary>
    /// Строковое представление автомобиля
    /// </summary>
    /// <returns>Строка с информацией об автомобиле</returns>
    public override string ToString()
    {
        return $"{Name} ({ProductionYear}), Макс. скорость: {MaxSpeed} км/ч";
    }
}

/// <summary>
/// Тип сортировки для автомобилей
/// </summary>
public enum SortBy
{
    /// <summary>
    /// Сортировка по названию
    /// </summary>
    Name,

    /// <summary>
    /// Сортировка по году выпуска
    /// </summary>
    ProductionYear,

    /// <summary>
    /// Сортировка по максимальной скорости
    /// </summary>
    MaxSpeed
}

/// <summary>
/// Компаратор для сортировки автомобилей по различным критериям
/// </summary>
public class CarComparer : IComparer<Car>
{
    private readonly SortBy _sortBy;

    /// <summary>
    /// Конструктор компаратора
    /// </summary>
    /// <param name="sortBy">Критерий сортировки</param>
    public CarComparer(SortBy sortBy)
    {
        _sortBy = sortBy;
    }

    /// <summary>
    /// Сравнение двух автомобилей по выбранному критерию
    /// </summary>
    /// <param name="x">Первый автомобиль для сравнения</param>
    /// <param name="y">Второй автомобиль для сравнения</param>
    /// <returns>
    /// Меньше 0: x меньше y
    /// Равно 0: x равно y
    /// Больше 0: x больше y
    /// </returns>
    public int Compare(Car x, Car y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return _sortBy switch
        {
            SortBy.Name => string.Compare(x.Name, y.Name, StringComparison.Ordinal),
            SortBy.ProductionYear => x.ProductionYear.CompareTo(y.ProductionYear),
            SortBy.MaxSpeed => x.MaxSpeed.CompareTo(y.MaxSpeed),
            _ => throw new ArgumentException("Неизвестный критерий сортировки")
        };
    }
}

/// <summary>
/// Класс для работы с JSON файлами автомобилей
/// </summary>
public static class CarJsonHelper
{
    private static readonly string FilePath = "cars.json";

    /// <summary>
    /// Сохранение массива автомобилей в JSON файл
    /// </summary>
    /// <param name="cars">Массив автомобилей для сохранения</param>
    public static void SaveCarsToJson(Car[] cars)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(cars, options);
        File.WriteAllText(FilePath, json);
        Console.WriteLine($"Данные сохранены в файл: {FilePath}");
    }

    /// <summary>
    /// Загрузка массива автомобилей из JSON файла
    /// </summary>
    /// <returns>Массив автомобилей</returns>
    public static Car[] LoadCarsFromJson()
    {
        if (!File.Exists(FilePath))
        {
            Console.WriteLine($"Файл {FilePath} не найден. Будет создан новый массив автомобилей.");
            return CreateSampleCars();
        }

        try
        {
            string json = File.ReadAllText(FilePath);
            var cars = JsonSerializer.Deserialize<Car[]>(json);
            Console.WriteLine($"Данные загружены из файла: {FilePath}");
            return cars ?? CreateSampleCars();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке файла: {ex.Message}");
            return CreateSampleCars();
        }
    }

    /// <summary>
    /// Создание тестового массива автомобилей
    /// </summary>
    /// <returns>Массив автомобилей</returns>
    private static Car[] CreateSampleCars()
    {
        return new Car[]
        {
            new Car("Toyota Camry", 2022, 210),
            new Car("BMW X5", 2023, 250),
            new Car("Audi A4", 2021, 240),
            new Car("Mercedes-Benz C-Class", 2020, 230),
            new Car("Honda Civic", 2019, 200),
            new Car("Ford Mustang", 2023, 280),
            new Car("Volkswagen Golf", 2022, 220),
            new Car("Tesla Model 3", 2023, 260)
        };
    }
}