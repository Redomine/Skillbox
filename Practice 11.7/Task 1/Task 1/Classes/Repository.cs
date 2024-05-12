using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Task_1.Classes.Task_1_3;
using Task_1.Interfaces;
using Task_1.Windows;
using static Task_1.Classes.Workers;

namespace Task_1.Model
{
    internal class Repository
    {
        private readonly string dbName = "_dBClients.json";
        public ObservableCollection<Client> ClientsDb { get; set; }
        public List<IEmployee> Workers { get; set; }
        public Repository()
        {
            ClientsDb = DeserializeClientsFromJson();
            Consultant consultant = new Consultant();
            Manager manager = new Manager();


            Workers = new List<IEmployee>
            {
                manager,
                consultant
            };

        }




        public void NewWindowAsDialog(Window mainWindow, Window newDialog)
        {
            newDialog.Owner = mainWindow;
            newDialog.ShowDialog();
        }

        private ObservableCollection<Client> DeserializeClientsFromJson()
        {
            string json = File.ReadAllText(dbName);
            ObservableCollection<Client> listOfClients = JsonConvert.DeserializeObject<ObservableCollection<Client>>(json);
            return listOfClients;
        }

        private void SerializeClientsToJson(ObservableCollection<Client> clients)
        {
            string json = JsonConvert.SerializeObject(clients);
            File.WriteAllText(dbName, json);
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

        public void EditClient(MainWindow mainWindow, Client client, IEmployee worker)
        {
            string nameOfChanged = "";
            

            if (client != null) 
            {
                MessageBox.Show(client.Id.ToString());
                string phoneNumber;
                if (worker.LevelOfAccess == "0")
                {
                    ChangePhone newDialog = new ChangePhone();
                    NewWindowAsDialog(mainWindow, newDialog);
                    phoneNumber = newDialog.PhoneNumber.Text;

                    foreach (Client dbClient in ClientsDb)
                    {
                        if (dbClient.Id == client.Id)
                        {
                            {
                                dbClient.PhoneNumber = phoneNumber;
                                nameOfChanged += newDialog.PhoneNumber.Name + " ";
                                dbClient.AutorOfChanges = worker.WorkerName;
                                dbClient.ChangesDate = DateTime.Now;
                                dbClient.NameOfChangedData = nameOfChanged;
                                dbClient.TypeOfChanges = "Изменение параметров";
                            }
                        }
                    }
                    SerializeClientsToJson(ClientsDb);
                    ClientsDb = DeserializeClientsFromJson();

                }
                else
                {
                    EditClient newDialog = new EditClient();
                    NewWindowAsDialog(mainWindow, newDialog);
                    string firstName = newDialog.FirstName.Text;
                    string secondName = newDialog.SecondName.Text;
                    string surName = newDialog.SurName.Text;
                    phoneNumber = newDialog.PhoneNumber.Text;
                    string passportNumber = newDialog.PassportNumber.Text;
                    

                    foreach (Client dbClient in ClientsDb)
                    {
                        if (dbClient.Id == client.Id)
                        {
                            if (firstName != "")
                            {
                                
                                dbClient.FirstName = firstName;
                                nameOfChanged += newDialog.FirstName.Name + " ";
                            }
                            if (secondName != "")
                            {
                                dbClient.SecondName = secondName;
                                nameOfChanged += newDialog.SecondName.Name + " ";
                            }
                            if (surName != "")
                            {
                                dbClient.SurName = surName;
                                nameOfChanged += newDialog.SurName.Name + " ";
                            }
                            if (phoneNumber != "")
                            {
                                dbClient.PhoneNumber = phoneNumber;
                                nameOfChanged += newDialog.PhoneNumber.Name + " ";
                            }
                            if (passportNumber != "")
                            {
                                dbClient.PassportNumber = passportNumber;
                                nameOfChanged = newDialog.PassportNumber.Name + " ";
                            }
                            if (nameOfChanged != "")
                            {
                                dbClient.AutorOfChanges = worker.WorkerName;
                                dbClient.ChangesDate = DateTime.Now;
                                dbClient.NameOfChangedData = nameOfChanged;
                                dbClient.TypeOfChanges = "Изменение параметров";
                            }
                        }
                    }
                    SerializeClientsToJson(ClientsDb);
                    ClientsDb = DeserializeClientsFromJson();
                }


            }
            
            mainWindow.lvClients.ItemsSource = ClientsDb;
            mainWindow.lvClients.Items.Refresh();
        }

        public void CreateClient(MainWindow mainWindow, IEmployee worker)
        {
            EditClient newDialog = new EditClient();
            NewWindowAsDialog(mainWindow, newDialog);

            string firstName = newDialog.FirstName.Text;
            string secondName = newDialog.SecondName.Text;
            string surName = newDialog.SurName.Text;
            string phoneNumber = newDialog.PhoneNumber.Text;
            string passportNumber = newDialog.PassportNumber.Text;

            if ((firstName == "" || secondName == "" ||  surName == "" || phoneNumber == "" || passportNumber == "") & newDialog.editable)
            {
                MessageBox.Show("Должны быть заполнены все поля");
            }
            else
            {
                if (newDialog.editable)
                {
                    Client newClient = new Client(firstName, secondName, surName, phoneNumber, passportNumber)
                    {
                        TypeOfChanges = "Добавление строки",
                        AutorOfChanges = worker.WorkerName,
                        NameOfChangedData = "Вся строка"
                    };

                    ClientsDb.Add(newClient);
                    SerializeClientsToJson(ClientsDb);
                    ClientsDb = DeserializeClientsFromJson();
                    mainWindow.lvClients.Items.Refresh();
                }
            }
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
