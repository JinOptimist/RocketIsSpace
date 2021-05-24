using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.Bank
{
    public class InsuranceTypeViewModel
    {
        public long Id { get; set; }
        public InsuranceNameType InsuranceNameType { get; set; }
        public InsurancePeriod InsurancePeriod { get; set; }
        public decimal Cost { get; set; }
    }
}
