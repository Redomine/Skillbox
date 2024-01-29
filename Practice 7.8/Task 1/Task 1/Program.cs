using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Repository repository = new Repository();

            //repository.AddStreamReader();
            Console.WriteLine(
                "Введите ReadAll, чтоб вывести все данные на экран\n" +
                "Введите ReadById, чтоб получить работника по Id\n" +
                "Введите Add, чтобы дополнить данные\n" +
                "Введите Del, чтобы удалить сотрудника по Id\n" +
                "Введите SortBy для сортировки по выбранному параметру\n" +
                "Введите BetweenTwoDates для вывода сотрудников родившихся в определенном периоде времени)");
            string workMode = Console.ReadLine();


            if (workMode == "Del")
            {
                Console.WriteLine("Введите Id");
                repository.DeleteWorker(Console.ReadLine());
            }

            switch (workMode)
            {
                case "Add":
                    Console.WriteLine("Введите имя нового сотрудника");
                    Worker worker = new Worker(Console.ReadLine());
                    repository.AddWorker(worker);
                    break;
                case "Del":
                    Console.WriteLine("Введите Id");
                    repository.DeleteWorker(Console.ReadLine());
                    break;
                case "ReadAll":
                    repository.PrintData(repository.workers);
                    break;
                case "ReadById":
                    Console.WriteLine("Введите Id");
                    List<Worker> workerById = (List<Worker>)repository.GetWorkerById(int.Parse(Console.ReadLine()));
                    if (workerById != null)
                    {
                        repository.PrintData(workerById);
                    }
                    else
                    {
                        Console.WriteLine("Такого работника нет");
                    }
                    break;
                case "SortBy":

                    Console.WriteLine("Введите имя поля");
                    List<Worker> sortedWorkers = repository.workers;
                    string sortPara = Console.ReadLine();
                    //repository.workers.Sort(new Comparison<Worker>((x, y) => String.Compare(x.Name, y.Name)))
                    switch (sortPara)
                    {
                        case "Name":
                            sortedWorkers = sortedWorkers.OrderBy(x => x.Name).ToList();
                            break;
                        case "DateOfBirth":
                            sortedWorkers = sortedWorkers.OrderBy(x => x.DateOfBirth).ToList();
                            break;
                        case "PlaceOfBirth":
                            sortedWorkers = sortedWorkers.OrderBy(x => x.PlaceOfBirth).ToList();
                            break;
                    }

                    repository.PrintData(sortedWorkers);
                    break;

                case "BetweenTwoDates":
                    Console.WriteLine("Введите дату 1 в формате гг.мм.дд.");
                    DateTime dateFrom = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Введите дату 2 в формате гг.мм.дд.");
                    DateTime dateTo = DateTime.Parse(Console.ReadLine());

                    repository.PrintData(repository.GetWorkersBetweenTwoDates(dateFrom, dateTo));
                    break;

            }

            Console.ReadKey();
        }
    }
}
