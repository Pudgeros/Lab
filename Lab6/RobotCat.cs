using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinedProject
{
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
