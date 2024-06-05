using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Shapes;
using Task_1.Classes;
using Task_1.Classes.Task_1_3;
using Task_1.Interfaces;
using Task_1.Windows;
using static Task_1.Classes.Workers;

namespace Task_1.Model
{
    internal class Repository
    {
        private readonly string dbName = "_dBClients.json";
        private readonly string dbBankAccountsName = "_dBBankAccounts.json";
        private readonly string dbDepoBankAccountsName = "_dBDepositBankAccounts.json";
        private readonly string dbEventLog = "_dBEventLog.txt";

        public ObservableCollection<Client> ClientsDb { get; set; }

        List<Account> VariantsOfAcc {  get; set; }

        List<Account> VariantsOfTargetAcc { get; set; }

        List<Account> TargetsDb { get; set; }

        public ObservableCollection<ReguralBankAccount> BankAccountsDb { get; set; }

        public ObservableCollection<DepositBankAccount> DepoBankAccountsDb { get; set; }

        public List<IEmployee> Workers { get; set; }

        public Repository()
        {
            ClientsDb = DeserializeClientsFromJson<Client>(dbName);
            BankAccountsDb = DeserializeClientsFromJson<ReguralBankAccount>(dbBankAccountsName);
            DepoBankAccountsDb = DeserializeClientsFromJson<DepositBankAccount>(dbDepoBankAccountsName);
            Consultant consultant = new Consultant();
            Manager manager = new Manager();
            Workers = new List<IEmployee>
            {
                manager,
                consultant
            };
        }

        public void FillBankWindowData(BankAccountWindow window)
        {
            if (window.ActiveAccount != null)
            {
                Account activeAcc = window.ActiveAccount.SelectedItem as Account;
               
                ReguralBankAccount regularAccount = BankAccountsDb[window.ActiveAccount.SelectedIndex];
                DepositBankAccount depoAccount = DepoBankAccountsDb[window.ActiveAccount.SelectedIndex];

                window.CurrentAccValue.Content = regularAccount.MoneyValue.ToString();
                window.CurrentAccDepoValue.Content = depoAccount.MoneyValue.ToString();

                VariantsOfAcc = CreateVariantsOfAcc(activeAcc.Id);
            }
        }
        public void CreateBoxes(BankAccountWindow window)
        {
            CreateComboBoxSource<Account>(window.IsDepoOrRegural, "AccName", 0, VariantsOfAcc);

            TargetsDb = new List<Account>();
            foreach (Account acc in BankAccountsDb)
            {
                if (acc != window.ActiveAccount.SelectedItem) { TargetsDb.Add(acc); }
            }

            CreateComboBoxSource<Account>(window.TargetAccount, "FullName", 0, TargetsDb);
            Account targetAcc = window.TargetAccount.SelectedItem as Account;
            VariantsOfTargetAcc = CreateVariantsOfAcc(targetAcc.Id);
            CreateComboBoxSource<Account>(window.TargetIsDepoOrRegural, "AccName", 0, VariantsOfTargetAcc);


        }

        public void RefreshBankWindow(BankAccountWindow window)
        {
            Account activeAccount = (Account)window.ActiveAccount.SelectedItem;
            Account targetAcc = window.TargetAccount.SelectedItem as Account;
            if (activeAccount != null)
            {
                FillBankWindowData(window);

                int id = activeAccount.Id;
                window.IsDepoOrRegural.ItemsSource = CreateVariantsOfAcc(id);
                if(window.IsDepoOrRegural.SelectedIndex == -1) { window.IsDepoOrRegural.SelectedIndex = 0; }

                TargetsDb = new List<Account>();
                foreach (Account acc in BankAccountsDb)
                {
                    if (acc != window.ActiveAccount.SelectedItem) { TargetsDb.Add(acc); }
                }

                CreateComboBoxSource<Account>(window.TargetAccount, "FullName", 0, TargetsDb);

                BlockButtons(window);
            }

            if (targetAcc != null)
            {
                int targetId = targetAcc.Id;
                window.TargetIsDepoOrRegural.ItemsSource = CreateVariantsOfAcc(targetId);
                if (window.TargetIsDepoOrRegural.SelectedIndex == -1) { window.TargetIsDepoOrRegural.SelectedIndex = 0; }
            }

        }

        private List<Account> CreateVariantsOfAcc(int id)
        {
            ReguralBankAccount regularAccount = null;
            DepositBankAccount depoAccount = null;

            foreach(ReguralBankAccount regAcc in BankAccountsDb)
            {
                if(regAcc.Id == id) { regularAccount = regAcc; }
            }
            foreach (DepositBankAccount depAcc in DepoBankAccountsDb)
            {
                if (depAcc.Id == id) { depoAccount = depAcc; }
            }

            List<Account> variantsOfAcc = new List<Account>()
                {
                    regularAccount,
                    depoAccount
                };
            return variantsOfAcc;

        }

