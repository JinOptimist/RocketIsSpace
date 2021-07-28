using SpaceWeb.EfStuff.Model.Enum;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class BankAccountHistory : BankAccount
    {
        public DateTime DateOfChange { get; set; }
        public virtual User UserWhoChanged { get; set; }
        public string Action { get; set; }
    }
}
