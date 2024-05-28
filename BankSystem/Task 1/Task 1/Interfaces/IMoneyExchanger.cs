using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.Interfaces
{
    internal interface IMoneyExchanger<in T, out K>
    {

        K GetReport(T targetAccount);
        void ExchageMoney(T activeAccount, T targetAccount, int moneyValue);

    }
}
