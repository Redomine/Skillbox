using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_3
{
    internal class Client
    {
        [JsonConstructor]
        public Client(
            string firstName, 
            string secondName, 
            string surname, 
            string phoneNumber, 
            string passportNumber, 
            string changesDate, 
            string nameOfChangedData,
            string typeOfChanges,
            string autorOfChanges) 
        {

            this.firstName = firstName;
            this.secondName = secondName;
            this.surname = surname;
            this.phoneNumber = phoneNumber;
            this.passportNumber = passportNumber;
            this.changesDate = changesDate;
            this.typeOfChanges = typeOfChanges;
            this.nameOfChangedData = nameOfChangedData;
            this.autorOfChanges = autorOfChanges;
        }
        
        public Client(string dataString)
        {
            List<string> parameters = dataString.Split(',').ToList();

            this.firstName = PrepareParam(parameters[0]);
            this.secondName = PrepareParam(parameters[1]);
            this.surname = PrepareParam(parameters[2]);
            this.phoneNumber = PrepareParam(parameters[3]);
            this.passportNumber = PrepareParam(parameters[4]);
            this.changesDate = PrepareParam(parameters[5]);
            this.nameOfChangedData = PrepareParam(parameters[6]);
            this.typeOfChanges = PrepareParam(parameters[7]);
            this.autorOfChanges = PrepareParam(parameters[8]);

        }

        public Client(
            string firstName,
            string secondName,
            string surname,
            string phoneNumber,
            string passportNumber)
        {

            this.firstName = firstName;
            this.secondName = secondName;
            this.surname = surname;
            this.phoneNumber = phoneNumber;
            this.passportNumber = passportNumber;

            this.changesDate = "None";
            this.typeOfChanges = "None";
            this.nameOfChangedData = "None";
            this.autorOfChanges = "None";
        }

        private string PrepareParam(string param)
        {
            return param.Split(':')[1];
        }

        private string firstName;
        private string secondName;
        private string surname;
        private string phoneNumber;
        private string passportNumber;
        private string changesDate;
        private string nameOfChangedData;
        private string typeOfChanges;
        private string autorOfChanges;


        public string FirstName { 
            get { return firstName; } 
            set { firstName = value; } 
        }
        public string SecondName { 
            get { return secondName;} 
            set { secondName = value; } 
        }
        public string Surname
        {
            get { return surname;}
            set { surname = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber;}
            set { phoneNumber = value; }
        }
        public string PassportNumber
        {
            get { return passportNumber;}
            set { passportNumber = value; }
        }
        public string ChangesDate 
        { 
            get { return changesDate;} 
            set {  changesDate = value; } 
        }

        public string NameOfChangedData
        {
            get { return nameOfChangedData;}
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
    }
}
