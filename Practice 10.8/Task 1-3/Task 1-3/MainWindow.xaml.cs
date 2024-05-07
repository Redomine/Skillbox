using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Task_1_3.Classes;
using Task_1_3.Windows;


namespace Task_1_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Repository repository = new Repository();
        readonly Consultant consultant = new Consultant();
        readonly Manager manager = new Manager();
        ObservableCollection<String> dataClients = new ObservableCollection<String>(); //Данные для обработки
        ObservableCollection<String> editedToViewConsultantClients = new ObservableCollection<String>(); //Данные для демонстрации в гуи консультанта
        ObservableCollection<String> editedToViewManagerClients = new ObservableCollection<String>(); //Данные для демонстрации в гуи менеджера

        public MainWindow()
        {
            InitializeComponent();
            FillLists();
            
        }

        private void FillLists()
        {
            dataClients = repository.GetClientData();
            editedToViewConsultantClients = repository.EditDataByAccessLevel(consultant, repository.GetClientData());
            editedToViewManagerClients = repository.EditDataByAccessLevel(manager, repository.GetClientData());
            ConsultantListBox.ItemsSource = editedToViewConsultantClients;
            ManagerListBox.ItemsSource = editedToViewManagerClients;
        }

        private void NewWindowAsDialog(Window newDialog)
        {
            //ChangePhone myOwnedDialog = new ChangePhone();
            newDialog.Owner = this;
            newDialog.ShowDialog();
        }

        private void ConsultantListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //Почему здесь крашит если убрать проверку на нулл? Началось после добавление обсервбл коллекшен. Почему-то эта ерунда вызывается дважды.
            if(ConsultantListBox.SelectedItem != null)
            {
                ChangePhone newConsultantDialog = new ChangePhone();
                NewWindowAsDialog(newConsultantDialog);

                if (newConsultantDialog.changingNumber)
                {
                    string curItem = ConsultantListBox.SelectedItem.ToString();
                    repository.ChangeElementOfListBox(curItem, dataClients, editedToViewConsultantClients, "PhoneNumber", newConsultantDialog.newNumber, consultant, "PhoneNumber");
                    FillLists();
                }
            }

        }

        private void ManagerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ManagerListBox.SelectedItem != null)
            {
                EditClient newManagerDialog = new EditClient();
                NewWindowAsDialog(newManagerDialog);
                Dictionary<string, string> clientData = newManagerDialog.clientData;
                string curItem = ManagerListBox.SelectedItem.ToString();

                string fullNameOfChangedParams = "";    
                foreach (KeyValuePair<string, string> entry in clientData)
                {
                    if(clientData[entry.Key] != "")
                    {
                        fullNameOfChangedParams += entry.Key + " ";
                        string newValue = clientData[entry.Key];
                        curItem = repository.ChangeElementOfListBox(curItem, dataClients, editedToViewManagerClients, entry.Key, newValue, manager, fullNameOfChangedParams);

                        FillLists();
                    }

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditClient newManagerDialog = new EditClient();
            NewWindowAsDialog(newManagerDialog);
            Dictionary<string, string> clientData = newManagerDialog.clientData;
            repository.CreateNewClient(clientData, manager);
            FillLists();
        }
    }
}
