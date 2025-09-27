using System;

class Program
{
    /// <summary>
    /// Основная точка входа в программу
    /// </summary>
    /// <param name="args">Аргументы командной строки</param>
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Введите минимальное значение для случайных чисел:");
            double minValue = double.Parse(Console.ReadLine());

            Console.WriteLine("Введите максимальное значение для случайных чисел:");
            double maxValue = double.Parse(Console.ReadLine());

            // Создаем матрицы
            MyMatrix matrix1 = new MyMatrix(2, 3);
            MyMatrix matrix2 = new MyMatrix(2, 3);
            MyMatrix matrix3 = new MyMatrix(3, 2);

            // Заполняем случайными числами
            matrix1.FillRandom(minValue, maxValue);
            matrix2.FillRandom(minValue, maxValue);
            matrix3.FillRandom(minValue, maxValue);

            Console.WriteLine("\nМатрица 1 (2x3):");
            matrix1.Print();

            Console.WriteLine("\nМатрица 2 (2x3):");
            matrix2.Print();

            Console.WriteLine("\nМатрица 3 (3x2):");
            matrix3.Print();

            // Демонстрация операций
            Console.WriteLine("\nСложение матриц 1 и 2:");
            (matrix1 + matrix2).Print();

            Console.WriteLine("\nВычитание матриц 1 и 2:");
            (matrix1 - matrix2).Print();

            Console.WriteLine("\nУмножение матриц 1 и 3:");
            (matrix1 * matrix3).Print();

            Console.WriteLine("\nУмножение матрицы 1 на 2:");
            (matrix1 * 2).Print();

            Console.WriteLine("\nДеление матрицы 1 на 2:");
            (matrix1 / 2).Print();

            // Демонстрация индексатора
            Console.WriteLine($"\nЭлемент [0,0] матрицы 1: {matrix1[0, 0]:F2}");
            matrix1[0, 0] = 10.5;
            Console.WriteLine($"После изменения элемент [0,0] матрицы 1: {matrix1[0, 0]:F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}