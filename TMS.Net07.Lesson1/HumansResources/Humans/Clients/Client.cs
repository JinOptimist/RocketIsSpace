using System.Linq;
using System;
using System.Collections.Generic;
using HumansResources.Humans.Persons;
using HumansResources.Humans.Orders;

namespace HumansResources.Humans.Clients
{
    public class Client
    {
        public string NameOfOrganization { get; set; }
        public int Index { get; set; }
        public string Location { get; set; }
        public List<string> BankAccountNumber { get; set; }
        public int FoundationDate { get; set; }
        public PostAddress PostAddress { get; set; }
        private readonly List<Order> listOrders = new List<Order>();


        public Client(string name, int index, string location, int foundationdate)
        {
            NameOfOrganization = name;
            Index = index;
            Location = location;
            FoundationDate = foundationdate;
        }

        public Client() { }

        public void SetOrder(Order order)
        {
            listOrders.Add(order);
        }
        public List<Order> GetOrders()
        {
            return listOrders;
        }

        
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
    }
}