        public void BlockButtons(BankAccountWindow window)
        {

            Account activeAccount = (Account)window.IsDepoOrRegural.SelectedItem;
            if (activeAccount != null)
            {
                window.CloseAccountButton.IsEnabled = activeAccount.IsAccountOpen;
                if (window.CloseAccountButton.IsEnabled == false)
                {
                    window.OpenAccountButton.IsEnabled = true;
                }
                else
                {
                    window.OpenAccountButton.IsEnabled = false;
                }
                window.AddMoney.IsEnabled = activeAccount.IsAccountOpen;
            }

        }
        public void OpenCloseAccount(BankAccountWindow window)
        {
            Account account = (Account)window.IsDepoOrRegural.SelectedItem;

            if (account.isAccountOpen == false)
            {
                account.isAccountOpen = true;
            }
            else
            {
                account.isAccountOpen = false;
            }

            SerializeClientsToJson<ReguralBankAccount>(BankAccountsDb, dbBankAccountsName);
            SerializeClientsToJson<DepositBankAccount>(DepoBankAccountsDb, dbDepoBankAccountsName);
            FillBankWindowData(window);
        }

        private void ExchangeMoney(BankAccountWindow window, Account activeAcc, Account targetAcc, string moneySource)
        {
            MoneyExchanger exchanger = new MoneyExchanger();
            if (int.TryParse(moneySource, out int number))
            {
                exchanger.Notify += KeepAnEventLog;
                exchanger.ExchageMoney(activeAcc, targetAcc, number);
                
                FillBankWindowData(window);
                SerializeClientsToJson<ReguralBankAccount>(BankAccountsDb, dbBankAccountsName);
                SerializeClientsToJson<DepositBankAccount>(DepoBankAccountsDb, dbDepoBankAccountsName);
            }
            else
            {
                MessageBox.Show("Введите целое число");
            }
        }



        public void AddMoney(BankAccountWindow window)
        {
            Account account = (Account)window.IsDepoOrRegural.SelectedItem;
            
            ExchangeMoney(window, account, account, window.MoneyValue.Text);
        }

        public void SendMoney(BankAccountWindow window)
        {
            Account account = (Account)window.IsDepoOrRegural.SelectedItem;
            Account targetAccount = (Account)window.TargetIsDepoOrRegural.SelectedItem;
            ExchangeMoney(window, account, targetAccount, window.TargetMoneyValue.Text);
        }

        public void CreateComboBoxSource<T>(ComboBox comboBox, string displayMemberPath, int selectedIndex, List<T> items)
        {
            if (selectedIndex == -1)
            {
                selectedIndex = 0;
            }

            comboBox.ItemsSource = items;
            comboBox.SelectedIndex = selectedIndex;
            comboBox.DisplayMemberPath = displayMemberPath;
        }

        public void CreateBankAccount<T>(ObservableCollection<T> accountDb, T newAccount, string dbName)
        {
            accountDb.Add(newAccount);
            
            SerializeClientsToJson(accountDb, dbName);
        }



        public void NewWindowAsDialog(Window mainWindow, Window newDialog)
        {
            newDialog.Owner = mainWindow;
            newDialog.ShowDialog();
        }

        private void KeepAnEventLog(string messege)
        {
            using (StreamWriter writer = new StreamWriter(dbEventLog, true))
            {
                writer.WriteLine(messege);
            }

        }
        private ObservableCollection<T> DeserializeClientsFromJson<T>(string dbAdress)
        {
            string json = File.ReadAllText(dbAdress);
            ObservableCollection<T> listOfClients = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
            return listOfClients;
        }

        private void SerializeClientsToJson<T>(ObservableCollection<T> clients, string dbAdress)
        {
            string json = JsonConvert.SerializeObject(clients);
            File.WriteAllText(dbAdress, json);
        }

        /// <summary>
        /// Проверяет доступ пользователя и правит возможность использовать кнопку добавления клиентов/видимость паспортных данных
        /// </summary>
        public void CheckCurrentUser(IEmployee worker, System.Windows.Controls.Button button, System.Windows.Controls.GridViewColumn passportColumn)
        {
            if (worker.LevelOfAccess == "0")
            {
                button.Visibility = Visibility.Hidden;
                passportColumn.DisplayMemberBinding = new Binding("HiddenPassportNumber");
            }
            else
            {
                button.Visibility = Visibility.Visible;
                passportColumn.DisplayMemberBinding = new Binding("PassportNumber");
            }
        }


        /// <summary>
        /// Проверяет параметры на пустоту и не пустые вставляет в клиента, возвращает новое значение имени изменения
        /// </summary>
        private string ReplacePartOfClientsDb(string nameOfChanged, Client dbClient, Dictionary<string, string> dbKeys)
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

            return nameOfChanged;
        }

