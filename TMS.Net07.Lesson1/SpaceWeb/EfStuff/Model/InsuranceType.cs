using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class InsuranceType : BaseModel
    {
        public string Name { get; set; }

        public int CostPerMonth { get; set; }

        public int Period { get; set; }
    }
}
