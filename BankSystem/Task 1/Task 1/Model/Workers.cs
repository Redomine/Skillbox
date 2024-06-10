using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1.Interfaces;

namespace Task_1.Classes
{
    internal class Workers
    {

        internal class Worker : IEmployee
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

        internal class Consultant : Worker
        {
            public Consultant()
            {
                this.levelOfAcces = "0";
                this.workerName = "Консультант";
            }
        }

        internal class Manager : Worker
        {
            public Manager()
            {
                this.levelOfAcces = "1";
                this.workerName = "Менеджер";
            }
        }





    }
}
