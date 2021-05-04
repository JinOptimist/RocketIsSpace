using SpaceWeb.Models.Bank.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class Insurance : BaseModel
    {
        public InsuranceName InsuranceName { get; set; }
        public InsurancePeriod InsurancePeriod { get; set; }
        public virtual InsuranceType InsuranceType { get; set; }
        public virtual User OwnerId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
