using System;

public class Time
{
    private byte hours;
    private byte minutes;

    public byte Hours
    {
        get { return hours; }
        set { hours = value; }
    }

    public byte Minutes
    {
        get { return minutes; }
        set { minutes = value; }
    }

    // Конструктор
    public Time(byte hours, byte minutes)
    {
        this.hours = hours;
        this.minutes = minutes;
    }

    // Конструктор копирования
    public Time(Time other)
    {
        this.hours = other.hours;
        this.minutes = other.minutes;
    }

    // Метод для вычитания переменной типа Time
    public Time Subtract(Time other)
    {
        int totalMinutes = this.hours * 60 + this.minutes;
        int otherTotalMinutes = other.hours * 60 + other.minutes;
        int resultTotalMinutes = totalMinutes - otherTotalMinutes;

        if (resultTotalMinutes < 0)
        {
            resultTotalMinutes += 24 * 60; // Переход в предыдущие сутки
        }

        byte resultHours = (byte)(resultTotalMinutes / 60);
        byte resultMinutes = (byte)(resultTotalMinutes % 60);

        return new Time(resultHours, resultMinutes);
    }

    // Унарные операции
    public static Time operator ++(Time time)
    {
        time.minutes++;
        if (time.minutes >= 60)
        {
            time.minutes = 0;
            time.hours++;
            if (time.hours >= 24)
            {
                time.hours = 0;
            }
        }
        return time;
    }

    public static Time operator --(Time time)
    {
        if (time.minutes == 0)
        {
            time.minutes = 59;
            if (time.hours <= 0)
            {
                time.hours = 23;
            }
            else
            {
                time.hours--;
            }
        }
        else
        {
            time.minutes--;
        }
        return time;
    }

    // Операции приведения типа
    public static implicit operator int(Time time)
    {
        return time.hours * 60 + time.minutes;
    }

    public static explicit operator bool(Time time)
    {
        return time.hours != 0 && time.minutes != 0;
    }

    // Бинарные операции
    public static bool operator <(Time t1, Time t2)
    {
        int totalMinutes1 = t1.Hours * 60 + t1.Minutes;
        int totalMinutes2 = t2.Hours * 60 + t2.Minutes;
        return totalMinutes1 < totalMinutes2;
    }

    public static bool operator >(Time t1, Time t2)
    {
        int totalMinutes1 = t1.Hours * 60 + t1.Minutes;
        int totalMinutes2 = t2.Hours * 60 + t2.Minutes;
        return totalMinutes1 > totalMinutes2;
    }

    // Перегрузка метода ToString()
    public override string ToString()
    {
        return $"{hours:D2}:{minutes:D2}";
    }
}

public class Program
{
    public static void Main()
    {
        Time time1 = ReadTime("Введите первое время (часы минуты): ");
        Time time2 = ReadTime("Введите второе время (часы минуты): ");

        Console.WriteLine("time1: " + time1.ToString());
        Console.WriteLine("time2: " + time2.ToString());

        Time result = time1.Subtract(time2);
        Console.WriteLine("Результат вычитания: " + result.ToString());

        // Тестирование унарных операций
        time1++;
        Console.WriteLine("time1++: " + time1.ToString());
        
        time2--;
        Console.WriteLine("time2--: " + time2.ToString());

        // Тестирование операций приведения типа
        int totalMinutes1 = time1;
        Console.WriteLine("totalMinutes1: " + totalMinutes1);
        
        int totalMinutes2 = time2;
        Console.WriteLine("totalMinutes2: " + totalMinutes2);

        bool isNonZero1 = (bool)time1;
        Console.WriteLine("isNonZero1: " + isNonZero1);
        
        bool isNonZero2 = (bool)time2;
        Console.WriteLine("isNonZero2: " + isNonZero2);

        // Тестирование бинарных операций
        bool isLess = time1 < time2;
        Console.WriteLine("time1 < time2: " + isLess);

        bool isGreater = time1 > time2;
        Console.WriteLine("time1 > time2: " + isGreater);
    }

    // Метод для чтения времени с проверкой
    public static Time ReadTime(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            if (parts.Length == 2 &&
                byte.TryParse(parts[0], out byte hours) &&
                byte.TryParse(parts[1], out byte minutes) &&
                hours >= 0 && hours <= 23 && minutes >= 0 &&
                minutes <= 59)
            {
                return new Time(hours, minutes);
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }
    }
}
