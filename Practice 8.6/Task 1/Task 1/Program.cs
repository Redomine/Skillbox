using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Первый цикл:");
            List<int> randomList = CreateRandomNumbers();
            PrintList(randomList);
            Console.WriteLine("Второй цикл:");
            List<int> refactorList = RefactorList(randomList);
            PrintList(refactorList);
            Console.ReadKey();
        }

        private static List<int> CreateRandomNumbers()
        {
            Random rnd = new Random();
            return Enumerable.Range(0, 100).Select(i => rnd.Next(0, 101)).ToList();
        }

        private static void PrintList(List<int> list)
        {
            foreach (int element in list)
            {
                Console.WriteLine(element);
            };
        }

        private static List<int> RefactorList(List<int> list)
        {
            return list.Where(number => number <= 25 || 50 <= number).ToList();
        }


    }
}
