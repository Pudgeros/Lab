using Aspose.Cells;

class Program
{
    static void Main(string[] args)
    {
        // Запрос на протоколирование
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

class DatabaseHelper
{
    private WorksheetCollection _sheets;
    private Workbook _workbook;

    public DatabaseHelper(WorksheetCollection sheets, Workbook workbook)
    {
        _sheets = sheets;
        _workbook = workbook;
    }

    public void ViewDatabase()
    {
        foreach (Worksheet sheet in _sheets)
        {
            Console.WriteLine($"\nТаблица: {sheet.Name}");
            int rows = sheet.Cells.MaxDataRow + 1;
            int cols = sheet.Cells.MaxDataColumn + 1;

            // Определяем максимальную длину строки для каждого столбца
            int[] columnWidths = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    int length = sheet.Cells[i, j].StringValue.Length;
                    if (length > columnWidths[j])
                    {
                        columnWidths[j] = length;
                    }
                }
            }

            // Выводим заголовки столбцов
            for (int j = 0; j < cols; j++)
            {
                Console.Write(sheet.Cells[0, j].StringValue.PadRight(columnWidths[j] + 2));
            }
            Console.WriteLine();

            // Выводим строки таблицы
            for (int i = 1; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(sheet.Cells[i, j].StringValue.PadRight(columnWidths[j] + 2));
                }
                Console.WriteLine();
            }
        }
    }


    public void DeleteElement()
    {
        Console.Write("Введите имя таблицы: ");
        string tableName = Console.ReadLine();
        if (!_sheets.Any(sheet => sheet.Name == tableName))
        {
            Console.WriteLine("Таблица не найдена.");
            return;
        }

        Console.Write("Введите ключ элемента для удаления: ");
        string key = Console.ReadLine();

        Worksheet sheet = _sheets[tableName];
        int keyColumnIndex = GetKeyColumnIndex(sheet);
        int rowIndex = GetRowIndexByKey(sheet, key, keyColumnIndex);

        if (rowIndex != -1)
        {
            sheet.Cells.DeleteRow(rowIndex);
            Console.WriteLine("Элемент успешно удален.");
            SaveChanges();
        }
        else
        {
            Console.WriteLine("Элемент не найден.");
        }
    }

    public void UpdateElement()
    {
        Console.Write("Введите имя таблицы: ");
        string tableName = Console.ReadLine();
        if (!_sheets.Any(sheet => sheet.Name == tableName))
        {
            Console.WriteLine("Таблица не найдена.");
            return;
        }

        Console.Write("Введите ключ элемента для корректировки: ");
        string key = Console.ReadLine();

        Worksheet sheet = _sheets[tableName];
        int keyColumnIndex = GetKeyColumnIndex(sheet);
        int rowIndex = GetRowIndexByKey(sheet, key, keyColumnIndex);

        if (rowIndex != -1)
        {
            Console.Write("Введите новое значение: ");
            string newValue = Console.ReadLine();
            sheet.Cells[rowIndex, keyColumnIndex + 1].PutValue(newValue);
            Console.WriteLine("Элемент успешно обновлен.");
            SaveChanges();
        }
        else
        {
            Console.WriteLine("Элемент не найден.");
        }
    }

    public void AddElement()
    {
        Console.Write("Введите имя таблицы: ");
        string tableName = Console.ReadLine();
        if (!_sheets.Any(sheet => sheet.Name == tableName))
        {
            Console.WriteLine("Таблица не найдена.");
            return;
        }

        Console.Write("Введите новый элемент (через запятую): ");
        string newElement = Console.ReadLine();

        Worksheet sheet = _sheets[tableName];
        int rows = sheet.Cells.MaxDataRow + 1;
        string[] values = newElement.Split(',');

        if (values.Length > sheet.Cells.MaxDataColumn + 1)
        {
            Console.WriteLine("Количество значений превышает количество столбцов в таблице.");
            return;
        }

        for (int i = 0; i < values.Length; i++)
        {
            sheet.Cells[rows, i].PutValue(values[i].Trim());
        }

        Console.WriteLine("Элемент успешно добавлен.");
        SaveChanges();
    }

    public void ExecuteQueries()
    {
        Console.WriteLine("Выберите запрос:");
        Console.WriteLine("1. Определить общее количество заказов за ? месяц ? года.");
        Console.WriteLine("2. Определить количество заказов для каждого типа услуг.");
        Console.WriteLine("3. Определить среднюю стоимость услуг для каждого типа услуг.");
        Console.WriteLine("4. Определить общую стоимость всех услуг для каждого клиента.");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Query1();
                break;
            case "2":
                Query2();
                break;
            case "3":
                Query3();
                break;
            case "4":
                Query4();
                break;
            default:
                Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                break;
        }
    }

    private void Query1()
    {
        var orders = GetTableData("Заказы");

        Console.Write("Введите месяц (1-12): ");
        int month = int.Parse(Console.ReadLine());
        Console.Write("Введите год: ");
        int year = int.Parse(Console.ReadLine());

        var result = from order in orders
                     where IsDateInMonthYear(order["Дата заказа"].ToString(), month, year)
                     select order;

        int orderCount = result.Count();
        Console.WriteLine($"Количество заказов за {month} месяц {year} года: {orderCount}");
    }

    private bool IsDateInMonthYear(string dateString, int month, int year)
    {
        if (DateTime.TryParse(dateString, out DateTime date))
        {
            return date.Month == month && date.Year == year;
        }
        return false;
    }

    private void Query2()
    {
        var orders = GetTableData("Заказы");
        var services = GetTableData("Услуги");
        var serviceTypes = GetTableData("Типы услуг");

        var result = from order in orders
                     join service in services on order["Код услуги"].ToString() equals service["Код услуги"].ToString()
                     join serviceType in serviceTypes on service["Код типа"].ToString() equals serviceType["Код типа"].ToString()
                     group order by serviceType["Название"].ToString() into g
                     select new
                     {
                         ServiceType = g.Key,
                         OrderCount = g.Count()
                     };

        foreach (var item in result)
        {
            Console.WriteLine($"Тип услуги: {item.ServiceType}, Количество заказов: {item.OrderCount}");
        }
    }

    private void Query3()
    {
        var services = GetTableData("Услуги");
        var serviceTypes = GetTableData("Типы услуг");

        var result = from service in services
                     join serviceType in serviceTypes on service["Код типа"].ToString() equals serviceType["Код типа"].ToString()
                     group service by serviceType["Название"].ToString() into g
                     select new
                     {
                         ServiceType = g.Key,
                         AverageCost = g.Average(s => (decimal)Convert.ToDouble(s["Стоимость"]))
                     };

        foreach (var item in result)
        {
            Console.WriteLine($"Тип услуги: {item.ServiceType}, Средняя стоимость: {item.AverageCost}");
        }
    }

    private void Query4()
    {
        var clients = GetTableData("Клиенты");
        var orders = GetTableData("Заказы");
        var services = GetTableData("Услуги");

        var result = from order in orders
                     join client in clients on order["Код клиента"].ToString() equals client["Код клиента"].ToString()
                     join service in services on order["Код услуги"].ToString() equals service["Код услуги"].ToString()
                     group new { order, service } by client["Код клиента"].ToString() into g
                     select new
                     {
                         Client = g.Key,
                         TotalCost = g.Sum(o => (int)Convert.ToDouble(o.order["Количество"]) * (decimal)Convert.ToDouble(o.service["Стоимость"]))
                     };

        foreach (var item in result)
        {
            Console.WriteLine($"Клиент: {item.Client}, Общая стоимость услуг: {item.TotalCost}");
        }
    }

    private List<Dictionary<string, object>> GetTableData(string tableName)
    {
        Worksheet sheet = _sheets[tableName];
        int rows = sheet.Cells.MaxDataRow + 1;
        int cols = sheet.Cells.MaxDataColumn + 1;

        var data = new List<Dictionary<string, object>>();
        var headers = new List<string>();

        for (int j = 0; j < cols; j++)
        {
            headers.Add(sheet.Cells[0, j].StringValue);
        }

        for (int i = 1; i < rows; i++)
        {
            var rowData = new Dictionary<string, object>();
            for (int j = 0; j < cols; j++)
            {
                rowData[headers[j]] = sheet.Cells[i, j].Value;
            }
            data.Add(rowData);
        }

        return data;
    }

    private int GetKeyColumnIndex(Worksheet sheet)
    {
        int cols = sheet.Cells.MaxDataColumn + 1;
        for (int j = 0; j < cols; j++)
        {
            if (sheet.Cells[0, j].StringValue.Contains("Код"))
            {
                return j;
            }
        }
        return -1;
    }

    private int GetRowIndexByKey(Worksheet sheet, string key, int keyColumnIndex)
    {
        int rows = sheet.Cells.MaxDataRow + 1;
        for (int i = 1; i < rows; i++)
        {
            if (sheet.Cells[i, keyColumnIndex].StringValue == key)
            {
                return i;
            }
        }
        return -1;
    }

    private void SaveChanges()
    {
        try
        {
            _workbook.Save("LR5-var7.xls");
            Console.WriteLine("Изменения успешно сохранены.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении изменений: {ex.Message}");
        }
    }
}
