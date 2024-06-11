using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1.Interfaces;

namespace Task_1.Classes
{
    public class Workers
    {

        public class Worker : IEmployee
        {

            internal string levelOfAcces;
            internal string workerName = "Консультант";
            public string LevelOfAccess
            {
                get { return levelOfAcces; }
            }
            public string WorkerName
            {
                get { return workerName; }
            }
        }

        public class Consultant : Worker
        {
            public Consultant()
            {
                this.levelOfAcces = "0";
                this.workerName = "Консультант";
            }
        }

        public class Manager : Worker
        {
            public Manager()
            {
                this.levelOfAcces = "1";
                this.workerName = "Менеджер";
            }
        }





    }
}
