using System;
using System.Collections.Generic;

public partial class TaskSolver
{
    // Метод для удаления элементов с одинаковыми соседями
    public static void RemoveElementsWithEqualNeighbors(LinkedList<string> list)
    {
        if (list == null || list.Count < 2)
        {
            throw new ArgumentException("Список должен содержать не менее двух элементов");
        }

        LinkedListNode<string> current = list.First;
        LinkedListNode<string> previous = null;
        LinkedListNode<string> next = current.Next;

        while (next != null)
        {
            if (previous != null && previous.Value == next.Value)
            {
                LinkedListNode<string> toRemove = current;
                current = current.Next;
                list.Remove(toRemove);
            }
            else
            {
                previous = current;
                current = current.Next;
            }
            next = current.Next;
        }

        // Проверка первого и последнего элемента
        if (list.First.Value == list.Last.Value)
        {
            list.RemoveFirst();
        }
    }

    // Вспомогательный метод для вывода списка
    private static void PrintList(LinkedList<string> list)
    {
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    // Вспомогательный метод для ввода списка пользователем
    private static LinkedList<string> ReadLinkedListFromUser()
    {
        LinkedList<string> list = new LinkedList<string>();
        Console.WriteLine("Введите элементы списка (по одному в строке, для завершения введите пустую строку):");

        while (true)
        {
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                break;
            }

            list.AddLast(input);
        }

        return list;
    }

    public static void MainForTask2(string[] args)
    {
        LinkedList<string> list = ReadLinkedListFromUser();

        if (list.Count < 2)
        {
            Console.WriteLine("Список должен содержать не менее двух элементов. Программа завершена.");
            return;
        }

        Console.WriteLine("Исходный список:");
        PrintList(list);

        RemoveElementsWithEqualNeighbors(list);

        Console.WriteLine("Список после удаления элементов с одинаковыми соседями:");
        PrintList(list);
    }
}
