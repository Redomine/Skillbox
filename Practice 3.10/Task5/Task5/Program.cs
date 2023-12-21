using System;

namespace Task5
{
    internal class Program
    {
        static int isIntOrExit(string inputValue, int randomValue = -1)
        {

            if (int.TryParse(inputValue, out int number))
            {
                return number;
            }
            else
            {
                if (randomValue >= 0)
                {
                    Console.WriteLine($"Загаданным числом было {randomValue}");
                    Console.ReadKey();
                    Environment.Exit(0);
                    return randomValue;
                }
                else
                {
                    Console.WriteLine("Введенное не является целым числом. Приложение будет закрыто.");
                    Console.ReadKey();
                    Environment.Exit(0);
                    return 0;
                }

            }

        }
        static void Main(string[] args)
        {
            Random randomize = new Random();
            Console.WriteLine("Введите длину последовательности:");
            int sequenceLen = isIntOrExit(Console.ReadLine());

            int randomNumber = randomize.Next(0, sequenceLen);
            Console.WriteLine(randomNumber);

            int userNumber = 0;
            Console.WriteLine("Угадайте число или нажмите Enter, чтобы увидеть ответ:");
            do
            {
                userNumber = isIntOrExit(Console.ReadLine(), randomNumber);
                if (userNumber != randomNumber)
                {
                    Console.WriteLine("Вы ошиблись, попробуйте еще:");
                }
            } while (userNumber != randomNumber);

            Console.WriteLine("Вы угадали");
            Console.ReadKey();
        }
    }
}
