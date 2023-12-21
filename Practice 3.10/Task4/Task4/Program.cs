using System;
using System.Collections.Generic;
using System.Linq;

namespace Task4
{
    internal class Program
    {
        static int isInt(string inputValue)
        {
            if (int.TryParse(inputValue, out int number))
            {
                return number;
            }
            else
            {
                Console.WriteLine("Введенное не является целым числом. Приложение будет закрыто.");
                Console.ReadKey();
                Environment.Exit(0);
                return 0;
            }

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите длину последовательности:");
            int sequenceLen = isInt(Console.ReadLine());
            List<int> list = new List<int>();
            Console.WriteLine("Введите целые числа из которых состоит последовательность:");
            for (int i = 0; i < sequenceLen; i++)
            {
                list.Add(isInt(Console.ReadLine()));
            }
            Console.WriteLine($"Минимальное целое число в последовательности {list.Min()}");
            Console.ReadKey();
        }
    }
}
