using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task_1.Classes.Task_1_3;
using Task_1.Interfaces;
using Task_1.Model;
using Task_1.Windows;

namespace Task_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal Repository data;
        public GridViewColumnHeader _lastHeaderClicked = null;
        public ListSortDirection _lastDirection = ListSortDirection.Ascending;

        public MainWindow()
        {
            InitializeComponent();
            data = Repository.CreateRepository();
            
            data.CreateComboBoxSource<IEmployee>(WorkerName, "WorkerName", 1, data.Workers);
            data.currentWorker = data.Workers[WorkerName.SelectedIndex];
            lvClients.ItemsSource = data.ClientsDb;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            data.CreateClient(this, (IEmployee)WorkerName.SelectedItem);

        }

        private void WorkerChanged(object sender, SelectionChangedEventArgs e)
        {
            data.CheckCurrentUser(data.Workers[WorkerName.SelectedIndex], AddClient, PassportColumn);
        }


        private void ClientSelected(object sender, SelectionChangedEventArgs e)
        {
            data.EditClient(this, (Client)lvClients.SelectedItem, (data.Workers[WorkerName.SelectedIndex]));
        }

        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            data.SortListView(e, _lastHeaderClicked, _lastDirection, lvClients, this);
        }

        private void OpenBankAccount(object sender, RoutedEventArgs e)
        {
            BankAccountWindow newDialog = new BankAccountWindow(data);
            data.NewWindowAsDialog(this, newDialog);
        }
    }
}
