using System;
using System.Globalization;

namespace MySpace
{
    internal class Program
    {
        private Random random;

        public Program(Random random)
        {
            this.random = random;
        }
        
        static void Main(string[] args)
        {
            Random random = new Random();
            Program program = new Program(random);

            Console.Write("Введите номер задания (1-39, только нечетные): ");
            string input = Console.ReadLine();
            int task_num;

            if (int.TryParse(input, out task_num))
            {
                switch (task_num)
                {
                    case 1:
                        program.HandleTask1();
                        break;

                    case 3:
                        program.HandleTask3();
                        break;

                    case 5:
                        program.HandleTask5();
                        break;

                    case 7:
                        program.HandleTask7();
                        break;
                    
                    case 9:
                        program.HandleTask9();
                        break;
                    
                    case 11:
                        program.HandleTask11();
                        break;
                    
                    case 13:
                        program.HandleTask13();
                        break;
                    
                    case 15:
                        program.HandleTask15();
                        break;
                    
                    case 17:
                        program.HandleTask17();
                        break;
                    
                    case 19:
                        program.HandleTask19();
                        break;
                    
                    case 21:
                        program.HandleTask21();
                        break;
                    
                    case 23:
                        program.HandleTask23();
                        break;
                    
                    case 25:
                        program.HandleTask25();
                        break;
                    
                    case 27:
                        program.HandleTask27();
                        break;
                    
                    case 29:
                        program.HandleTask29();
                        break;
                    
                    case 31:
                        program.HandleTask31();
                        break;
                    
                    case 33:
                        program.HandleTask33();
                        break;
                    
                    case 35:
                        program.HandleTask35();
                        break;
                    
                    case 37:
                        program.HandleTask37();
                        break;
                    
                    case 39:
                        program.HandleTask39();
                        break;

                    default:
                        Console.WriteLine("Такого задания нет :(");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }

        public void HandleTask1()
        {
            Console.Write("Введите вещественное число: ");
            string input = Console.ReadLine();
            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double x))
            {
                double result = fraction(x);
                Console.WriteLine("Дробная часть: {0:F4}", result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите вещественное число (через точку).");
            }
        }

        public void HandleTask3()
        {
            Console.Write("Введите цифру: ");
            string input = Console.ReadLine();
            if (char.TryParse(input, out char x) && ('0' <= x && x <= '9'))
            {
                int result = charToNum(x);
                Console.WriteLine("Результат: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите цифру.");
            }
        }

        public void HandleTask5()
        {
            Console.Write("Введите целое число: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int x))
            {
                bool result = is2Digits(x);
                Console.WriteLine("Результат: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число.");
            }
        }

        public void HandleTask7()
        {
            Console.Write("Введите левую и правую границу, число x (через пробел): ");
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            if (parts.Length == 3 &&
                int.TryParse(parts[0], out int a) &&
                int.TryParse(parts[1], out int b) &&
                int.TryParse(parts[2], out int x))
            {
                bool result = isInRange(a, b, x);
                Console.WriteLine("Результат: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите три целых числа через пробел.");
            }
        }
        
        public void HandleTask9()
        {
            Console.Write("Введите 3 целых числа (через пробел): ");
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            if (parts.Length == 3 &&
                int.TryParse(parts[0], out int a) &&
                int.TryParse(parts[1], out int b) &&
                int.TryParse(parts[2], out int c))
            {
                bool result = isEqual(a, b, c);
                Console.WriteLine("Результат: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите три целых числа через пробел.");
            }
        }
        
        public void HandleTask11()
        {
            Console.Write("Введите целое число: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int x))
            {
                int result = abs(x);
                Console.WriteLine("Модуль числа: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число.");
            }
        }
        
        public void HandleTask13()
        {
            Console.Write("Введите целое число: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int x))
            {
                bool result = is35(x);
                Console.WriteLine("Результат: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число.");
            }
        }
        
        public void HandleTask15()
        {
            Console.Write("Введите 3 целых числа (через пробел): ");
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            if (parts.Length == 3 &&
                int.TryParse(parts[0], out int x) &&
                int.TryParse(parts[1], out int y) &&
                int.TryParse(parts[2], out int z))
            {
                int result = max3(x, y, z);
                Console.WriteLine("Максимальное число: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите три целых числа через пробел.");
            }
        }
        
        public void HandleTask17()
        {
            Console.Write("Введите 2 целых числа (через пробел): ");
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            if (parts.Length == 2 &&
                int.TryParse(parts[0], out int x) &&
                int.TryParse(parts[1], out int y))
            {
                int result = sum2(x, y);
                Console.WriteLine("Результат: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите три целых числа через пробел.");
            }
        }
        
        public void HandleTask19()
        {
            Console.Write("Введите день недели по счету (в виде числа): ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int x))
            {
                string result = day(x);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число.");
            }
        }
        
        public void HandleTask21()
        {
            Console.Write("Введите целое число: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int x))
            {
                string result = listNums(x);
                Console.WriteLine("Результат: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число.");
            }
        }
        
        public void HandleTask23()
        {
            Console.Write("Введите целое число: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int x))
            {
                string result = chet(x);
                Console.WriteLine("Результат: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число.");
            }
        }
        
        public void HandleTask25()
        {
            Console.Write("Введите целое число: ");
            string input = Console.ReadLine();
            if (long.TryParse(input, out long x))
            {
                int result = numLen(x);
                Console.WriteLine("Результат: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число.");
            }
        }
        
        public void HandleTask27()
        {
            Console.Write("Введите целое число: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int x))
            {
                Console.WriteLine("Результат: ");
                square(x);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число.");
            }
        }
        
        public void HandleTask29()
        {
            Console.Write("Введите целое число: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int x))
            {
                Console.WriteLine("Результат: ");
                rightTriangle(x);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число.");
            }
        }
        
        public void HandleTask31()
        {
            Console.Write("Введите целое число: ");
            string input = Console.ReadLine();
            Console.Write("Введите размер массива: ");
            string input2 = Console.ReadLine();
            if (int.TryParse(input, out int x) && int.TryParse(input2, out int size) && size > 0)
            {
                int[] arr = new int[size];
                for (int i = 0; i < size; i++)
                {
                    arr[i] = random.Next(1, 10); 
                }
                int result = findFirst(arr, x);
                Console.WriteLine("Индекс первого вхождения: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число. Учтите, что размер массива должен быть > 0");
            }
        }
        
        public void HandleTask33()
        {
            Console.Write("Введите размер массива: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int size) && size > 0)
            {
                int[] arr = new int[size];
                for (int i = 0; i < size; i++)
                {
                    arr[i] = random.Next(-9, 10); 
                }
                int result = maxAbs(arr);
                Console.WriteLine("Наибольшее значение по модулю: " + result);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число. Учтите, что размер массива должен быть > 0");
            }
        }
        
        public void HandleTask35()
        {
            Console.Write("Введите номер позиции: ");
            string input3 = Console.ReadLine();
            Console.Write("Введите размер 1-ого массива: ");
            string input = Console.ReadLine();
            Console.Write("Введите размер 2-ого массива: ");
            string input2 = Console.ReadLine();
            if (int.TryParse(input, out int size) && int.TryParse(input2, out int size2) && int.TryParse(input3, out int pos) && size > 0 && size2 > 0)
            {
                int[] arr = new int[size];
                int[] ins = new int[size2];
                for (int i = 0; i < size; i++)
                {
                    arr[i] = random.Next(1, 10); 
                }
                for (int i = 0; i < size2; i++)
                {
                    ins[i] = random.Next(1, 10); 
                }
                int[] result = add(arr, ins, pos);
                Console.Write("Результат: ");
                for (int i = 0; i < arr.Length + ins.Length; i++)
                {
                    Console.Write(result[i] + " ");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число. Учтите, что размер массива должен быть > 0");
            }
        }
        
        public void HandleTask37()
        {
            Console.Write("Введите размер массива: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int size) && size > 0)
            {
                int[] arr = new int[size];
                for (int i = 0; i < size; i++)
                {
                    arr[i] = random.Next(1, 10); 
                }
                int[] result = reverseBack(arr);
                Console.Write("Результат: ");
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(result[i] + " ");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число. Учтите, что размер массива должен быть > 0");
            }
        }
        
        public void HandleTask39()
        {
            Console.Write("Введите целое число: ");
            string input2 = Console.ReadLine();
            Console.Write("Введите размер массива: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int size) && int.TryParse(input2, out int x) && size > 0)
            {
                int[] arr = new int[size];
                for (int i = 0; i < size; i++)
                {
                    arr[i] = random.Next(1, 10); 
                }
                int[] result = findAll(arr, x);
                Console.Write("Результат: ");
                for (int i = 0; i < result.Length; i++)
                {
                    Console.Write(result[i] + " ");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число. Учтите, что размер массива должен быть > 0");
            }
        }

        public double fraction(double x) // Задание 1 (Задание 1 № 1)
        {
            int intPart = (int)x;
            return x - intPart;
        }

        public int charToNum(char x) // Задание 3 (Задание 1 № 3)
        {
            return x - 48;
        }

        public bool is2Digits(int x) // Задание 5 (Задание 1 № 5)
        {
            return (x >= 10 && x <= 99) || (x >= -99 && x <= -10);
        }

        public bool isInRange(int a, int b, int num) // Задание 7 (Задание 1 № 7)
        {
            int min = a < b ? a : b;
            int max = a > b ? a : b;
            return min <= num && num <= max;
        }

        public bool isEqual(int a, int b, int c) // Задание 9 (Задание 1 № 9)
        {
            return (a == b) && (a == c);
        }

        public int abs(int x) // Задание 11 (Задание 2 № 1)
        {
            if (x < 0)
                x = 0 - x;
            return x;
        }

        public bool is35(int x) // Задание 13 (Задание 2 № 3)
        {
            if (x % 3 == 0 && x % 5 == 0)
                return false;
            return (x % 3 == 0 || x % 5 == 0);
        }

        public int max3(int x, int y, int z) // Задание 15 (Задание 2 № 5)
        {
            int max = x;
            max = y > max ? y : max;
            max = z > max ? z : max;
            return max;
        }

        public int sum2(int x, int y) // Задание 17 (Задание 2 № 7)
        {
            int sum = (x + y) >= 10 && (x+y) <= 19 ? 20 : x + y;
            return sum;
        }

        public string day(int x) // Задание 19 (Задание 2 № 9)
        {
            switch (x)
            {
                case 1:
                    return "Понедельник";
                case 2:
                    return "Вторник";
                case 3:
                    return "Среда";
                case 4:
                    return "Четверг";
                case 5:
                    return "Пятница";
                case 6:
                    return "Суббота";
                case 7:
                    return "Воскресенье";
                default:
                    return "Это не день недели.";
            }
        }

        public string listNums(int x) // Задание 21 (Задание 3 № 1)
        {
            string res = "";
            for (int i = 0; i <= x; i++)
                res += i.ToString() + " ";
            return res;
        }

        public string chet(int x) // Задание 23 (Задание 3 № 3)
        {
            string res = "";
            for (int i = 0; i <= x; i += 2)
                res += i.ToString() + " ";
            return res;
        }

        public int numLen(long x) // Задание 25 (Задание 3 № 5)
        {
            int length = 1;
            while (x / 10 > 0)
            {
                x /= 10;
                length++;
            }
            return length;
        }

        public void square(int x) // Задание 27 (Задание 3 № 7)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Console.Write('*');
                }
                Console.WriteLine();
            }
        }

        public void rightTriangle(int x) // Задание 29 (Задание 3 № 9)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j <= x-i; j++)
                {
                    Console.Write(' ');
                }
                for (int j = x-i; j <= x; j++)
                {
                    Console.Write('*');
                }
                Console.WriteLine();
            }
        }

        public int findFirst(int[] arr, int x) // Задание 31 (Задание 4 № 1)
        {
            Console.Write("Массив: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == x) return i;
            }
            return -1;
        }

        public int maxAbs(int[] arr) // Задание 33 (Задание 4 № 3)
        {
            Console.Write("Массив: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            
            int max = arr[0];
            int max_ind = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < 0 && (max < 0 - arr[i]))
                {
                    max = 0-arr[i];
                    max_ind = i;
                }
                else
                {
                    if (arr[i] > max)
                    {
                        max = arr[i];
                        max_ind = i;
                    }
                }
            }

            return arr[max_ind];
        }

        public int[] add(int[] arr, int[] ins, int pos) // Задание 35 (Задание 4 № 5)
        {
            Console.Write("1-ый массив: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            
            Console.Write("2-ой массив: ");
            for (int i = 0; i < ins.Length; i++)
            {
                Console.Write(ins[i] + " ");
            }
            Console.WriteLine();
            
            int[] res = new int[arr.Length + ins.Length];
            
            for (int i = 0; i < pos; i++)
            {
                res[i] = arr[i];
            }
            
            for (int i = 0; i < ins.Length; i++)
            {
                res[pos + i] = ins[i];
            }
            
            for (int i = pos; i < arr.Length; i++)
            {
                res[i + ins.Length] = arr[i];
            }

            return res;
        }

        public int[] reverseBack(int[] arr) // Задание 37 (Задание 4 № 7)
        {
            Console.Write("Массив: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            
            int[] res = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                res[i] = arr[arr.Length - 1 - i];
            }

            return res;
        }

        public int[] findAll(int[] arr, int x) // Задание 39 (Задание 4 № 9)
        {
            int res_size = 0;
            
            Console.Write("Массив: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
                if (arr[i] == x) res_size++;
            }
            Console.WriteLine();

            int[] res = new int[res_size];
            int ind = 0;
            
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == x) res[ind++] = i;
            }

            return res;
        }
    }
}
