using System;

namespace Task_3
{
    internal class Program
    {
        static bool isSimple(int number)
        {
            int i = 1;
            while (i < (number - 1))
            {
                if (number % i == 0 && i > 1)
                {
                    return false;
                }
                i++;
            }
            return true;
        }
        static void Main(string[] args)
        {
            {
                Console.Write("Введите целое число: ");
                string numberText = Console.ReadLine();
                if (int.TryParse(numberText, out int number))
                {

                    if (isSimple(number))
                    {
                        Console.WriteLine($"Число {number} является простым");
                    }
                    else
                    {
                        Console.WriteLine($"Число {number} не является простым");
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
}
