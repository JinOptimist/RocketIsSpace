using System;
using System.Collections.Generic;

namespace SpaceWeb.Models.Chart
{
    public class OrderChartViewModel
    {
        public List<int> Labels { get; set; }

        public List<OrderDatasetViewModel> Datasets { get; set; } = new List<OrderDatasetViewModel>();
    }
}