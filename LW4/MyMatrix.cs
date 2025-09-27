using System;

/// <summary>
/// Класс, представляющий матрицу m на n с базовыми операциями
/// </summary>
class MyMatrix
{
    private double[,] matrix;

    /// <summary>
    /// Количество строк матрицы
    /// </summary>
    public int Rows { get; }

    /// <summary>
    /// Количество столбцов матрицы
    /// </summary>
    public int Columns { get; }

    /// <summary>
    /// Конструктор матрицы с заданными размерами
    /// </summary>
    /// <param name="rows">Количество строк (должно быть положительным)</param>
    /// <param name="cols">Количество столбцов (должно быть положительным)</param>
    /// <exception cref="ArgumentException">Выбрасывается при невалидных размерах матрицы</exception>
    public MyMatrix(int rows, int cols)
    {
        if (rows <= 0 || cols <= 0)
            throw new ArgumentException("Размеры матрицы должны быть положительными числами");

        Rows = rows;
        Columns = cols;
        matrix = new double[rows, cols];
    }

    /// <summary>
    /// Заполнение матрицы случайными числами в заданном диапазоне
    /// </summary>
    /// <param name="minValue">Минимальное значение элемента</param>
    /// <param name="maxValue">Максимальное значение элемента</param>
    public void FillRandom(double minValue, double maxValue)
    {
        Random rand = new Random();
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                matrix[i, j] = rand.NextDouble() * (maxValue - minValue) + minValue;
            }
        }
    }

    /// <summary>
    /// Индексатор для доступа к элементам матрицы по индексам
    /// </summary>
    /// <param name="row">Индекс строки (от 0 до Rows-1)</param>
    /// <param name="col">Индекс столбца (от 0 до Columns-1)</param>
    /// <returns>Элемент матрицы по указанным индексам</returns>
    /// <exception cref="IndexOutOfRangeException">Выбрасывается при выходе за границы матрицы</exception>
    public double this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
                throw new IndexOutOfRangeException("Индекс выходит за границы матрицы");
            return matrix[row, col];
        }
        set
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
                throw new IndexOutOfRangeException("Индекс выходит за границы матрицы");
            matrix[row, col] = value;
        }
    }

    /// <summary>
    /// Оператор сложения двух матриц одинакового размера
    /// </summary>
    /// <param name="a">Первая матрица</param>
    /// <param name="b">Вторая матрица</param>
    /// <returns>Результирующая матрица после сложения</returns>
    /// <exception cref="ArgumentException">Выбрасывается при несовпадении размеров матриц</exception>
    public static MyMatrix operator +(MyMatrix a, MyMatrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
            throw new ArgumentException("Матрицы должны быть одного размера для сложения");

        MyMatrix result = new MyMatrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }
        return result;
    }

    /// <summary>
    /// Оператор вычитания двух матриц одинакового размера
    /// </summary>
    /// <param name="a">Первая матрица (уменьшаемое)</param>
    /// <param name="b">Вторая матрица (вычитаемое)</param>
    /// <returns>Результирующая матрица после вычитания</returns>
    /// <exception cref="ArgumentException">Выбрасывается при несовпадении размеров матриц</exception>
    public static MyMatrix operator -(MyMatrix a, MyMatrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
            throw new ArgumentException("Матрицы должны быть одного размера для вычитания");

        MyMatrix result = new MyMatrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = a[i, j] - b[i, j];
            }
        }
        return result;
    }

    /// <summary>
    /// Оператор умножения двух матриц (количество столбцов первой должно равняться количеству строк второй)
    /// </summary>
    /// <param name="a">Первая матрица</param>
    /// <param name="b">Вторая матрица</param>
    /// <returns>Результирующая матрица после умножения</returns>
    /// <exception cref="ArgumentException">Выбрасывается при несовместимых размерах матриц</exception>
    public static MyMatrix operator *(MyMatrix a, MyMatrix b)
    {
        if (a.Columns != b.Rows)
            throw new ArgumentException("Количество столбцов первой матрицы должно равняться количеству строк второй матрицы");

        MyMatrix result = new MyMatrix(a.Rows, b.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < b.Columns; j++)
            {
                double sum = 0;
                for (int k = 0; k < a.Columns; k++)
                {
                    sum += a[i, k] * b[k, j];
                }
                result[i, j] = sum;
            }
        }
        return result;
    }

    /// <summary>
    /// Оператор умножения матрицы на скаляр
    /// </summary>
    /// <param name="a">Исходная матрица</param>
    /// <param name="scalar">Скаляр для умножения</param>
    /// <returns>Результирующая матрица после умножения на скаляр</returns>
    public static MyMatrix operator *(MyMatrix a, double scalar)
    {
        MyMatrix result = new MyMatrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = a[i, j] * scalar;
            }
        }
        return result;
    }

    /// <summary>
    /// Оператор умножения скаляра на матрицу
    /// </summary>
    /// <param name="scalar">Скаляр для умножения</param>
    /// <param name="a">Исходная матрица</param>
    /// <returns>Результирующая матрица после умножения скаляра на матрицу</returns>
    public static MyMatrix operator *(double scalar, MyMatrix a)
    {
        return a * scalar;
    }

    /// <summary>
    /// Оператор деления матрицы на скаляр
    /// </summary>
    /// <param name="a">Исходная матрица</param>
    /// <param name="scalar">Скаляр для деления (не может быть нулем)</param>
    /// <returns>Результирующая матрица после деления на скаляр</returns>
    /// <exception cref="DivideByZeroException">Выбрасывается при попытке деления на ноль</exception>
    public static MyMatrix operator /(MyMatrix a, double scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException("Деление на ноль невозможно");

        MyMatrix result = new MyMatrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = a[i, j] / scalar;
            }
        }
        return result;
    }

    /// <summary>
    /// Вывод матрицы в консоль с форматированием
    /// </summary>
    public void Print()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Console.Write($"{matrix[i, j]:F2}\t");
            }
            Console.WriteLine();
        }
    }
}
