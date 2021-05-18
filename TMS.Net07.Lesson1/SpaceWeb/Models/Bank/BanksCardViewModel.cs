﻿using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
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
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual List<BankAccount> BankAccount { get; set; }
    }
}
