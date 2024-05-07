using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using Task_1_3.Interafaces;

namespace Task_1_3.Classes
{
    internal class Repository
    {
        
        public Repository() 
        {
        }

        private List<Client> DeserializeClientsFromJson()
        {
            string json = File.ReadAllText("_dBClients_2.json");
            List<Client> listOfClients = JsonConvert.DeserializeObject<List<Client>>(json);
            return listOfClients;
        }

        private void SerializeClientsToJson(List<Client> clients)
        {
            string json = JsonConvert.SerializeObject(clients);
            File.WriteAllText("_dBClients_2.json", json);
        }

        public ObservableCollection<String> GetClientData()
        {
            List<Client> listOfClients = DeserializeClientsFromJson();
            ObservableCollection<String> sourceForListbox = new ObservableCollection<String>();
            foreach (Client client in listOfClients)
            {
                //String passportNumber = GetClientPassport(employee, client);
                string dataString = 
                    $"Имя:{client.FirstName}, " +
                    $"Фамилия:{client.SecondName}, " +
                    $"Отчество:{client.Surname}, " +
                    $"Номер телефона:{client.PhoneNumber}, " +
                    $"Серия/номер паспорта:{client.PassportNumber}, " +
                    $"Дата изменений:{ client.ChangesDate}, " +
                    $"Имя измененных данных:{client.NameOfChangedData}, " +
                    $"Тип измененных данных:{client.TypeOfChanges}, " +
                    $"Автор изменений:{client.AutorOfChanges}";
                sourceForListbox.Add(dataString);
            }
            return sourceForListbox;
        }

        public ObservableCollection<String> EditDataByAccessLevel(IEmployee employee, ObservableCollection<String> clients)
        {
            if (employee.LevelOfAccess != "0")
            {
                return clients;
            }

            for (int i = 0; i < clients.Count; i++)
            {
                int leftIndex = clients[i].IndexOf("Серия/номер паспорта:");
                int rightIndex = clients[i].IndexOf("Дата изменений:");
                //21 - количество символов в Серия/номер паспорта:
                string newClient = clients[i].Remove(leftIndex + 21, rightIndex - leftIndex - 21);
                newClient = newClient.Insert(leftIndex + 21, "[ДАННЫЕ СКРЫТЫ], ");
                clients[i] = newClient;
            }

            return clients;
        }


        private Dictionary<string, string> EditDictOfParams(Dictionary<string, string> dictOfParams, string paraName, string newValue)
        {
            string targetParam = dictOfParams[paraName];
            targetParam = targetParam.Split(':')[0] + ":" + newValue;
            dictOfParams[paraName] = targetParam;
            return dictOfParams;
        }


        private string CreateEditedDataString(string dataString, string paraName, string newValue, IEmployee worker, string fullNameOfChangedParams)
        {

            List<string> parameters = dataString.Split(new string[] { ", " }, StringSplitOptions.None).ToList();

            Dictionary<string, string> dictOfParams = new Dictionary<string, string>()
{
                { "FirstName", parameters[0]},
                { "SecondName", parameters[1]},
                { "SurName", parameters[2]},
                { "PhoneNumber", parameters[3]},
                { "PassportNumber", parameters[4]},
                { "ChangesDate", parameters[5]},
                { "NameOfChangedData", parameters[6]},
                { "TypeOfChanges", parameters[7]},
                { "AutorOfChanges", parameters[8]}
            };


            dictOfParams = EditDictOfParams(dictOfParams, paraName, newValue);
            dictOfParams = EditDictOfParams(dictOfParams, "ChangesDate", DateTime.Now.ToString().Replace(":", "-"));
            dictOfParams = EditDictOfParams(dictOfParams, "NameOfChangedData", fullNameOfChangedParams);
            dictOfParams = EditDictOfParams(dictOfParams, "TypeOfChanges", "Редактирование строки");
            dictOfParams = EditDictOfParams(dictOfParams, "AutorOfChanges", worker.WorkerName);

            string result = String.Empty;
            foreach (KeyValuePair<string, string> entry in dictOfParams)
            {
                result += dictOfParams[entry.Key] + ", ";
            }

            result = result.Substring(0, result.Length - 2);

            return result;
        }

        public string ChangeElementOfListBox(
            string dataString, 
            ObservableCollection<String> ClientData, 
            ObservableCollection<String> editedToViewClientData, 
            string paraName, 
            string newValue,
            IEmployee worker,
            string fullNameOfChangedParams)
        {

            int indexOfData = editedToViewClientData.IndexOf(dataString);
            dataString = ClientData[indexOfData];
            string newDataString = CreateEditedDataString(dataString, paraName, newValue, worker, fullNameOfChangedParams);


            ClientData[indexOfData] = newDataString;

            List<Client> clients = new List<Client>();
            
            foreach (string data in ClientData)
            {
                clients.Add(new Client(data));
            }
            SerializeClientsToJson(clients);


            return newDataString;
        }

        public void CreateNewClient(Dictionary<string, string> clientDict, IEmployee worker)
        {
            List<Client> clients = DeserializeClientsFromJson();
            Client client = new Client(clientDict["FirstName"], clientDict["SecondName"], clientDict["SurName"], clientDict["PhoneNumber"], clientDict["PassportNumber"])
            {
                AutorOfChanges = worker.WorkerName,
                TypeOfChanges = "Добавление нового клиента",
                ChangesDate = DateTime.Now.ToString(),
                NameOfChangedData = "Вся строка"
            };

            if (client.FirstName == "" || client.SecondName == "" || client.Surname == "" || client.PassportNumber == "" || client.PhoneNumber == "")
            {
                MessageBox.Show("Должны быть значения для всех параметров");
            }
            else
            {
                clients.Add(client);
                SerializeClientsToJson(clients);
            }

        }

    }



}
