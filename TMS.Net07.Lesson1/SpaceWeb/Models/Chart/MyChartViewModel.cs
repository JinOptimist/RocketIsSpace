using System.Collections.Generic;

namespace SpaceWeb.Models.Chart
{
    public class MyChartViewModel<T>
    {
        public List<string> Labels { get; set; }
        public List<MyDatasetViewModel<T>> Datasets { get; set; } = new List<MyDatasetViewModel<T>>();
    }
}