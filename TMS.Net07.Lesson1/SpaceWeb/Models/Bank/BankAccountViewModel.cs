using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class BankAccountViewModel
    {
        public string BankAccountId { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public long OwnerId { get; set; }
    }
}
