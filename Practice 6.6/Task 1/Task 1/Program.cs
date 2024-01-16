using System;
using System.IO;

namespace Task_1
{
    internal class Program
    {

        static string GetConsoleWriteLine(string message)
        {
            string data = "#";
            Console.WriteLine(message);
            data += Console.ReadLine();
            return data;
        }

        static string CreateEmployeeData()
        {
            string id = GetConsoleWriteLine("Введите ID сотрудника");

            string fullName = GetConsoleWriteLine("Введите полное имя сотрудника");

            string height = GetConsoleWriteLine("Введите рост");
            string birthDate = GetConsoleWriteLine("Введите дату рождения");
            string birthCity = GetConsoleWriteLine("Введите место рождение");

            string employeeData = id + "#" + DateTime.Now + fullName + height + birthDate + birthCity;

            return employeeData;
        }

        static void ReadData()
        {
            string fileName = "EmployeesData.txt";
            string line = "";
            if (System.IO.File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line.Replace("#", " "));
                    }
                }
            }
            else
            {
                Console.WriteLine("Файл не существует");
            }

        }

        static void WriteData()
        {
            using (StreamWriter sw = new StreamWriter("EmployeesData.txt", append: true))
            {

                sw.WriteLine(CreateEmployeeData());
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите 1, чтоб вывести данные на экран, введите 2, чтобы дополнить данные.");
            string workMode = Console.ReadLine();

            if (workMode == "2")
            {
                WriteData();
            }
            else
            {

                ReadData();
            }

            Console.ReadLine();
        }
    }
}
