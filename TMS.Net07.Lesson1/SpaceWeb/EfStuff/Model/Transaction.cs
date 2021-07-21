using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class Transaction : BaseModel
    {
        public virtual BanksCard BanksCardFrom { get; set; }
        public virtual BanksCard BanksCardTo { get; set; }
        public decimal TransferAmount { get; set; }


    }
}
