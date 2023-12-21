using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите целое число: ");
            string numberText = Console.ReadLine();
            if (int.TryParse(numberText, out int number))
            {
                if ((number % 2) == 0)
                {
                    Console.WriteLine($"Число {number} четное");
                }
                else
                {
                    Console.WriteLine($"Число {number} нечетное");
                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Введенные данные не являеются целым числом.");
                Console.ReadKey();
            }

        }
    }
}


