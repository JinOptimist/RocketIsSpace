using System.Collections.Generic;
using System.Drawing;

namespace SpaceWeb.Models.Chart
{
    public class MyDatasetViewModel<T>
    {
        public MyDatasetViewModel()
        {
            backgroundColor = new List<Color>() { Color.Red, Color.Blue, Color.Cyan, Color.Green };
        }
        public string Label { get; set; }
        public List<T> Data { get; set; }
        public List<Color> backgroundColor { get; set; }
    }
}