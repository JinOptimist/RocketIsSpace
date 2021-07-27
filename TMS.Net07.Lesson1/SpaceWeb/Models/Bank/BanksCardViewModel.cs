using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class BanksCardViewModel
    {
        public long Id { get; set; }
        public EnumBankCard Card { get; set; }
        public string CardUrl { get; set; }
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        //public virtual List<BankAccount> BankAccount { get; set; }
        public virtual List<BanksCard> BanksCard { get; set; }
        public CurrencyExchengerViewModel CurrencyExchengerViewModel { get; set; } = new CurrencyExchengerViewModel();
    }
}
