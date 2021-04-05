using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class ComplexViewModel
    {
        public List<RelicViewModel> Relics { get; set; }

        public RelicViewModel NewRelict { get; set; }
    }
}
