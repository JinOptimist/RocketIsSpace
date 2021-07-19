using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class TransactionBank : BaseModel
    {
        public string TransactionNumber { get; set; }

        public DateTime CreationDate { get; set; }
        public virtual BanksCard BanksCardFrom { get; set; }
        public virtual BanksCard BanksCardTo { get; set; }
        public decimal TransferAmount { get; set; }


    }
}
