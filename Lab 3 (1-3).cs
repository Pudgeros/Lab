using System;

class Matrix
{
    private int[,] matrix;

    public Matrix(int n, int m)
    {
        matrix = new int[n, m];
    }

    public void Task1_1()
    {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);

        for (int col = 0; col < m; col++)
        {
            for (int row = n - 1; row >= 0; row--)
            {
                Console.Write($"Введите элемент для позиции [{row}][{col}]: ");
                int value;
                while (!int.TryParse(Console.ReadLine(), out value))
                {
                    Console.WriteLine("Некорректное значение. Введите целое число.");
                    Console.Write($"Введите элемент для позиции [{row}][{col}]: ");
                }
                matrix[row, col] = value;
            }
        }
    }

    public void Task1_2()
    {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);

        Random rand = new Random();
        int[] oddDigits = { 1, 3, 5, 7, 9 };

        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < m; col++)
            {
                int number = 0;
                for (int i = 0; i < 4; i++)
                {
                    number = number * 10 + oddDigits[rand.Next(oddDigits.Length)];
                }
                matrix[row, col] = number;
            }
        }
    }

    public void Task1_3()
    {
        int n = matrix.GetLength(0);

        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i <= k; i++)
            {
                int j = k - i;
                if ((k % 2 != 0 && n % 2 == 0) || (k % 2 == 0 && n % 2 != 0))
                {
                    matrix[n - 1 - j, n - 1 - i] = i + 1;
                }
                else
                {
                    matrix[n - 1 - j, n - 1 - i] = k - i + 1;
                }
            }
        }
    }

    public void Task2()
    {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);

        double[] avg = new double[m];
        int[] counts = new int[m];

        int sum = 0;
        for (int row = 0; row < n; row++)
        {
            sum += matrix[row, 0];
        }
        avg[0] = sum / n;

        for (int col = 1; col < m; col++)
        {
            sum = 0;
            for (int row = 0; row < n; row++)
            {
                sum += matrix[row, col];
            }
            avg[col] = sum / n;

            for (int row = 0; row < n; row++)
            {
                if (matrix[row, col] > avg[col - 1])
                {
                    counts[col]++;
                }
            }
        }

        Console.WriteLine("Среднее арифметическое первого столбца: " + avg[0]);
        for (int col = 1; col < m; col++)
        {
            Console.WriteLine($"Количество элементов в столбце {col + 1}, превышающих среднее арифметическое предыдущего столбца: {counts[col]}");
        }
    }

    public void PrintMatrix()
    {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);

        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < m; col++)
            {
                Console.Write(matrix[row, col] + " ");
            }
            Console.WriteLine();
        }
    }

    public override string ToString()
    {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);

        string result = "";
        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < m; col++)
            {
                result += matrix[row, col] + " ";
            }
            result += "\n";
        }
        return result;
    }

    // Перегрузка операторов для задания 3
    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (a.matrix.GetLength(0) != b.matrix.GetLength(0) || a.matrix.GetLength(1) != b.matrix.GetLength(1))
        {
            throw new ArgumentException("Матрицы должны быть одинакового размера.");
        }

        Matrix result = new Matrix(a.matrix.GetLength(0), a.matrix.GetLength(1));
        for (int row = 0; row < a.matrix.GetLength(0); row++)
        {
            for (int col = 0; col < a.matrix.GetLength(1); col++)
            {
                result.matrix[row, col] = a.matrix[row, col] - b.matrix[row, col];
            }
        }
        return result;
    }

    public static Matrix operator *(int scalar, Matrix b)
    {
        Matrix result = new Matrix(b.matrix.GetLength(0), b.matrix.GetLength(1));
        for (int row = 0; row < b.matrix.GetLength(0); row++)
        {
            for (int col = 0; col < b.matrix.GetLength(1); col++)
            {
                result.matrix[row, col] = scalar * b.matrix[row, col];
            }
        }
        return result;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.matrix.GetLength(1) != b.matrix.GetLength(0))
        {
            throw new InvalidOperationException("Матрицы не могут быть умножены.");
        }

        Matrix result = new Matrix(a.matrix.GetLength(0), b.matrix.GetLength(1));
        for (int i = 0; i < a.matrix.GetLength(0); i++)
        {
            for (int j = 0; j < b.matrix.GetLength(1); j++)
            {
                result.matrix[i, j] = 0;
                for (int k = 0; k < a.matrix.GetLength(1); k++)
                {
                    result.matrix[i, j] += a.matrix[i, k] * b.matrix[k, j];
                }
            }
        }
        return result;
    }

    public static Matrix operator -(Matrix a, int scalar)
    {
        Matrix result = new Matrix(a.matrix.GetLength(0), a.matrix.GetLength(1));
        for (int row = 0; row < a.matrix.GetLength(0); row++)
        {
            for (int col = 0; col < a.matrix.GetLength(1); col++)
            {
                result.matrix[row, col] = a.matrix[row, col] - scalar;
            }
        }
        return result;
    }

    public Matrix Transpose()
    {
        Matrix result = new Matrix(matrix.GetLength(1), matrix.GetLength(0));
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                result.matrix[col, row] = matrix[row, col];
            }
        }
        return result;
    }

    public static Matrix Task3(Matrix A, Matrix B, Matrix C)
    {
        Matrix A_scaled = 7 * A;
        Matrix B_transposed = B.Transpose();
        Matrix B_transposed_minus_C = B_transposed - C;
        Matrix result = A_scaled * B_transposed_minus_C;
        return result;
    }
}

class Program
{
    static void Main()
    {
        int n = GetPositiveInteger("Введите количество строк (n): ");
        int m = GetPositiveInteger("Введите количество столбцов (m): ");

        Console.WriteLine("Первое задание: ");

        Matrix matrix1 = new Matrix(n, m);
        matrix1.Task1_1();
        Console.WriteLine("Первый массив:");
        matrix1.PrintMatrix();

        Matrix matrix2 = new Matrix(n, n);
        matrix2.Task1_2();
        Console.WriteLine("Второй массив:");
        matrix2.PrintMatrix();

        Matrix matrix3 = new Matrix(n, n);
        matrix3.Task1_3();
        Console.WriteLine("Третий массив:");
        matrix3.PrintMatrix();

        Console.WriteLine("Второе задание: ");
        matrix1.Task2();

        Console.WriteLine("Третье задание: ");
        Matrix result = Matrix.Task3(matrix1, matrix2, matrix3);
        Console.WriteLine("7*А*(Вт-С):");
        Console.WriteLine(result);
    }

    static int GetPositiveInteger(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value) && value > 0)
            {
                return value;
            }
            else
            {
                Console.WriteLine("Некорректное значение. Введите положительное целое число.");
            }
        }
    }
}
