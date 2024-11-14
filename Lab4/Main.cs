using System;

namespace myspace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите номер задания (1-5): ");
            string input = Console.ReadLine();
            int task_num;
            
            if (int.TryParse(input, out task_num))
            {
                switch (task_num)
                {
                    case 1:
                        TaskSolver.MainForTask1(args);
                        break;

                    case 2:
                        TaskSolver.MainForTask2(args);
                        break;

                    case 3:
                        TaskSolver.MainForTask3(args);
                        break;

                    case 4:
                        TaskSolver.MainForTask4(args);
                        break;

                    case 5:
                        TaskSolver.MainForTask5(args);
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
    }
}
