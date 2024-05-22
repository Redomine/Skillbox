using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.Interfaces
{
    internal interface IBankAccount<in T>
    {
        void OpenCloseAccount(T bankAccount);
        //void CreateBankAccount(T bankAccount);
    }
}

//interface IMessenger<in T, out K>
//{
//    void SendMessage(T message);
//    K WriteMessage(string text);
//}