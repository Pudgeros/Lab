using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinedProject
{
    class Program
    {
        // Метод для вызова мяуканья у набора объектов
        static void MakeAllMeow(IEnumerable<IMeowable> meowables)
        {
            foreach (var meowable in meowables)
            {
                meowable.Meow();
            }
        }

        static void Main(string[] args)
        {
            // Создаем котов
            Cat barsik = new Cat("Барсик");
            Cat murzik = new Cat("Мурзик");
            Cat vasya = new Cat("Вася");

            // Создаем робота-кота
            RobotCat robotCat = new RobotCat("RoboCat 3000");

            // Создаем обертки для отслеживания количества мяуканий
            MeowCounter barsikCounter = new MeowCounter(barsik);
            MeowCounter murzikCounter = new MeowCounter(murzik);
            MeowCounter vasyaCounter = new MeowCounter(vasya);
            MeowCounter robotCatCounter = new MeowCounter(robotCat);

            // Создаем список мяукающих объектов
            List<IMeowable> meowables = new List<IMeowable> { barsikCounter, murzikCounter, vasyaCounter, robotCatCounter };

            // Вызываем мяуканье у всех объектов
            MakeAllMeow(meowables);

            // Вызываем мяуканье у кота Барсик три раза
            barsikCounter.Meow(3);

            // Вызываем мяуканье у всех объектов еще раз
            MakeAllMeow(meowables);

            // Выводим количество мяуканий для каждого объекта
            Console.WriteLine($"Барсик мяукал {barsikCounter.MeowCount} раз");
            Console.WriteLine($"Мурзик мяукал {murzikCounter.MeowCount} раз");
            Console.WriteLine($"Вася мяукал {vasyaCounter.MeowCount} раз");
            Console.WriteLine($"RoboCat 3000 мяукал {robotCatCounter.MeowCount} раз");

            // Создаем несколько экземпляров дробей
            Fraction f1 = new Fraction(1, 3);
            Fraction f2 = new Fraction(2, 3);
            Fraction f3 = new Fraction(1, 2);

            // Примеры использования методов
            Fraction sum = f1 + f2;
            Fraction sub = f1 - f2;
            Fraction mul = f1 * f2;
            Fraction div = f1 / f2;

            // Выводим примеры и результаты их выполнения
            Console.WriteLine($"{f1} + {f2} = {sum}");
            Console.WriteLine($"{f1} - {f2} = {sub}");
            Console.WriteLine($"{f1} * {f2} = {mul}");
            Console.WriteLine($"{f1} / {f2} = {div}");

            // Примеры операций с целыми числами
            Fraction sumInt = f1 + 2;
            Fraction subInt = f1 - 2;
            Fraction mulInt = f1 * 2;
            Fraction divInt = f1 / 2;

            // Выводим примеры и результаты их выполнения
            Console.WriteLine($"{f1} + 2 = {sumInt}");
            Console.WriteLine($"{f1} - 2 = {subInt}");
            Console.WriteLine($"{f1} * 2 = {mulInt}");
            Console.WriteLine($"{f1} / 2 = {divInt}");

            // Вычисляем f1.sum(f2).div(f3).minus(5)
            Fraction result = (f1 + f2) / f3 - 5;
            Console.WriteLine($"({f1} + {f2}) / {f3} - 5 = {result}");

            // Примеры сравнения дробей
            Fraction f4 = new Fraction(2, 6);
            Fraction f5 = new Fraction(1, 3);
            Console.WriteLine($"{f4} == {f5} : {f4.Equals(f5)}"); // Должно быть true
            Console.WriteLine($"{f1} == {f2} : {f1.Equals(f2)}"); // Должно быть false

            // Примеры клонирования дробей
            Fraction f6 = (Fraction)f1.Clone();
            Console.WriteLine($"Клон {f1} : {f6}");
            Console.WriteLine($"{f1} == {f6} : {f1.Equals(f6)}"); // Должно быть true

            // Примеры получения вещественного значения
            Console.WriteLine($"Вещественное значение {f1} : {f1.GetRealValue()}");
            Console.WriteLine($"Вещественное значение {f2} : {f2.GetRealValue()}");

            // Примеры установки числителя и знаменателя
            f1.SetNumerator(3);
            f1.SetDenominator(4);
            Console.WriteLine($"Обновленная дробь {f1} : {f1.GetRealValue()}");
        }
    }
}
