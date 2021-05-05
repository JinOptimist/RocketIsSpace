using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.Bank
{
    public class InsuranceViewModel
    {
        public InsuranceNameType InsuranceNameType { get; set; }
        public InsurancePeriod InsurancePeriod { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public User OwnerId { get; set; }
    }
}
