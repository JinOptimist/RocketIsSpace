using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class BankAccount : BaseModel
    {
        public string BankAccountId { get; set; }
        public string Currency { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }

        public virtual User Owner { get; set; }
    }
}
