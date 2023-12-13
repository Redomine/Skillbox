using System;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fullName = "Иванов Иван Иванович";
            int age = 30;
            string email = "test@gmail.com";
            double programmingPoints = 50.2;
            double mathPoitns = 44.6;
            double physicsPoints = 68;

            string dataPattern = "ФИО: {0} \nВозраст: {1} \nАдрес электронной почты: {2}  \nБаллы по программированию: {3} \nБаллы по математике: {4} \nБаллы по физике: {5}";
            Console.WriteLine(
                dataPattern,
                fullName,
                age,
                email,
                programmingPoints,
                mathPoitns,
                physicsPoints);
            Console.ReadKey();

            double pointsSum = physicsPoints + mathPoitns + physicsPoints;
            double arithmeticalMean = pointsSum / 3;

            string calculationPattern = "\nСредний балл по всем предметам: {0}";
            Console.WriteLine(
                calculationPattern,
                arithmeticalMean
                );

            Console.ReadKey();
        }
    }
}
