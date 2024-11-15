using System;
using System.Collections.Generic;

public partial class TaskSolver
{
    // Метод для переноса первого элемента в конец списка
    public static void MoveFirstElementToEnd(List<string> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new ArgumentException("Список не должен быть пустым или null");
        }

        string firstElement = list[0];
        list.RemoveAt(0);
        list.Add(firstElement);
    }

    // Вспомогательный метод для вывода списка
    private static void PrintList(List<string> list)
    {
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    // Вспомогательный метод для ввода списка пользователем
    private static List<string> ReadListFromUser()
    {
        List<string> list = new List<string>();
        Console.WriteLine("Введите элементы списка (по одному в строке, для завершения введите пустую строку):");

        while (true)
        {
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                break;
            }

            list.Add(input);
        }

        return list;
    }

    public static void MainForTask1(string[] args)
    {
        List<string> list = ReadListFromUser();

        if (list.Count == 0)
        {
            Console.WriteLine("Список пуст. Программа завершена.");
            return;
        }

        Console.WriteLine("Исходный список:");
        PrintList(list);

        MoveFirstElementToEnd(list);

        Console.WriteLine("Список после переноса первого элемента в конец:");
        PrintList(list);
    }
}
