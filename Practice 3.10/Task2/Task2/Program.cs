using System;
using System.Collections.Generic;

namespace Task2
{
    internal class Program
    {
        //Проверяем корректность введнного числа/буквы. Если некорректно - возвращаем 0
        static int getIntValue(string numberText)
        {

            List<string> picCards = new List<string>() { "J", "K", "Q", "T" };

            if (int.TryParse(numberText, out int number) && number <= 10)
            {
                return number;
            }
            else
            {
                if (picCards.Contains(numberText))
                {
                    return 10;
                }
                else
                {
                    return 0;
                }

            }

        }

        static void Main(string[] args)
        {
            Console.Write("Сколько у вас карт?: ");
            string cardNumberText = Console.ReadLine();
            int valuesSumm = 0;

            Console.WriteLine("Введите номинал ваших карт по очереди:");
            if (int.TryParse(cardNumberText, out int numberOfCards))
            {
                for (int i = 0; i < numberOfCards; i++)
                {
                    int cardNumber = getIntValue(Console.ReadLine());
                    if (cardNumber == 0)
                    {
                        do
                        {
                            Console.WriteLine("Таких карт не бывает. Введите верное значение:");
                            cardNumber = getIntValue(Console.ReadLine());
                        } while (cardNumber == 0);

                    }
                    valuesSumm += cardNumber;

                }

                Console.WriteLine($"Сумма ваших очков {valuesSumm}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Такого количества карт не может быть.");
                Console.ReadKey();
            }
        }
    }
}
