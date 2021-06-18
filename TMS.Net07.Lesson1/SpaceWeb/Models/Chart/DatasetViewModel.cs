using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.Chart
{
    public class DatasetViewModel
    {
        public string Label { get; set; }
        public List<decimal> Data { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
    }
}
