using System;
using System.IO;
using System.Text.Json;

public struct Toy
{
    public string Name { get; set; }
    public int Price { get; set; }
    public int MinAge { get; set; }
    public int MaxAge { get; set; }
}

class FileOperations
{
    public static void HandleTask4()
    {
        Console.Write("Введите количество элементов для файла: ");
        string input = Console.ReadLine();
        string Path = "data.bin";
        if (int.TryParse(input, out int x) && x >= 2)
        {
            FillFile(Path, x);
            int difference = FindDifference(Path);
            Console.WriteLine($"Разность между максимальным и минимальным элементами: {difference}");
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Введите целое число >= 2");
        }
    }

    public static void HandleTask5()
    {
        Console.Write("Введите кол-во элементов для файла и число k (разница в цене между самой дорогой игрушкой): ");
        string input = Console.ReadLine();
        string[] parts = input.Split(' ');
        string toyPath = "toys.json";
        if (parts.Length == 2 && int.TryParse(parts[0], out int a) &&
            int.TryParse(parts[1], out int b) && a > 0 && b > 0 )
        {
            FillToyFile(toyPath, a);
            ExpensiveToys(toyPath, b);
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Введите 2 целых положительных числа");
        }
    }

    public static void HandleTask6()
    {
        Console.Write("Введите количество элементов для файла: ");
        string input = Console.ReadLine();
        string Path = "data.txt";
        if (int.TryParse(input, out int x) && x >= 1)
        {
            FillTextFile(Path, x);
            int count = CountMaxElementOccurrences(Path);
            Console.WriteLine($"Количество вхождений максимального элемента: {count}");
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Введите целое число >= 1");
        }
    }

    public static void HandleTask7()
    {
        Console.Write("Введите количество элементов для файла: ");
        string input = Console.ReadLine();
        string Path = "data_task7.txt";
        if (int.TryParse(input, out int x) && x >= 1)
        {
            FillTextFileWithMultipleNumbers(Path, x);
            int count = CountEvenElements(Path);
            Console.WriteLine($"Количество четных элементов: {count}");
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Введите целое число >= 1");
        }
    }

    public static void HandleTask8()
    {
        Console.Write("Введите комбинацию символов для поиска: ");
        string combination = Console.ReadLine();
        string inputPath = "new.txt";
        string outputPath = "output_task8.txt";

        if (File.Exists(inputPath))
        {
            CreateFilteredFile(inputPath, outputPath, combination);
            Console.WriteLine($"Файл с отфильтрованными строками создан: {outputPath}");
            PrintFileContent(outputPath);
        }
        else
        {
            Console.WriteLine("Файл не найден. Проверьте путь к файлу.");
        }
    }

    // Метод для заполнения бинарного файла случайными данными (Задание 4)
    public static void FillFile(string filePath, int count)
    {
        Random rand = new Random();
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            for (int i = 0; i < count; i++)
            {
                int randomNumber = rand.Next(1, 100); 
                writer.Write(randomNumber);
            }
        }
    }

    // Метод для нахождения разности между максимальным и минимальным элементами в бинарном файле (Задание 4)
    public static int FindDifference(string filePath)
    {
        int max = int.MinValue;
        int min = int.MaxValue;

        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int number = reader.ReadInt32();
                if (number > max)
                {
                    max = number;
                }
                if (number < min)
                {
                    min = number;
                }
            }
        }
        Console.WriteLine($"min: {min}, max: {max}");

        return max - min;
    }

    // Метод для заполнения бинарного файла данными о игрушках (Задание 5)
    public static void FillToyFile(string filePath, int count)
    {
        Random rand = new Random();
        Toy[] toys = new Toy[count];

        for (int i = 0; i < count; i++)
        {
            toys[i] = new Toy
            {
                Name = $"Toy{i + 1}",
                Price = rand.Next(100, 1000), 
                MinAge = rand.Next(1, 5), 
                MaxAge = rand.Next(6, 10) 
            };
        }

        string jsonString = JsonSerializer.Serialize(toys);
        File.WriteAllText(filePath, jsonString);
    }

    // Метод для вывода названий наиболее дорогих игрушек (Задание 5)
    public static void ExpensiveToys(string filePath, int k)
    {
        string jsonString = File.ReadAllText(filePath);
        Toy[] toys = JsonSerializer.Deserialize<Toy[]>(jsonString);

        int maxPrice = int.MinValue;
        foreach (var toy in toys)
        {
            if (toy.Price > maxPrice)
            {
                maxPrice = toy.Price;
            }
        }

        Console.WriteLine($"Наиболее дорогие игрушки (цена отличается от {maxPrice} не более, чем на {k} руб.):");
        foreach (var toy in toys)
        {
            if (maxPrice - toy.Price <= k)
            {
                Console.WriteLine($"{toy.Name}: {toy.Price} руб.");
            }
        }
    }

    // Метод для заполнения текстового файла случайными данными (Задание 6)
    public static void FillTextFile(string filePath, int count)
    {
        Random rand = new Random();
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < count; i++)
            {
                int randomNumber = rand.Next(1, 100); 
                writer.WriteLine(randomNumber);
            }
        }
    }

    // Метод для подсчета количества вхождений максимального элемента в файл (Задание 6)
    public static int CountMaxElementOccurrences(string filePath)
    {
        int max = int.MinValue;
        int count = 0;

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (int.TryParse(line, out int number))
                {
                    if (number > max)
                    {
                        max = number;
                        count = 1;
                    }
                    else if (number == max)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    // Метод для заполнения текстового файла случайными данными (Задание 7)
    public static void FillTextFileWithMultipleNumbers(string filePath, int count)
    {
        Random rand = new Random();
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < count; i++)
            {
                int randomNumber = rand.Next(1, 100); 
                writer.Write(randomNumber + " ");
            }
        }
    }

    // Метод для подсчета количества четных элементов в файле (Задание 7)
    public static int CountEvenElements(string filePath)
    {
        int count = 0;

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line = reader.ReadToEnd();
            string[] numbers = line.Split(' ');
            foreach (var numberStr in numbers)
            {
                if (int.TryParse(numberStr, out int number) && number % 2 == 0)
                {
                    count++;
                }
            }
        }

        return count;
    }
    
    // Метод для создания нового текстового файла с отфильтрованными строками (Задание 8)
    public static void CreateFilteredFile(string inputPath, string outputPath, string combination)
    {
        using (StreamReader reader = new StreamReader(inputPath))
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.ToLower().Contains(combination.ToLower()))
                {
                    writer.WriteLine(line);
                }
            }
        }
    }

    // Метод для вывода содержимого файла (Задание 8)
    public static void PrintFileContent(string filePath)
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                Console.WriteLine("Содержимое файла: ");
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите номер задания (4-8): ");
        string input = Console.ReadLine();
        int task_num;

        if (int.TryParse(input, out task_num))
        {
            switch (task_num)
            {
                case 4:
                    FileOperations.HandleTask4();
                    break;

                case 5:
                    FileOperations.HandleTask5();
                    break;

                case 6:
                    FileOperations.HandleTask6();
                    break;

                case 7:
                    FileOperations.HandleTask7();
                    break;

                case 8:
                    FileOperations.HandleTask8();
                    break;

                default:
                    Console.WriteLine("Такого задания нет :(");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод.");
        }
    }
}
