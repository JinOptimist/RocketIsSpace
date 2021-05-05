using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class BanksCard : BaseModel
    {
        public Currency Currency { get; set; }
        public EnumBankCard Card { get; set; }
        public string CardNumber { get; set; }
        public string PinCard { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public DateTime CreationDate { get; set; }
        
    }
}
