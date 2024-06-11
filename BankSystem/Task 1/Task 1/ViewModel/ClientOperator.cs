using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Task_1.Classes.Task_1_3;
using Task_1.Classes;
using Task_1.Interfaces;
using Task_1.Windows;
using Task_1.Model;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using BankClassLibrary.Exceptions;

namespace Task_1.ViewModel
{
    internal class ClientOperator
    {
        internal Repository repository;
        internal DbOperator dbOperator;
        public ClientOperator(Repository data) 
        { 
            repository = data;
            dbOperator = new DbOperator();

        }

        public void CreateClient(MainWindow mainWindow, IEmployee worker)
        {
            EditClient newDialog = new EditClient();
            repository.NewWindowAsDialog(mainWindow, newDialog);

            if (AreAllFieldsFilled(newDialog) && newDialog.editable)
            {
                Client newClient = CreateNewClient(newDialog, worker);

                repository.ClientsDb.Add(newClient);

                string fullName = GetFullName(newClient);
                AccountClientFactory accountFactory = new AccountClientFactory();
                accountFactory.Notify += repository.KeepAnEventLog;


                List<Account> accList = accountFactory.CreateAccounts(newClient.Id, fullName, worker);
                DepositBankAccount newDepositBankAccount = accList[0] as DepositBankAccount;
                ReguralBankAccount newReguralBankAccount = accList[1] as ReguralBankAccount;


                CreateBankAccount<ReguralBankAccount>(repository.BankAccountsDb, newReguralBankAccount, repository.dbBankAccountsName);
                CreateBankAccount<DepositBankAccount>(repository.DepoBankAccountsDb, newDepositBankAccount, repository.dbDepoBankAccountsName);

                dbOperator.SerializeClientsToJson(repository.ClientsDb, repository.dbName);
                repository.ClientsDb = dbOperator.DeserializeClientsFromJson<Client>(repository.dbName);

                mainWindow.lvClients.Items.Refresh();
            }
            else
            {
                try
                {
                    throw new ClientException("Должны быть заполнены все поля");
                }
                catch { }
                
            }
        }

        private void ShowErrorMessage()
        {
            MessageBox.Show("Должны быть заполнены все поля");
        }

        private bool AreAllFieldsFilled(EditClient newDialog)
        {
            string firstName = newDialog.FirstName.Text;
            string secondName = newDialog.SecondName.Text;
            string surName = newDialog.SurName.Text;
            string phoneNumber = newDialog.PhoneNumber.Text;
            string passportNumber = newDialog.PassportNumber.Text;
            if (new[] { firstName, secondName, surName, phoneNumber, passportNumber }.All(x => x != "")) 
            {
                return true;
            }
            return false;
            

        }

        private Client CreateNewClient(EditClient newDialog, IEmployee worker)
        {
            string firstName = newDialog.FirstName.Text;
            string secondName = newDialog.SecondName.Text;
            string surName = newDialog.SurName.Text;
            string phoneNumber = newDialog.PhoneNumber.Text;
            string passportNumber = newDialog.PassportNumber.Text;

            return new Client(firstName, secondName, surName, phoneNumber, passportNumber)
            {
                TypeOfChanges = "Добавление строки",
                AutorOfChanges = worker.WorkerName,
                NameOfChangedData = "Вся строка"
            };
        }

        private string GetFullName(Client newClient)
        {
            return String.Join(" ", new[] { newClient.FirstName, newClient.SecondName, newClient.SurName });
        }

        public void CreateBankAccount<T>(ObservableCollection<T> accountDb, T newAccount, string dbName)
        {
            accountDb.Add(newAccount);

            dbOperator.SerializeClientsToJson(accountDb, dbName);
        }

        public void EditClient(MainWindow mainWindow, Client client, IEmployee worker)
        {
            AccountClientFactory accountClientFactory = new AccountClientFactory();
            accountClientFactory.Notify += repository.KeepAnEventLog;

            if (client != null)
            {
                Client dbClient = dbOperator.FindClient(repository.ClientsDb, client.Id);
                if (worker.LevelOfAccess == "0")
                {
                    ChangePhone newDialog = new ChangePhone();
                    repository.NewWindowAsDialog(mainWindow, newDialog);
                    //ChangePhoneNumber(newDialog, client, worker);
                    accountClientFactory.EditPhone(newDialog.currentNumber, dbClient, worker);
                    dbOperator.SerializeClientsToJson(repository.ClientsDb, repository.dbName);
                    repository.ClientsDb = dbOperator.DeserializeClientsFromJson<Client>(repository.dbName);
                }
                else
                {
                    EditClient newDialog = new EditClient();
                    repository.NewWindowAsDialog(mainWindow, newDialog);
                    ChangeClientDetails(newDialog, client, worker);
                }
            }
            mainWindow.lvClients.ItemsSource = repository.ClientsDb;
            mainWindow.lvClients.Items.Refresh();
        }

        private void ChangeClientDetails(EditClient newDialog, Client client, IEmployee worker)
        {
            string firstName = newDialog.FirstName.Text;
            string secondName = newDialog.SecondName.Text;
            string surName = newDialog.SurName.Text;
            string phoneNumber = newDialog.PhoneNumber.Text;
            string passportNumber = newDialog.PassportNumber.Text;
            string nameOfChanged = "";
            Dictionary<string, string> paraNamesParaValues = new Dictionary<string, string>()
                            {
                                { "FirstName", firstName },
                                { "SecondName", secondName },
                                { "SurName", surName },
                                { "PhoneNumber", phoneNumber },
                                { "PassportNumber", passportNumber },
                            };
            Client dbClient = dbOperator.FindClient(repository.ClientsDb, client.Id);
            AccountClientFactory factory = new AccountClientFactory();
            factory.Notify += repository.KeepAnEventLog;
            nameOfChanged = factory.ReplacePartOfClientsDb(nameOfChanged, dbClient, paraNamesParaValues, worker);
            if (nameOfChanged != "")
            {
                dbClient.AutorOfChanges = worker.WorkerName;
                dbClient.ChangesDate = DateTime.Now;
                dbClient.NameOfChangedData = nameOfChanged;
                dbClient.TypeOfChanges = "Изменение параметров";
            }

            dbOperator.SerializeClientsToJson(repository.ClientsDb, repository.dbName);
            repository.ClientsDb = dbOperator.DeserializeClientsFromJson<Client>(repository.dbName);
        }


    }
}
