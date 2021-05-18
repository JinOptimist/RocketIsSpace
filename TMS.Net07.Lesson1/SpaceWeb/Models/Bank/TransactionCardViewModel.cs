using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class TransactionCardViewModel
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public long BankAccountId { get; set; }
        public virtual List<BanksCard> BanksCard { get; set; }
    }
}
