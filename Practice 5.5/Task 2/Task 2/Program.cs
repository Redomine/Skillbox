using System;
using System.Linq;

namespace Task2
{
    internal class Program
    {
        static string SplitAndReverseText(string text)
        {
            string[] WordArray = SplitText(text);

            string ResultString = "";
            foreach (string word in WordArray.Reverse())
            {
                ResultString += word + " ";
            }
            return ResultString;
        }
        static string[] SplitText(string text)
        {
            return text.Split(' ');
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