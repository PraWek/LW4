using System;
using System.Text.Json;

class Program
{
    /// <summary>
    /// Основная точка входа в программу
    /// </summary>
    static void Main()
    {
        try
        {
            // Загрузка автомобилей из JSON файла
            Car[] cars = CarJsonHelper.LoadCarsFromJson();

            Console.WriteLine("Исходный массив автомобилей:");
            PrintCars(cars);

            // Демонстрация сортировки различными способами
            Console.WriteLine("\nСортировка по названию");
            Array.Sort(cars, new CarComparer(SortBy.Name));
            PrintCars(cars);

            Console.WriteLine("\nСортировка по году выпуска");
            Array.Sort(cars, new CarComparer(SortBy.ProductionYear));
            PrintCars(cars);

            Console.WriteLine("\nСортировка по максимальной скорости");
            Array.Sort(cars, new CarComparer(SortBy.MaxSpeed));
            PrintCars(cars);

            Console.WriteLine("\nСортировка по максимальной скорости (убывание)");
            Array.Sort(cars, new CarComparer(SortBy.MaxSpeed));
            Array.Reverse(cars);
            PrintCars(cars);

            // Добавление нового автомобиля и сохранение в JSON
            Console.WriteLine("\nДобавление нового автомобиля");
            var newCar = new Car("Porsche 911", 2023, 320);
            Array.Resize(ref cars, cars.Length + 1);
            cars[^1] = newCar;

            Console.WriteLine("Массив после добавления нового автомобиля:");
            PrintCars(cars);

            // Сохранение обновленного массива в JSON
            CarJsonHelper.SaveCarsToJson(cars);

            // Демонстрация загрузки из сохраненного файла
            Console.WriteLine("\nПроверка загрузки из json");
            Car[] loadedCars = CarJsonHelper.LoadCarsFromJson();
            Array.Sort(loadedCars, new CarComparer(SortBy.Name));
            PrintCars(loadedCars);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    /// <summary>
    /// Вывод массива автомобилей в консоль
    /// </summary>
    /// <param name="cars">Массив автомобилей для вывода</param>
    private static void PrintCars(Car[] cars)
    {
        for (int i = 0; i < cars.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {cars[i]}");
        }
    }
}