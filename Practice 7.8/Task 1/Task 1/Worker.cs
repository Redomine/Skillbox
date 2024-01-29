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

        public Worker(string Name)
        {
            this.Id = 0;
            this.AddTime = DateTime.Now;
            this.Name = Name;
            this.Height = 0;
            this.Age = 0;
            this.PlaceOfBirth = "г. Москва";
            this.DateOfBirth = new DateTime(1900, 01, 01);

        }

        public Worker(string Name, DateTime DateOfBirth)
        {
            this.Id = 0;
            this.AddTime = DateTime.Now;
            this.Name = Name;
            this.Height = 0;
            this.Age = 0;
            this.PlaceOfBirth = "г. Москва";
            this.DateOfBirth = DateOfBirth;

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