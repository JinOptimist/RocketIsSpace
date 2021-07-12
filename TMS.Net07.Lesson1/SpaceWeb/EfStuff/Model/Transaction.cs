using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class Transaction : BaseModel
    {
        public virtual BanksCard BanksCardFrom { get; set; }
        public virtual BanksCard BanksCardTo { get; set; }
        public decimal TransferAmount { get; set; }
        public DateTime Time { get; set; }
        public virtual User Owner { get; set; }


    }
}
