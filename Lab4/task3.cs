using System;
using System.Collections.Generic;
using System.IO;

public partial class TaskSolver
{
    // Метод для определения фабрик, мебель которых приобреталась покупателями
    public static void DetermineFactoryPurchases(HashSet<string> allFactories, Dictionary<string, HashSet<string>> buyerPurchases)
    {
        HashSet<string> factoriesBoughtByAll = new HashSet<string>(allFactories);
        HashSet<string> factoriesBoughtBySome = new HashSet<string>();
        HashSet<string> factoriesBoughtByNone = new HashSet<string>(allFactories);

        foreach (var buyer in buyerPurchases)
        {
            factoriesBoughtByAll.IntersectWith(buyer.Value);
            factoriesBoughtBySome.UnionWith(buyer.Value);
        }

        factoriesBoughtByNone.ExceptWith(factoriesBoughtBySome);

        Console.WriteLine("\nФабрики, мебель которых приобреталась всеми покупателями:");
        foreach (var factory in factoriesBoughtByAll)
        {
            Console.WriteLine(factory);
        }

        Console.WriteLine("\nФабрики, мебель которых приобреталась некоторыми покупателями:");
        foreach (var factory in factoriesBoughtBySome)
        {
            Console.WriteLine(factory);
        }

        Console.WriteLine("\nФабрики, мебель которых не приобреталась никем из покупателей:");
        foreach (var factory in factoriesBoughtByNone)
        {
            Console.WriteLine(factory);
        }
    }

    // Метод для чтения данных из файла
    private static (HashSet<string>, Dictionary<string, HashSet<string>>) ReadDataFromFile(string filePath)
    {
        HashSet<string> allFactories = new HashSet<string>();
        Dictionary<string, HashSet<string>> buyerPurchases = new Dictionary<string, HashSet<string>>();

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length == 0)
            {
                Console.WriteLine("Файл пуст.");
                return (allFactories, buyerPurchases);
            }

            // Чтение фабрик из первой строки
            string[] factories = lines[0].Split(' ');
            foreach (var factory in factories)
            {
                allFactories.Add(factory.Trim());
            }

            // Чтение покупателей и их покупок
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Trim().Split(' ');
                if (parts.Length < 2)
                {
                    Console.WriteLine($"Некорректная строка в файле: {lines[i]}");
                    continue;
                }

                string buyer = parts[0].Trim();
                HashSet<string> purchases = new HashSet<string>();
                for (int j = 1; j < parts.Length; j++)
                {
                    string factory = parts[j].Trim();
                    if (!allFactories.Contains(factory))
                    {
                        Console.WriteLine($"Некорректные данные: фабрика '{factory}' не входит в список всех фабрик.");
                        return (new HashSet<string>(), new Dictionary<string, HashSet<string>>());
                    }
                    purchases.Add(factory);
                }
                buyerPurchases[buyer] = purchases;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }

        return (allFactories, buyerPurchases);
    }

    public static void MainForTask3(string[] args)
    {
        string filePath = "task3.txt";
        var (allFactories, buyerPurchases) = ReadDataFromFile(filePath);

        if (allFactories.Count == 0 || buyerPurchases.Count == 0)
        {
            Console.WriteLine("Некорректный ввод данных. Программа завершена.");
            return;
        }

        DetermineFactoryPurchases(allFactories, buyerPurchases);
    }
}
