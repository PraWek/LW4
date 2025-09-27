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
            Car[] carsArray = CarJsonHelper.LoadCarsFromJson();
            CarCatalog catalog = new CarCatalog(carsArray);

            Console.WriteLine("Демонстрация работы каталога автомобилей\n");

            // 1. Прямой проход с первого элемента до последнего
            Console.WriteLine("1. Прямой подход (от первого к последнему):");
            foreach (var car in catalog)
            {
                Console.WriteLine($"   {car}");
            }

            // 2. Обратный проход от последнего к первому
            Console.WriteLine("\n2. Обратный проход (от последнего к первому):");
            foreach (var car in catalog.Reverse())
            {
                Console.WriteLine($"   {car}");
            }

            // 3. Проход по элементам с фильтром по году выпуска
            Console.WriteLine("\n3. Фильтрация по году выпуска (2023 год):");
            foreach (var car in catalog.FilterByProductionYear(2023))
            {
                Console.WriteLine($"   {car}");
            }

            // 4. Проход по элементам с фильтром по максимальной скорости
            Console.WriteLine("\n4. Фильтрация по максимальной скорости (от 250 км/ч):");
            foreach (var car in catalog.FilterByMaxSpeed(250))
            {
                Console.WriteLine($"   {car}");
            }

            // Дополнительно: фильтрация по диапазону скорости
            Console.WriteLine("\n5. Фильтрация по диапазону скорости (200-240 км/ч):");
            foreach (var car in catalog.FilterBySpeedRange(200, 240))
            {
                Console.WriteLine($"   {car}");
            }

            // Демонстрация работы с разными годами
            Console.WriteLine("\n6. Фильтрация по разным годам выпуска:");
            int[] years = { 2019, 2020, 2021, 2022, 2023 };
            foreach (int year in years)
            {
                Console.WriteLine($"   Автомобили {year} года:");
                bool found = false;
                foreach (var car in catalog.FilterByProductionYear(year))
                {
                    Console.WriteLine($"     - {car.Name} ({car.MaxSpeed} км/ч)");
                    found = true;
                }
                if (!found)
                {
                    Console.WriteLine($"     - Нет автомобилей");
                }
            }

            // Сохранение данных обратно в JSON (на случай изменений)
            CarJsonHelper.SaveCarsToJson(carsArray);

            // Демонстрация работы индексатора
            Console.WriteLine("\n7. Демонстрация работы индексатора:");
            Console.WriteLine($"   Первый автомобиль: {catalog[0]}");
            Console.WriteLine($"   Последний автомобиль: {catalog[catalog.Count - 1]}");

            // Добавление нового автомобиля через индексатор (в существующий массив)
            Console.WriteLine("\n8. Обновление данных:");
            catalog[0] = new Car("Toyota Camry Hybrid", 2023, 220);
            Console.WriteLine($"   Обновленный первый автомобиль: {catalog[0]}");

            // Сохранение обновленных данных
            CarJsonHelper.SaveCarsToJson(carsArray);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}