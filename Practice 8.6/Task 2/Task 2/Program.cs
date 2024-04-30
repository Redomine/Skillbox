using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Наполнение книжки");
            Dictionary<String, String> phoneBook = createPhoneBook();
            Console.WriteLine("Чтение книжки");
            FillOrReadPhoneBook(phoneBook, false);
            Console.ReadKey();
        }

        private static Dictionary<String, String> createPhoneBook()
        {
            Dictionary<String, String> phoneBook = new Dictionary<String, String>();
            FillOrReadPhoneBook(phoneBook, true);
            return phoneBook;
        }


        private static bool CanAddPhoneNumber(String phoneNumber, Dictionary<string, string> dict) 
        {
            return dict.TryGetValue(phoneNumber, out string value);
        }


        private static Dictionary<String, String> AddNumber(Dictionary<string, string> dict, string phoneNumber)
        {
            String phoneName;
            if (!CanAddPhoneNumber(phoneNumber, dict))
            {
                Console.WriteLine("Введите имя адресата:");
                phoneName = Console.ReadLine();
                dict.Add(phoneNumber, phoneName);
            }
            else
            {
                Console.WriteLine("Такой номер уже есть в книжке");
            }
            return dict;
        }

        private static Dictionary<String, String> FillOrReadPhoneBook(Dictionary<string, string> dict, bool fillBook)
        {
            String phoneNumber;
            

            while (true) 
            {
                Console.WriteLine("Введите номер телефона, или пустую строку для окончания наполнения:");
                
                phoneNumber = Console.ReadLine();
                if(phoneNumber == "")
                {
                    return dict;
                }

                if (fillBook)
                {
                    AddNumber(dict, phoneNumber);
                }
                else
                {
                    Console.WriteLine(dict[phoneNumber]);
                    
                }

            }
            return dict;
        }



    }
}
