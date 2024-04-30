using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XElement data = CreateData();
            UploadData(data);
        }

        private static XElement CreateData()
        {
            XElement xData = new XElement("Data");
            xData.SetAttributeValue("name", GetString("Введите имя:"));

            XElement xmlAdreessTree = new XElement("Address",
                new XElement("Street", GetString("Введите название улицы:")),
                new XElement("HouseNumber", GetString("Введите номер дома:")),
                new XElement("FlatNumber", GetString("Введите номер квартиры:"))
            );
            xData.Add(xmlAdreessTree);

            XElement xmlPhoneTree = new XElement("Phones",
                new XElement("MobilePhone", GetString("Введите номер мобильного телефона:")),
                new XElement("FlatPhone", GetString("Введите номер домашнего телефона:"))
            );
            xData.Add(xmlPhoneTree);

            return xData;
        }

        private static string GetString(string message)
        {
            Console.WriteLine(message);
            string result = Console.ReadLine();
            return result;
        }

        private static void UploadData(XElement data)
        {
            using (Stream stream = new FileStream("./data.xml", FileMode.Append, FileAccess.Write))
            {
                data.Save(stream);
                stream.Close();
            }
        }

    }
}
