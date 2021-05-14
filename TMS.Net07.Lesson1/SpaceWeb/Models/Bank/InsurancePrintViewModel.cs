using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.Bank
{
    public class InsurancePrintViewModel
    {
        public long Id { get; set; }
        public DateTime DateCreationing { get; set; }
        public InsuranceType InsuranceType { get; set; }
    }
}
