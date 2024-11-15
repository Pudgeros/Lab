using System;
using System.Collections.Generic;

public partial class TaskSolver
{
    // Метод для переноса первого элемента в конец списка
    public static void MoveFirstElementToEnd<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new ArgumentException("Список не должен быть пустым или null");
        }

        T firstElement = list[0];
        list.RemoveAt(0);
        list.Add(firstElement);
    }

    // Вспомогательный метод для вывода списка
    private static void PrintList<T>(List<T> list)
    {
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    // Вспомогательный метод для ввода списка пользователем
    private static List<T> ReadListFromUser<T>()
    {
        List<T> list = new List<T>();
        Console.WriteLine("Введите элементы списка (по одному в строке, для завершения введите пустую строку):");

        while (true)
        {
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                break;
            }

            // Преобразование ввода в тип T
            T item = (T)Convert.ChangeType(input, typeof(T));
            list.Add(item);
        }

        return list;
    }

    public static void MainForTask1(string[] args)
    {
        List<string> stringList = ReadListFromUser<string>();

        if (stringList.Count == 0)
        {
            Console.WriteLine("Список пуст. Программа завершена.");
            return;
        }

        Console.WriteLine("Исходный список:");
        PrintList(stringList);

        MoveFirstElementToEnd(stringList);

        Console.WriteLine("Список после переноса первого элемента в конец:");
        PrintList(stringList);
    }
}