        /// <summary>
        /// Ищет клиента по айди или возвращает null
        /// </summary>
        private Client FindClient(ObservableCollection<Client> ClientsDb, int id)
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

        /// <summary>
        /// Редактирует только номер телефона
        /// </summary>
        private void ChangePhoneNumber(ChangePhone newDialog, Client client, IEmployee worker)
        {
            string phoneNumber = newDialog.PhoneNumber.Text;
            string nameOfChanged = "";
            Client dbClient = FindClient(ClientsDb, client.Id);
            
            if (phoneNumber != "")
            {
                dbClient.PhoneNumber = phoneNumber;
                nameOfChanged += "PhoneNumber";
                dbClient.AutorOfChanges = worker.WorkerName;
                dbClient.ChangesDate = DateTime.Now;
                dbClient.NameOfChangedData = nameOfChanged;
                dbClient.TypeOfChanges = "Изменение параметров";
            }

            SerializeClientsToJson(ClientsDb, dbName);
            ClientsDb = DeserializeClientsFromJson<Client>(dbName);
        }

        /// <summary>
        /// Редактирует всю строку данных из ListView
        /// </summary>
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
            Client dbClient = FindClient(ClientsDb, client.Id);
            nameOfChanged = ReplacePartOfClientsDb(nameOfChanged, dbClient, paraNamesParaValues);
            if (nameOfChanged != "")
            {
                dbClient.AutorOfChanges = worker.WorkerName;
                dbClient.ChangesDate = DateTime.Now;
                dbClient.NameOfChangedData = nameOfChanged;
                dbClient.TypeOfChanges = "Изменение параметров";
            }

            SerializeClientsToJson(ClientsDb, dbName);
            ClientsDb = DeserializeClientsFromJson<Client>(dbName);
        }

        public void EditClient(MainWindow mainWindow, Client client, IEmployee worker)
        {
            if (client != null) 
            {
                if (worker.LevelOfAccess == "0")
                {
                    ChangePhone newDialog = new ChangePhone();
                    NewWindowAsDialog(mainWindow, newDialog);
                    ChangePhoneNumber(newDialog, client, worker);
                }
                else
                {
                    EditClient newDialog = new EditClient();
                    NewWindowAsDialog(mainWindow, newDialog);
                    ChangeClientDetails(newDialog, client, worker);
                }
            }
            mainWindow.lvClients.ItemsSource = ClientsDb;
            mainWindow.lvClients.Items.Refresh();
        }

        public void CreateClient(MainWindow mainWindow, IEmployee worker)
        {
            EditClient newDialog = new EditClient();
            NewWindowAsDialog(mainWindow, newDialog);

            if (AreAllFieldsFilled(newDialog) && newDialog.editable)
            {
                Client newClient = CreateNewClient(newDialog, worker);

                ClientsDb.Add(newClient);

                string fullName = GetFullName(newClient);
                AccountFactory accountFactory = new AccountFactory();
                accountFactory.Notify += KeepAnEventLog;


                List<Account> accList = accountFactory.CreateAccounts(newClient.Id, fullName);
                DepositBankAccount newDepositBankAccount = accList[0] as DepositBankAccount;
                ReguralBankAccount newReguralBankAccount = accList[1] as ReguralBankAccount;


                CreateBankAccount<ReguralBankAccount>(BankAccountsDb, newReguralBankAccount, dbBankAccountsName);
                CreateBankAccount<DepositBankAccount>(DepoBankAccountsDb, newDepositBankAccount, dbDepoBankAccountsName);

                SerializeClientsToJson(ClientsDb, dbName);
                ClientsDb = DeserializeClientsFromJson<Client>(dbName);

                mainWindow.lvClients.Items.Refresh();
            }
            else
            {
                ShowErrorMessage();
            }
        }

        private bool AreAllFieldsFilled(EditClient newDialog)
        {
            string firstName = newDialog.FirstName.Text;
            string secondName = newDialog.SecondName.Text;
            string surName = newDialog.SurName.Text;
            string phoneNumber = newDialog.PhoneNumber.Text;
            string passportNumber = newDialog.PassportNumber.Text;

            return !((firstName ?? secondName ?? surName ?? phoneNumber ?? passportNumber) == "");
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

        private void ShowErrorMessage()
        {
            MessageBox.Show("Должны быть заполнены все поля");
        }

        private void Sort(string sortBy, ListSortDirection direction, ListView lvClients)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(lvClients.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
        public void SortListView(RoutedEventArgs e, GridViewColumnHeader _lastHeaderClicked, ListSortDirection _lastDirection, ListView lvClients, MainWindow mainWindow)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;
            ResourceDictionary resources = new ResourceDictionary();
            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction, lvClients);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }


                    mainWindow._lastHeaderClicked = headerClicked;
                    mainWindow._lastDirection = direction;

                }
            }
        }
        internal static Repository CreateRepository()
        {
            return new Repository();
        }

    }


}
