using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models.Bank.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.Bank
{
    public class InsuranceViewModel
    {
        public long Id { get; set; }
        public InsuranceName InsuranceName { get; set; }
        public InsurancePeriod InsurancePeriod { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public User OwnerId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
