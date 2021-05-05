using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class InsuranceType : BaseModel
    {
        public InsuranceNameType InsuranceNameType { get; set; }

        public decimal Cost { get; set; }

        public InsurancePeriod InsurancePeriod { get; set; }
    }
}
