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
        internal class Consultant : IEmployee
        {
            public Consultant()
            {
                levelOfAcces = "0";
            }
            private readonly string levelOfAcces;
            private readonly string workerName = "Консультант";
            public string LevelOfAccess
            {
                get { return levelOfAcces; }
            }
            public string WorkerName
            {
                get { return workerName; }
            }

        }
        internal class Manager : IEmployee
        {
            public Manager()
            {
                levelOfAcces = "1";
            }
            private readonly string workerName = "Менеджер";
            private readonly string levelOfAcces;
            public string LevelOfAccess
            {
                get { return levelOfAcces; }
            }

            public string WorkerName
            {
                get { return workerName; }
            }

        }
    }
}
