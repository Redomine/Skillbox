using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_1.Classes.Task_1_3;
using Task_1.Interfaces;
using Task_1.Windows;
using static Task_1.Classes.Workers;

namespace Task_1.Classes
{
    

    internal class Account
    {



        public Account(int id, int moneyValue, bool isAccountOpen, string fullName)
        {
            this.id = id;
            this.IsAccountOpen = isAccountOpen;
            this.MoneyValue = moneyValue;
            this.FullName = fullName;

        }

        public Account(int id, string fullName)
        {
            this.id = id;
            this.isAccountOpen = true;
            this.moneyValue = 0;
            this.FullName = fullName;
        }

        internal int id;
        internal int moneyValue;
        internal bool isAccountOpen;
        internal string fullName;
        internal string accName;
        public int Id { get { return id; } }
        public int MoneyValue { get { return moneyValue; } set { moneyValue = value; } }
        public bool IsAccountOpen { get { return isAccountOpen; } set { isAccountOpen = value; } }

        public string FullName { get { return fullName; } set { fullName = value; } }

        public string AccName { get { return accName; } set {  accName = value; } }

        public void PutMoneyInto(int moneyValue) {  this.MoneyValue += moneyValue; }

        public void TakeMoneyFrom(int moneyValue) { this.MoneyValue -= moneyValue; }


    }

    internal class ReguralBankAccount : Account
    {
        [JsonConstructor]
        public ReguralBankAccount(int id, int moneyValue, bool isAccountOpen, string fullName) : base(id, moneyValue, isAccountOpen, fullName)
        {
            this.accName = "Обычный аккаунт";
            
        }

        public ReguralBankAccount(int id, string fullName) : base(id, fullName)
        {
            this.accName = "Обычный аккаунт";
        }
    }

    internal class DepositBankAccount : Account
    {

        [JsonConstructor]
        public DepositBankAccount(int id, int moneyValue, bool isAccountOpen, string fullName) : base(id, moneyValue, isAccountOpen, fullName)
        {
            this.accName = "Депозитный аккаунт";
        }

        public DepositBankAccount(int id, string fullName) : base(id, fullName)
        {
            this.accName = "Депозитный аккаунт";
        }
    }

    internal class MoneyExchanger : IMoneyExchanger<Account, string>
    {
        public delegate void ExchangerHandler(string message);
        public event ExchangerHandler Notify;
        public string GetReport(Account targetAccount)
        {
            return $"Новый баланс аккаунта {targetAccount.FullName} равен {targetAccount.MoneyValue}";
        }

        public void ExchageMoney(Account activeAccount, Account targetAccount, int moneyValue, IEmployee worker)
        {
            if(activeAccount != targetAccount)
            {
                activeAccount.TakeMoneyFrom(moneyValue);
            }
            targetAccount.PutMoneyInto(moneyValue);

            if(activeAccount == targetAccount)
            {
                Notify?.Invoke($"{activeAccount.AccName} с Id {activeAccount.Id} был пополнен на {moneyValue}. Операцию выполнил {worker.WorkerName} в {DateTime.Now}");
            }
            else 
            {
                Notify?.Invoke($"{activeAccount.AccName} с Id {activeAccount.Id} выполнил обмен с {targetAccount.AccName} с Id {targetAccount.Id} на сумму {moneyValue}. Операцию выполнил {worker.WorkerName} в {DateTime.Now}");
            }
            MessageBox.Show(GetReport(targetAccount));
        }
    }


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

            
            if(newDialog.PhoneNumber.Text != "")
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
 