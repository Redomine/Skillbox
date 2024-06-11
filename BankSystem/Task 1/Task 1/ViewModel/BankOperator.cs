using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_1.Classes;
using Task_1.Interfaces;
using Task_1.Model;
using Task_1.Windows;


namespace Task_1.ViewModel
{
    internal class BankOperator
    {
        
        internal Repository repository;
        internal DbOperator dbOperator;
        public BankOperator(Repository data)
        {
            repository = data;
            dbOperator = new DbOperator();
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

            dbOperator.SerializeClientsToJson<ReguralBankAccount>(repository.BankAccountsDb, repository.dbBankAccountsName);
            dbOperator.SerializeClientsToJson<DepositBankAccount>(repository.DepoBankAccountsDb, repository.dbDepoBankAccountsName);
            repository.FillBankWindowData(window);
        }

        private void ExchangeMoney(BankAccountWindow window, Account activeAcc, Account targetAcc, string moneySource)
        {
            MoneyExchanger exchanger = new MoneyExchanger();
            if (int.TryParse(moneySource, out int number))
            {
                exchanger.Notify += repository.KeepAnEventLog;
                exchanger.ExchageMoney(activeAcc, targetAcc, number, repository.currentWorker);

                repository.FillBankWindowData(window);
                dbOperator.SerializeClientsToJson<ReguralBankAccount>(repository.BankAccountsDb, repository.dbBankAccountsName);
                dbOperator.SerializeClientsToJson<DepositBankAccount>(repository.DepoBankAccountsDb, repository.dbDepoBankAccountsName);
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
    }
}
