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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Reverse(object sender, RoutedEventArgs e)
        {

            string reverse_source = ReverseString(userInputToReverse.Text);
            reverseText.Content = reverse_source;
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> split_source = userInputToSplit.Text.Split(' ').ToList();
            splitList.ItemsSource = split_source;

        }

        private static string ReverseString(string userInput)
        {
            string[] words = userInput.Split(' ');
            string result = string.Empty;
            for (int i = words.Length - 1; i >= 0; i--)
            {
                result+=words[i] + " ";
            }

            return result;
        }

    }
}
