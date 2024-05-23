using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Task_1.Classes;
using Task_1.Interfaces;
using Task_1.Model;

namespace Task_1.Windows
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class BankAccountWindow : Window
    {
        internal Repository data;
        internal BankAccountWindow(Repository mainData)
        {
            InitializeComponent();
            data = mainData;
            data.CreateComboBoxSource<ReguralBankAccount>(ActiveAccount, "FullName", 0, data.BankAccountsDb.ToList());
            data.FillBankWindowData(this);
            data.CreateBoxes(this);
            data.BlockButtons(this);
        }


        private void OpenAcc(object sender, RoutedEventArgs e)
        {
            data.OpenCloseAccount(this);
            data.RefreshBankWindow(this);
        }

        private void CloseAcc(object sender, RoutedEventArgs e)
        {
            data.OpenCloseAccount(this);
            data.RefreshBankWindow(this);
        }


        private void AddMoney_Click(object sender, RoutedEventArgs e)
        {
            data.AddMoney(this);
        }

        private void Send_Money_Button(object sender, RoutedEventArgs e)
        {
            data.SendMoney(this);
        }

        private void TargetAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            data.RefreshBankWindow(this);
        }

        private void ActiveAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            data.RefreshBankWindow(this);
        }
        private void IsDepoOrRegural_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            data.RefreshBankWindow(this);
        }
    }
}
