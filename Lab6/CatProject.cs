using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinedProject
{
    // Интерфейс для мяукающих объектов
    public interface IMeowable
    {
        void Meow();
        void Meow(int times);
    }

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

    // Класс-обертка для отслеживания количества мяуканий
    public class MeowCounter : IMeowable
    {
        private readonly IMeowable _meowable;
        public int MeowCount { get; private set; }

        public MeowCounter(IMeowable meowable)
        {
            _meowable = meowable;
            MeowCount = 0;
        }

        public void Meow()
        {
            _meowable.Meow();
            MeowCount++;
        }

        public void Meow(int times)
        {
            _meowable.Meow(times);
            MeowCount += times;
        }
    }

    // Другой класс, реализующий интерфейс IMeowable
    public class RobotCat : IMeowable
    {
        public string Model { get; private set; }

        public RobotCat(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentException("Модель робота-кота не может быть пустой или null.");
            }
            Model = model;
        }

        public void Meow()
        {
            Console.WriteLine($"{Model}: мяу!");
        }

        public void Meow(int times)
        {
            if (times <= 0)
            {
                throw new ArgumentException("Количество мяуканий должно быть положительным числом.");
            }
            string meows = string.Join("-", Enumerable.Repeat("мяу", times));
            Console.WriteLine($"{Model}: {meows}!");
        }
    }
}