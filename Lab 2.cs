using System;

public class BaseClass
{
    private int field1;
    private int field2;
    private int field3;

    public int Field1
    {
        get { return field1; }
        set { field1 = value; }
    }

    public int Field2
    {
        get { return field2; }
        set { field2 = value; }
    }

    public int Field3
    {
        get { return field3; }
        set { field3 = value; }
    }
    
    // Конструктор без параметров
    public BaseClass()
    {
        this.field1 = 0;
        this.field2 = 0;
        this.field3 = 0;
    }

    // Конструктор с параметрами
    public BaseClass(int field1, int field2, int field3)
    {
        this.field1 = field1;
        this.field2 = field2;
        this.field3 = field3;
    }

    // Конструктор копирования
    public BaseClass(BaseClass other)
    {
        this.field1 = other.field1;
        this.field2 = other.field2;
        this.field3 = other.field3;
    }

    // Метод для вычисления произведения полей
    public int CalculateProduct()
    {
        return field1 * field2 * field3;
    }

    // Перегрузка метода ToString()
    public override string ToString()
    {
        return $"Поле1: {field1}, Поле2: {field2}, Поле3: {field3}";
    }
}

public class Box : BaseClass
{
    // Конструктор
    public Box(int length, int width, int height)
        : base(length, width, height)
    {
    }

    // Конструктор копирования
    public Box(Box other)
        : base(other)
    {
    }

    // Метод для вычисления объема коробки
    public int CalculateVolume()
    {
        return CalculateProduct();
    }

    // Метод для вычисления площади поверхности коробки
    public int CalculateSurfaceArea()
    {
        int length = Field1;
        int width = Field2;
        int height = Field3;
        return 2 * (length * width + width * height + height * length);
    }

    // Метод для проверки, является ли коробка кубом
    public bool IsCube()
    {
        return Field1 == Field2 && Field2 == Field3;
    }

    // Перегрузка метода ToString()
    public override string ToString()
    {
        return $"Box: Длина = {Field1}, Ширина = {Field2}, Высота = {Field3}";
    }
}

public class Program
{
public static void Main()
    {
        CheckBaseClass();
        CheckBoxClass();
    }

    public static void CheckBaseClass()
    {
        Console.Write("Введите 3 целых числа (через пробел): ");
        string input = Console.ReadLine();
        string[] parts = input.Split(' ');

        if (parts.Length == 3 &&
            int.TryParse(parts[0], out int field1) &&
            int.TryParse(parts[1], out int field2) &&
            int.TryParse(parts[2], out int field3))
        {
            BaseClass baseObj = new BaseClass(field1, field2, field3);
            Console.WriteLine(baseObj.ToString());
            Console.WriteLine("Произведение: " + baseObj.CalculateProduct());

            BaseClass baseObjCopy = new BaseClass(baseObj);
            Console.WriteLine(baseObjCopy.ToString());
            Console.WriteLine("Произведение (copy): " + baseObjCopy.CalculateProduct());
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Введите три целых числа через пробел.");
            CheckBaseClass();
        }
    }

    public static void CheckBoxClass()
    {
        Console.Write("Введите длину, ширину, высоту (через пробел): ");
        string input = Console.ReadLine();
        string[] parts = input.Split(' ');

        if (parts.Length == 3 &&
            int.TryParse(parts[0], out int length) &&
            int.TryParse(parts[1], out int width) &&
            int.TryParse(parts[2], out int height))
        {
            Box box = new Box(length, width, height);
            Console.WriteLine(box.ToString());
            Console.WriteLine("Объем: " + box.CalculateVolume());
            Console.WriteLine("Площадь поверхности: " + box.CalculateSurfaceArea());
            Console.WriteLine("Куб?: " + box.IsCube());

            Box boxCopy = new Box(box);
            Console.WriteLine(boxCopy.ToString());
            Console.WriteLine("Объем (copy): " + boxCopy.CalculateVolume());
            Console.WriteLine("Площадь поверхности (copy): " + boxCopy.CalculateSurfaceArea());
            Console.WriteLine("Куб? (copy): " + boxCopy.IsCube());
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Введите три целых числа через пробел.");
            CheckBoxClass();
        }
    }
}
