using System;
using System.Collections.Generic;

public partial class TaskSolver
{
    // Метод для удаления элементов с одинаковыми соседями
    public static void RemoveElementsWithEqualNeighbors<T>(LinkedList<T> list)
    {
        if (list == null || list.Count < 2)
        {
            throw new ArgumentException("Список должен содержать не менее двух элементов");
        }

        LinkedListNode<T> current = list.First;
        LinkedListNode<T> previous = null;
        LinkedListNode<T> next = current.Next;

        while (next != null)
        {
            if (previous != null && EqualityComparer<T>.Default.Equals(previous.Value, next.Value))
            {
                LinkedListNode<T> toRemove = current;
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
        if (EqualityComparer<T>.Default.Equals(list.First.Value, list.Last.Value))
        {
            list.RemoveFirst();
        }
    }

    // Вспомогательный метод для вывода списка
    private static void PrintList<T>(LinkedList<T> list)
    {
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    // Вспомогательный метод для ввода списка пользователем
    private static LinkedList<T> ReadLinkedListFromUser<T>()
    {
        LinkedList<T> list = new LinkedList<T>();
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
            list.AddLast(item);
        }

        return list;
    }

    public static void MainForTask2(string[] args)
    {
        LinkedList<string> stringList = ReadLinkedListFromUser<string>();

        if (stringList.Count < 2)
        {
            Console.WriteLine("Список должен содержать не менее двух элементов. Программа завершена.");
            return;
        }

        Console.WriteLine("Исходный список:");
        PrintList(stringList);

        RemoveElementsWithEqualNeighbors(stringList);

        Console.WriteLine("Список после удаления элементов с одинаковыми соседями:");
        PrintList(stringList);
    }
}
