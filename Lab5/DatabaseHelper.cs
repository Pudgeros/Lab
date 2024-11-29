using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Cells;

public class DatabaseHelper
{
    private WorksheetCollection _sheets;
    private Workbook _workbook;
    private List<Client> _clients;
    private List<Order> _orders;
    private List<Service> _services;
    private List<ServiceType> _serviceTypes;

    public DatabaseHelper(WorksheetCollection sheets, Workbook workbook)
    {
        _sheets = sheets;
        _workbook = workbook;
        LoadData();
    }

    private void LoadData()
    {
        _clients = GetTableData<Client>("Клиенты", row => new Client(
            int.Parse(row[0]),
            row[1],
            row[2],
            row[3],
            row[4]
        ));

        _orders = GetTableData<Order>("Заказы", row => new Order(
            int.Parse(row[0]),
            int.Parse(row[1]),
            DateTime.Parse(row[2]),
            int.Parse(row[3]),
            int.Parse(row[4])
        ));

        _services = GetTableData<Service>("Услуги", row => new Service(
            int.Parse(row[0]),
            int.Parse(row[1]),
            row[2],
            decimal.Parse(row[3].Replace(" р.", ""))
        ));

        _serviceTypes = GetTableData<ServiceType>("Типы услуг", row => new ServiceType(
            int.Parse(row[0]),
            row[1]
        ));
    }

    private List<T> GetTableData<T>(string tableName, Func<string[], T> createObject)
    {
        Worksheet sheet = _sheets[tableName];
        int rows = sheet.Cells.MaxDataRow + 1;
        int cols = sheet.Cells.MaxDataColumn + 1;

        var data = new List<T>();

        for (int i = 1; i < rows; i++)
        {
            var rowData = new string[cols];
            for (int j = 0; j < cols; j++)
            {
                rowData[j] = sheet.Cells[i, j].StringValue;
            }
            data.Add(createObject(rowData));
        }

        return data;
    }

    public void ViewDatabase()
    {
        Console.WriteLine("Клиенты:");
        foreach (var client in _clients)
        {
            Console.WriteLine(client);
        }

        Console.WriteLine("Заказы:");
        foreach (var order in _orders)
        {
            Console.WriteLine(order);
        }

        Console.WriteLine("Услуги:");
        foreach (var service in _services)
        {
            Console.WriteLine(service);
        }

        Console.WriteLine("Типы услуг:");
        foreach (var serviceType in _serviceTypes)
        {
            Console.WriteLine(serviceType);
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
        Console.Write("Введите месяц (1-12): ");
        int month = int.Parse(Console.ReadLine());
        Console.Write("Введите год: ");
        int year = int.Parse(Console.ReadLine());

        var result = _orders.Where(order => order.OrderDate.Month == month && order.OrderDate.Year == year);

        int orderCount = result.Count();
        Console.WriteLine($"Количество заказов за {month} месяц {year} года: {orderCount}");
    }

    private void Query2()
    {
        var result = from order in _orders
                     join service in _services on order.ServiceId equals service.ServiceId
                     join serviceType in _serviceTypes on service.ServiceTypeId equals serviceType.ServiceTypeId
                     group order by serviceType.Name into g
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
        var result = from service in _services
                     join serviceType in _serviceTypes on service.ServiceTypeId equals serviceType.ServiceTypeId
                     group service by serviceType.Name into g
                     select new
                     {
                         ServiceType = g.Key,
                         AverageCost = g.Average(s => s.Cost)
                     };

        foreach (var item in result)
        {
            Console.WriteLine($"Тип услуги: {item.ServiceType}, Средняя стоимость: {item.AverageCost}");
        }
    }

    private void Query4()
    {
        var result = from order in _orders
                     join client in _clients on order.ClientId equals client.ClientId
                     join service in _services on order.ServiceId equals service.ServiceId
                     group new { order, service } by client.ClientId into g
                     select new
                     {
                         Client = g.Key,
                         TotalCost = g.Sum(o => o.order.Quantity * o.service.Cost)
                     };

        foreach (var item in result)
        {
            Console.WriteLine($"Клиент: {item.Client}, Общая стоимость услуг: {item.TotalCost}");
        }
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
