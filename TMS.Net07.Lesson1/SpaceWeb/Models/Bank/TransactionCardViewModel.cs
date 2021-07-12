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
        public DateTime CreationDate { get; set; }
        public long CardFromId { get; set; }
        public long CardToId { get; set; }
        public virtual List<BanksCard> BanksCard { get; set; }
        public virtual User Owner { get; set; }

        public decimal TransferAmount { get; set; }
    }
}
