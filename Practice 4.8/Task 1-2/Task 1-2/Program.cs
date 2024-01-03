using System;

namespace Task_1_2
{
    internal class Program
    {
        static int isIntOrExit(string inputValue)
        {

            if (int.TryParse(inputValue, out int number))
            {
                return number;
            }
            else
            {
                Console.WriteLine("Введенное не является целым числом. Приложение будет закрыто.");
                Console.ReadKey();
                Environment.Exit(0);
                return 0;
            }

        }
        static void Main(string[] args)
        {

            Console.WriteLine("Введите количество строк:");
            int rowsNum = isIntOrExit(Console.ReadLine());
            Console.WriteLine("Введите количество колонок:");
            int columnsNum = isIntOrExit(Console.ReadLine());

            int[,] matrix_1 = new int[rowsNum, columnsNum];

            Random r = new Random();

            Console.WriteLine("Первая матрица:");
            int elementsSum = 0;
            for (int i = 0; i < rowsNum; i++)
            {
                for (int j = 0; j < columnsNum; j++)
                {
                    matrix_1[i, j] = r.Next(10);
                    elementsSum += matrix_1[i, j];
                    Console.Write($"{matrix_1[i, j]} ");
                }
                Console.WriteLine();
            }


            Console.WriteLine($"Сумма элементов матрицы: {elementsSum}. При нажатии любой клавиши, будет выполнено задание 2");
            Console.ReadKey();


            Console.WriteLine("Вторая матрица:");
            int[,] matrix_2 = new int[rowsNum, columnsNum];

            for (int i = 0; i < rowsNum; i++)
            {
                for (int j = 0; j < columnsNum; j++)
                {
                    matrix_2[i, j] = r.Next(10);
                    Console.Write($"{matrix_2[i, j]} ");
                }
                Console.WriteLine();
            }

            int[,] matrixSum = new int[rowsNum, columnsNum];
            Console.WriteLine("Сумма матриц:");
            for (int i = 0; i < rowsNum; i++)
            {
                for (int j = 0; j < columnsNum; j++)
                {
                    matrixSum[i, j] = matrix_1[i, j] + matrix_2[i, j];
                    Console.Write($"{matrixSum[i, j]} ");
                }
                Console.WriteLine();
            }


            Console.ReadKey();
        }
    }
}
