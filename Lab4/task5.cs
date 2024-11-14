using System;
using System.Collections.Generic;
using System.IO;

public partial class TaskSolver
{
    // Метод для определения количества магазинов, продающих сметану дешевле всего
    public static void DetermineCheapestStores(string filePath)
    {
        Dictionary<int, (int MinPrice, int StoreCount)> fatnessPrices = new Dictionary<int, 
            (int MinPrice, int StoreCount)>
        {
            { 15, (int.MaxValue, 0) },
            { 20, (int.MaxValue, 0) },
            { 25, (int.MaxValue, 0) }
        };

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');

                if (parts.Length != 4)
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите данные в формате: " +
                                      "<Фирма> <Улица> <Жирность> <Цена>");
                    continue;
                }

                string firm = parts[0];
                string street = parts[1];
                if (!int.TryParse(parts[2], out int fatness) || !int.TryParse(parts[3], out int price))
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите данные в формате: " +
                                      "<Фирма> <Улица> <Жирность> <Цена>");
                    continue;
                }

                if (price < 2000 || price > 5000)
                {
                    Console.WriteLine("Некорректный ввод цены. Цена должна быть в диапазоне от 2000 до 5000.");
                    continue;
                }

                if (!fatnessPrices.ContainsKey(fatness))
                {
                    fatnessPrices[fatness] = (int.MaxValue, 0);
                }

                if (price < fatnessPrices[fatness].MinPrice)
                {
                    fatnessPrices[fatness] = (price, 1);
                }
                else if (price == fatnessPrices[fatness].MinPrice)
                {
                    fatnessPrices[fatness] = (fatnessPrices[fatness].MinPrice, 
                        fatnessPrices[fatness].StoreCount + 1);
                }
            }

            int count15 = fatnessPrices.ContainsKey(15) ? fatnessPrices[15].StoreCount : 0;
            int count20 = fatnessPrices.ContainsKey(20) ? fatnessPrices[20].StoreCount : 0;
            int count25 = fatnessPrices.ContainsKey(25) ? fatnessPrices[25].StoreCount : 0;

            Console.WriteLine("Количество магазинов, продающих дешевле всего сметану " +
                              $"с жирностью 15, 20 и 25 процентов: {count15} {count20} {count25}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }
    }

    public static void MainForTask5(string[] args)
    {
        string filePath = "task5.txt";
        DetermineCheapestStores(filePath);
    }
}
