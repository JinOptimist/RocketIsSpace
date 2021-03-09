using System.Linq;
using System;
using System.Collections.Generic;

namespace HumansResources.Humans.Clients
{
    class Client
    {
        public string NameOfOrganization { get; set; }
        public int Index { get; set; }
        public string Location { get; set; }
        public List<string> BankAccountNumber { get; set; }
        public int FoundationDate { get; set; }

        public Client(string name, int index, string location, int foundationdate)
        {
            NameOfOrganization = name;
            Index = index;
            Location = location;
            FoundationDate = foundationdate;
        }

        public Client() { }
        
        public static int GetAge(DateTime reference, DateTime foundationdate)
        {
            int age = reference.Year - foundationdate.Year;
            if (reference < foundationdate.AddYears(age)) age--;

            return age;
        }
        
        public string GetFullNameOfClient()
        {
            return string.Format("Info: {0} {1} {2}", NameOfOrganization, Index, FoundationDate);
        }
        
        class Program
        {
            static void Main(string[] args)
            {
                var BankAccountNumber = new List<string>();
                {                 

                };
            }
        }
    }
}