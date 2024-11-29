using System;
using System.IO;
using Aspose.Cells;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите имя файла для протоколирования (или нажмите Enter для использования существующего файла): ");
        string logFileName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(logFileName))
        {
            logFileName = "log.txt";
        }

        StreamWriter logWriter = new StreamWriter(logFileName, true);
        logWriter.WriteLine($"Сеанс начат: {DateTime.Now}");

        // Чтение базы данных из Excel файла
        Workbook wb;
        try
        {
            wb = new Workbook("LR5-var7.xls");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            logWriter.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            logWriter.Close();
            return;
        }

        WorksheetCollection sheets = wb.Worksheets;
        var database = new DatabaseHelper(sheets, wb);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Просмотр базы данных");
            Console.WriteLine("2. Удаление элементов (по ключу)");
            Console.WriteLine("3. Корректировка элементов (по ключу)");
            Console.WriteLine("4. Добавление элементов");
            Console.WriteLine("5. Выполнение запросов");
            Console.WriteLine("6. Выход");

            string choice = Console.ReadLine();
            logWriter.WriteLine($"Выбранное действие: {choice}");

            switch (choice)
            {
                case "1":
                    database.ViewDatabase();
                    break;
                case "2":
                    database.DeleteElement();
                    break;
                case "3":
                    database.UpdateElement();
                    break;
                case "4":
                    database.AddElement();
                    break;
                case "5":
                    database.ExecuteQueries();
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }

        logWriter.WriteLine($"Сеанс завершен: {DateTime.Now}");
        logWriter.Close();
    }
}
