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

namespace Task_1.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangePhone.xaml
    /// </summary>
    public partial class ChangePhone : Window
    {
        public bool changingNumber = false; //Меняем ли номер?
        public ChangePhone()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PhoneNumber.Text == "")
            {
                MessageBox.Show("Номер не может быть пустым");
            }
            else
            {
                changingNumber = true;
                Close();
            }
        }
    }
}
