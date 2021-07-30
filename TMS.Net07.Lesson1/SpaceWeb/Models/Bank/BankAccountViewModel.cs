using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class BankAccountViewModel
    {
        public long Id { get; set; }
        public string AccountNumber { get; set; }
        public Currency Currency { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public User Owner { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int AccountIndex { get; set; }
        public BankAccountType BankAccountType { get; set; }
        public string AmountString { get; set; }
        public List<BankAccountViewModel> UserAccounts { get; set; }
        public bool IsFrozen { get; set; }
    }
}
