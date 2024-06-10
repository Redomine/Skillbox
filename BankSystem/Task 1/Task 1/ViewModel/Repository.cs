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
        public string dbName = "_dBClients.json";
        public string dbBankAccountsName = "_dBBankAccounts.json";
        public string dbDepoBankAccountsName = "_dBDepositBankAccounts.json";
        public string dbEventLog = "_dBEventLog.txt";

        public IEmployee currentWorker;
        internal DbOperator dbOperator;

        public ObservableCollection<Client> ClientsDb { get; set; }

        List<Account> VariantsOfAcc {  get; set; }

        List<Account> VariantsOfTargetAcc { get; set; }

        List<Account> TargetsDb { get; set; }

        public ObservableCollection<ReguralBankAccount> BankAccountsDb { get; set; }

        public ObservableCollection<DepositBankAccount> DepoBankAccountsDb { get; set; }

        public List<IEmployee> Workers { get; set; }

        public Repository()
        {
            dbOperator = new DbOperator();
            ClientsDb = dbOperator.DeserializeClientsFromJson<Client>(dbName);
            BankAccountsDb = dbOperator.DeserializeClientsFromJson<ReguralBankAccount>(dbBankAccountsName);
            DepoBankAccountsDb = dbOperator.DeserializeClientsFromJson<DepositBankAccount>(dbDepoBankAccountsName);
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
            if (activeAccount != null)
            {
                FillBankWindowData(window);

                int id = activeAccount.Id;
                window.IsDepoOrRegural.ItemsSource = CreateVariantsOfAcc(id);
                if (window.IsDepoOrRegural.SelectedIndex == -1) { window.IsDepoOrRegural.SelectedIndex = 0; }

                TargetsDb = new List<Account>();
                foreach (Account acc in BankAccountsDb)
                {
                    if (acc != window.ActiveAccount.SelectedItem) { TargetsDb.Add(acc); }
                }

                CreateComboBoxSource<Account>(window.TargetAccount, "FullName", 0, TargetsDb);

                BlockButtons(window);
            }

            if (window.TargetAccount.SelectedItem is Account targetAcc)
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

        public void NewWindowAsDialog(Window mainWindow, Window newDialog)
        {
            newDialog.Owner = mainWindow;
            newDialog.ShowDialog();
        }

        public void KeepAnEventLog(string messege)
        {
            using (StreamWriter writer = new StreamWriter(dbEventLog, true))
            {
                writer.WriteLine(messege);
            }

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
            currentWorker = worker;
        }

        /// <summary>
        /// Ищет клиента по айди или возвращает null
        /// </summary>




        internal static Repository CreateRepository()
        {
            return new Repository();
        }

    }


}
