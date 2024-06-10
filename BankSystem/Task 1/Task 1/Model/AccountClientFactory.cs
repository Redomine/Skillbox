using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1.Classes.Task_1_3;
using Task_1.Classes;
using Task_1.Interfaces;
using Task_1.Windows;

namespace Task_1.Model
{
    internal class AccountClientFactory
    {
        public delegate void AccountHandler(string message);
        public event AccountHandler Notify;
        public AccountClientFactory() { }

        public List<Account> CreateAccounts(int id, string fullName, IEmployee worker)
        {
            Notify?.Invoke($"Cоздан аккаунт клиента с депозитным и обычным счетом, Id {id}, полное имя {fullName}. Изменение внес {worker.WorkerName} в {DateTime.Now}");
            return new List<Account> { new DepositBankAccount(id, fullName), new ReguralBankAccount(id, fullName) };

        }

        public void EditPhone(ChangePhone newDialog, Client dbClient, IEmployee worker)
        {


            if (newDialog.PhoneNumber.Text != "")
            {
                dbClient.PhoneNumber = newDialog.PhoneNumber.Text;
                dbClient.AutorOfChanges = worker.WorkerName;
                dbClient.ChangesDate = DateTime.Now;
                dbClient.NameOfChangedData = "PhoneNumber";
                dbClient.TypeOfChanges = "Изменение параметров";

                Notify?.Invoke($"У клиента {dbClient.Id} изменены параметры {dbClient.NameOfChangedData}. Изменение внес {worker.WorkerName} в {DateTime.Now}");
            }

        }

        public string ReplacePartOfClientsDb(string nameOfChanged, Client dbClient, Dictionary<string, string> dbKeys, IEmployee worker)
        {
            foreach (string key in dbKeys.Keys)
            {
                if (dbKeys[key] != "")
                {
                    nameOfChanged += key + " ";
                }
            }
            if (dbKeys["FirstName"] != "") { dbClient.FirstName = dbKeys["FirstName"]; }
            if (dbKeys["SecondName"] != "") { dbClient.SecondName = dbKeys["SecondName"]; }
            if (dbKeys["SurName"] != "") { dbClient.SurName = dbKeys["SurName"]; }
            if (dbKeys["PhoneNumber"] != "") { dbClient.PhoneNumber = dbKeys["PhoneNumber"]; }
            if (dbKeys["PassportNumber"] != "") { dbClient.PassportNumber = dbKeys["PassportNumber"]; }

            Notify?.Invoke($"У клиента {dbClient.Id} изменены параметры {nameOfChanged}. Изменение внес {worker.WorkerName} в {DateTime.Now}");
            return nameOfChanged;
        }


    }
}
