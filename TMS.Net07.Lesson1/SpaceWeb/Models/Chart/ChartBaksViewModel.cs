using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.Chart
{
    public class ChartBaksViewModel
    {
        public List<string> Labels { get; set; }
        public List<DatasetBanksViewModel> Datasets { get; set; } = new List<DatasetBanksViewModel>();
    }
}
