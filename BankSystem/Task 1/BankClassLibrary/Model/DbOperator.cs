using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1.Classes.Task_1_3;

namespace Task_1.Model
{
    public class DbOperator
    {
        public ObservableCollection<T> DeserializeClientsFromJson<T>(string dbAdress)
        {
            string json = File.ReadAllText(dbAdress);
            ObservableCollection<T> listOfClients = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
            return listOfClients;
        }

        public void SerializeClientsToJson<T>(ObservableCollection<T> clients, string dbAdress)
        {
            string json = JsonConvert.SerializeObject(clients);
            File.WriteAllText(dbAdress, json);
        }

        public Client FindClient(ObservableCollection<Client> ClientsDb, int id)
        {
            foreach (Client dbClient in ClientsDb)
            {
                if (dbClient.Id == id)
                {
                    return dbClient;
                }
            }
            return null;
        }

    }
}
