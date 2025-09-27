using System;
using System.Collections;
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
/// Класс каталога автомобилей с различными итераторами
/// </summary>
public class CarCatalog : IEnumerable<Car>
{
    private Car[] _cars;

    /// <summary>
    /// Количество автомобилей в каталоге
    /// </summary>
    public int Count => _cars.Length;

    /// <summary>
    /// Конструктор каталога из массива автомобилей
    /// </summary>
    /// <param name="cars">Массив автомобилей</param>
    public CarCatalog(Car[] cars)
    {
        _cars = cars ?? throw new ArgumentNullException(nameof(cars));
    }

    /// <summary>
    /// Индексатор для доступа к автомобилям по индексу
    /// </summary>
    /// <param name="index">Индекс автомобиля</param>
    /// <returns>Автомобиль по указанному индексу</returns>
    public Car this[int index]
    {
        get
        {
            if (index < 0 || index >= _cars.Length)
                throw new IndexOutOfRangeException("Индекс выходит за границы массива");
            return _cars[index];
        }
        set
        {
            if (index < 0 || index >= _cars.Length)
                throw new IndexOutOfRangeException("Индекс выходит за границы массива");
            _cars[index] = value;
        }
    }

    /// <summary>
    /// Стандартный итератор для прямого прохода (с первого до последнего элемента)
    /// </summary>
    /// <returns>Итератор для прямого прохода</returns>
    public IEnumerator<Car> GetEnumerator()
    {
        for (int i = 0; i < _cars.Length; i++)
        {
            yield return _cars[i];
        }
    }

    /// <summary>
    /// Явная реализация интерфейса IEnumerable
    /// </summary>
    /// <returns>Итератор для прямого прохода</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Итератор для обратного прохода (от последнего к первому элементу)
    /// </summary>
    /// <returns>Итератор для обратного прохода</returns>
    public IEnumerable<Car> Reverse()
    {
        for (int i = _cars.Length - 1; i >= 0; i--)
        {
            yield return _cars[i];
        }
    }

    /// <summary>
    /// Итератор для фильтрации по году выпуска
    /// </summary>
    /// <param name="year">Год выпуска для фильтрации</param>
    /// <returns>Итератор для автомобилей указанного года выпуска</returns>
    public IEnumerable<Car> FilterByProductionYear(int year)
    {
        foreach (var car in _cars)
        {
            if (car.ProductionYear == year)
            {
                yield return car;
            }
        }
    }

    /// <summary>
    /// Итератор для фильтрации по минимальной максимальной скорости
    /// </summary>
    /// <param name="minSpeed">Минимальная скорость для фильтрации</param>
    /// <returns>Итератор для автомобилей с максимальной скоростью не менее указанной</returns>
    public IEnumerable<Car> FilterByMaxSpeed(int minSpeed)
    {
        foreach (var car in _cars)
        {
            if (car.MaxSpeed >= minSpeed)
            {
                yield return car;
            }
        }
    }

    /// <summary>
    /// Итератор для фильтрации по диапазону скорости
    /// </summary>
    /// <param name="minSpeed">Минимальная скорость</param>
    /// <param name="maxSpeed">Максимальная скорость</param>
    /// <returns>Итератор для автомобилей в указанном диапазоне скоростей</returns>
    public IEnumerable<Car> FilterBySpeedRange(int minSpeed, int maxSpeed)
    {
        foreach (var car in _cars)
        {
            if (car.MaxSpeed >= minSpeed && car.MaxSpeed <= maxSpeed)
            {
                yield return car;
            }
        }
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
            new Car("Tesla Model 3", 2023, 260),
            new Car("Porsche 911", 2021, 320),
            new Car("Nissan Qashqai", 2020, 190)
        };
    }
}