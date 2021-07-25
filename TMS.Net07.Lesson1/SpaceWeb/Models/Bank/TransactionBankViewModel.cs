using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class TransactionBankViewModel
    {
        public DateTime CreationDate { get; set; }
        public long CardFromId { get; set; }
        public long CardToId { get; set; }
        public decimal TransferAmount { get; set; }

    }
}
