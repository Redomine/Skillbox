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
using Task_1.Classes.Task_1_3;
using Task_1.Model;

namespace Task_1.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        public bool editable = false;
        public EditClient()//Repository mainData)
        {
            InitializeComponent();
            //data = mainData;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            editable = true;
            Close();
        }
    }
}
