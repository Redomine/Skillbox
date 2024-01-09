using System;

namespace Task1
{
    internal class Program
    {
        static string[] SplitText(string text)
        {
            return text.Split(' ');
        }

        static void PrintWords(string[] text)
        {
            foreach (string word in text)
            {
                Console.WriteLine(word);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите предложение:");
            string InputData = Console.ReadLine();
            PrintWords(SplitText(InputData));
            Console.ReadKey();
        }
    }
}
