using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class Transaction : BaseModel
    {
        public long FromAccountId { get; set; }
        public long ToAccountId { get; set; }
        public string TransferAmount { get; set; }

        public virtual BanksCard BanksCard { get; set; }


    }
}
