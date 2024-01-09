using System;
using System.Linq;

namespace Task2
{
    internal class Program
    {
        static string SplitAndReverseText(string text)
        {
            string[] WordArray = text.Split(' ');
            return Reverse(WordArray);
        }

        static string Reverse(string[] WordArray)
        {
            string ResultString = "";
            foreach (string word in WordArray.Reverse())
            {
                ResultString += word + " ";
            }
            return ResultString;
        }

        static void Main()
        {
            Console.WriteLine("Введите предложение:");
            string InputData = Console.ReadLine();
            Console.WriteLine(SplitAndReverseText(InputData));
            Console.ReadKey();
        }
    }
}