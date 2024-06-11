using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankClassLibrary.Exceptions
{
    public class ClientException: Exception
    {
        public ClientException(string message) : base(message)
        {
            MessageBox.Show(message);
        }
    }
}
