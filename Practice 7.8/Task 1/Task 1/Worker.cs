using System;

namespace Task_1
{

    struct Worker
    {
        private static readonly Repository repository = new Repository();

        public Worker(int Id, string Name, int Age, int Height, DateTime DateOfBirth, string PlaceOfBirth)
        {
            this.Id = Id;
            this.AddTime = DateTime.Now;
            this.Name = Name;
            this.Height = Height;
            this.Age = Age;
            this.PlaceOfBirth = PlaceOfBirth;
            this.DateOfBirth = DateOfBirth;
        }

        public Worker(string Name, int Age, int Height, DateTime DateOfBirth, string PlaceOfBirth) :
            this(0, Name, Age, Height, DateOfBirth, PlaceOfBirth)
        {

        }

        public Worker(string Name):
            this(0, Name, 0, 0, new DateTime(1900, 01, 01), "г. Москва")
        { 
        }

        public int Id { get; set; }
        public DateTime AddTime { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }

    }
}