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

namespace Task_1_3.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        public Dictionary<string, string> clientData = new Dictionary<string, string>()
            {
                { "FirstName", String.Empty},
                { "SecondName", String.Empty},
                { "SurName", String.Empty},
                { "PhoneNumber", String.Empty},
                { "PassportNumber", String.Empty},
            };

        
        public EditClient()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            clientData["FirstName"] = FirstName.Text;
            clientData["SecondName"] = SecondName.Text;
            clientData["SurName"] = SurName.Text;
            clientData["PhoneNumber"] = PhoneNumber.Text;
            clientData["PassportNumber"] = PassportNumber.Text;

            Close();
        }
    }
}
