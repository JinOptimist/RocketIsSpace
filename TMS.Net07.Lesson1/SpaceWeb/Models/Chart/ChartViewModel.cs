using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.Chart
{
    public class ChartViewModel
    {
        public List<string> Labels { get; set; }

        public List<DatasetViewModel> Datasets { get; set; } = new List<DatasetViewModel>();
    }
}
