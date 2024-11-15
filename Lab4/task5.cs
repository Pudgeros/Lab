using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public partial class TaskSolver
{
    // Класс для хранения данных о магазинах
    public class Store
    {
        public string Firm { get; set; }
        public string Street { get; set; }
        public int Fatness { get; set; }
        public int Price { get; set; }
    }

    // Метод для генерации и сериализации данных 
    public static void GenerateAndSerializeData(string filePath)
    {
        List<Store> stores = new List<Store>
        {
            new Store { Firm = "Firm1", Street = "Street1", Fatness = 15, Price = 2500 },
            new Store { Firm = "Firm2", Street = "Street2", Fatness = 20, Price = 3000 },
            new Store { Firm = "Firm3", Street = "Street3", Fatness = 25, Price = 3500 },
            new Store { Firm = "Firm4", Street = "Street4", Fatness = 15, Price = 2500 },
            new Store { Firm = "Firm5", Street = "Street5", Fatness = 20, Price = 3000 },
            new Store { Firm = "Firm6", Street = "Street6", Fatness = 25, Price = 3500 }
        };

        XmlSerializer serializer = new XmlSerializer(typeof(List<Store>));
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, stores);
        }
    }

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
            XmlSerializer serializer = new XmlSerializer(typeof(List<Store>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<Store> stores = (List<Store>)serializer.Deserialize(reader);

                foreach (Store store in stores)
                {
                    int fatness = store.Fatness;
                    int price = store.Price;

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
        string filePath = "task5.xml";
        GenerateAndSerializeData(filePath);
        DetermineCheapestStores(filePath);
    }
}
