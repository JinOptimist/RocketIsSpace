
using System;
namespace HumansResources.Humans.Clients
{
    class Client
    {
        public int DataTime { get; set; }
        public bool IsMale { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }

        public Client(string firstName, int age, string middlename, bool ismale, string lastName)
        {
            DataTime = age;
            Name = firstName;
            Surname = middlename;
            IsMale = ismale;
            Lastname = lastName;

        }


        public Client() { }

        public string GetFullName()
        {
            return string.Format("{0} {1} {2}", Name, Surname, Lastname);
        }

        public static int GetAge(DateTime reference, DateTime birthday)
        {
            int age = reference.Year - birthday.Year;
            if (reference < birthday.AddYears(age)) age--;

            return age;
        }

        class Program
        {
            static void Main(string[] args)
            {

            }
        }
    }
}