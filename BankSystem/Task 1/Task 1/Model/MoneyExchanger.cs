using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_1.Classes;
using Task_1.Interfaces;

namespace Task_1.Model
{
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
            if (activeAccount != targetAccount)
            {
                activeAccount.TakeMoneyFrom(moneyValue);
            }
            targetAccount.PutMoneyInto(moneyValue);

            if (activeAccount == targetAccount)
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
}
