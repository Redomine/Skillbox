using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Task_1.Classes.Task_1_3;
using Task_1.Interfaces;
using static Task_1.Classes.Workers;

namespace Task_1.Classes
{
    

    public class Account
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

        public int id;
        public int moneyValue;
        public bool isAccountOpen;
        public string fullName;
        public string accName;
        public int Id { get { return id; } }
        public int MoneyValue { get { return moneyValue; } set { moneyValue = value; } }
        public bool IsAccountOpen { get { return isAccountOpen; } set { isAccountOpen = value; } }

        public string FullName { get { return fullName; } set { fullName = value; } }

        public string AccName { get { return accName; } set {  accName = value; } }

        public void PutMoneyInto(int moneyValue) {  this.MoneyValue += moneyValue; }

        public void TakeMoneyFrom(int moneyValue) { this.MoneyValue -= moneyValue; }


    }

    public class ReguralBankAccount : Account
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

    public class DepositBankAccount : Account
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


}
 