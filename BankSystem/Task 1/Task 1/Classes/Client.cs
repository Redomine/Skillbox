using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Task_1.Classes
{

    namespace Task_1_3
    {
        internal class Client: INotifyPropertyChanged
        {
            [JsonConstructor]
            public Client(
                int id,
                string firstName,
                string secondName,
                string surname,
                string phoneNumber,
                string passportNumber,
                DateTime changesDate,
                string nameOfChangedData,
                string typeOfChanges,
                string autorOfChanges)
            {
                this.Id = id;
                this.firstName = firstName;
                this.secondName = secondName;
                this.surname = surname;
                this.phoneNumber = phoneNumber;
                this.passportNumber = passportNumber;
                this.hiddenPassportNumber = "Нет доступа";
                this.changesDate = changesDate;
                this.typeOfChanges = typeOfChanges;
                this.nameOfChangedData = nameOfChangedData;
                this.autorOfChanges = autorOfChanges;
                AddClient();
            }


            public Client(
                string firstName,
                string secondName,
                string surname,
                string phoneNumber,
                string passportNumber)
            {
                this.Id = Client.clientCounter;
                this.firstName = firstName;
                this.secondName = secondName;
                this.surname = surname;
                this.phoneNumber = phoneNumber;
                this.passportNumber = passportNumber;
                this.hiddenPassportNumber = "Нет доступа";
                this.changesDate = DateTime.Now;
                AddClient();
            }

            private string firstName;
            private string secondName;
            private string surname;
            private string phoneNumber;
            private string passportNumber;
            private readonly string hiddenPassportNumber;
            private DateTime changesDate;
            private string nameOfChangedData;
            private string typeOfChanges;
            private string autorOfChanges;

            public int Id { get; set; }
            public string FirstName
            {
                get { return firstName; }
                set { firstName = value; }
            }
            public string SecondName
            {
                get { return secondName; }
                set { secondName = value; }
            }
            public string SurName
            {
                get { return surname; }
                set { surname = value; }
            }

            public string PhoneNumber
            {
                get { return phoneNumber; }
                set { phoneNumber = value; }
            }
            public string PassportNumber
            {
                get { return passportNumber; }
                set { passportNumber = value; }
            }

            public string HiddenPassportNumber
            {
                get { return hiddenPassportNumber; }
            }

            public DateTime ChangesDate
            {
                get { return changesDate; }
                set { changesDate = value; }
            }

            public string NameOfChangedData
            {
                get { return nameOfChangedData; }
                set { nameOfChangedData = value; }
            }
            public string TypeOfChanges
            {
                get { return typeOfChanges; }
                set { typeOfChanges = value; }
            }
            public string AutorOfChanges
            {
                get { return autorOfChanges; }
                set { autorOfChanges = value; }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }

            public static int clientCounter;

            public static int AddClient()
            {
                return ++clientCounter;
            }

        }
    }
}
