using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashSet <int> ints = new HashSet <int> ();
            int number;
            while (true)
            {
                Console.WriteLine("Введите число:");
                number = int.Parse(Console.ReadLine());
                if (!ints.Contains(number))
                {
                    ints.Add(number);
                    Console.WriteLine("Число успешно добавлено");
                }
                else
                {
                    Console.WriteLine("Число уже вводилось");
                }
            }
        }
    }
}
