using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinedProject
{
    // Класс Кот, реализующий интерфейс IMeowable
    public class Cat : IMeowable
    {
        // Свойство для имени кота
        public string Name { get; private set; }

        // Конструктор для создания кота с указанием имени
        public Cat(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя кота не может быть пустым или null.");
            }
            Name = name;
        }

        // Метод для преобразования кота в текстовую форму
        public override string ToString()
        {
            return $"кот: {Name}";
        }

        // Метод для мяуканья кота
        public void Meow()
        {
            Console.WriteLine($"{Name}: мяу!");
        }

        // Метод для мяуканья кота N раз
        public void Meow(int times)
        {
            if (times <= 0)
            {
                throw new ArgumentException("Количество мяуканий должно быть положительным числом.");
            }
            string meows = string.Join("-", Enumerable.Repeat("мяу", times));
            Console.WriteLine($"{Name}: {meows}!");
        }
    }
}
