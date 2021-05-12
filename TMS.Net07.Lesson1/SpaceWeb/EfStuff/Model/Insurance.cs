using SpaceWeb.Models.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class Insurance : BaseModel
    {
        public virtual InsuranceType InsuranceType { get; set; }
        public DateTime DateCreationing { get; set; }
        public virtual User Owner { get; set; }
    }
}
