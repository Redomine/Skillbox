using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task_1
{
    class Repository
    {
        private string fileName = "EmployeesData.txt";



        public List<Worker> workers { get; set; }

        public Repository()
        {
            workers = GetAllWorkers();
        }


        public void PrintData(List<Worker> workersList)
        {

            if (System.IO.File.Exists(fileName))
            {

                foreach (Worker worker in workersList)
                {

                    string data =
                        worker.Id + "#" +
                        worker.AddTime + "#" +
                        worker.Name + "#" +
                        worker.Age + "#" +
                        worker.Height + "#" +
                        worker.DateOfBirth + "#" +
                        worker.PlaceOfBirth;
                    Console.WriteLine(data.Replace("#", " "));
                }
            }
            else
            {
                Console.WriteLine("Файл не существует");
            }

        }

        public int GetNewId()
        {
            string line = "";

            if (!System.IO.File.Exists(fileName))
            {
                return 1;
            }

            if (System.IO.File.ReadAllLines(fileName).Length == 0)
            {
                return 1;
            }

            string[] lastLine = File.ReadLines(fileName).Last().Split('#');
            int Id = Convert.ToInt32(lastLine[0]) + 1;


            return Id;
        }

        public List<Worker> GetAllWorkers()
        {

            List<Worker> workersList = new List<Worker>();
            string line = "";
            if (System.IO.File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] dataLine = line.Split('#');
                        int id = int.Parse(dataLine[0]);
                        DateTime addTime = DateTime.Parse(dataLine[1]);
                        string name = dataLine[2];
                        int age = int.Parse(dataLine[3]);
                        int height = int.Parse(dataLine[4]);
                        DateTime dateOfBirth = DateTime.Parse(dataLine[5]);
                        string placeOfBirth = dataLine[6];
                        Worker worker = new Worker(id, name, age, height, dateOfBirth, placeOfBirth);
                        workersList.Add(worker);

                    }
                }
            }


            return workersList;

            // здесь происходит чтение из файла
            // и возврат массива считанных экземпляров

        }

        public object GetWorkerById(int id)
        {
            List<Worker> data = new List<Worker>();
            foreach (Worker worker in workers)
            {
                if (worker.Id == id)
                {
                    data.Add(worker);
                    return data;
                }
            }
            return null;
            // происходит чтение из файла, возвращается Worker
            // с запрашиваемым ID

        }

        public void DeleteWorker(string id)
        {
            // считывается файл, находится нужный Worker
            // происходит запись в файл всех Worker,
            // кроме удаляемого
            List<string> dataList = new List<string>();
            string line = "";
            using (StreamReader sr = new StreamReader(fileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string lineId = line.Split('#')[0];
                    if (lineId != id)
                    {
                        dataList.Add(line);
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (string data in dataList)
                {
                    sw.WriteLine(data);
                }
            }
        }

        public void AddWorker(Worker worker)
        {
            // присваиваем worker уникальный ID,
            // дописываем нового worker в файл
            worker.Id = GetNewId();
            string employeeData =
                worker.Id + "#" +
                worker.AddTime + "#" +
                worker.Name + "#" +
                worker.Age + "#" +
                worker.Height + "#" +
                worker.DateOfBirth + "#" +
                worker.PlaceOfBirth;
            using (StreamWriter sw = new StreamWriter(fileName, append: true))
            {
                sw.WriteLine(employeeData);
            }
        }

        public List<Worker> GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            List<Worker> workersBetweenDates = new List<Worker>();

            foreach (Worker worker in workers)
            {
                if ((worker.DateOfBirth > dateFrom) && (worker.DateOfBirth < dateTo))
                {
                    workersBetweenDates.Add(worker);
                }

            }

            return workersBetweenDates;
            // здесь происходит чтение из файла
            // фильтрация нужных записей
            // и возврат массива считанных экземпляров
        }
    }
}
