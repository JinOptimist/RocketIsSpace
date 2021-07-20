using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.EfStuff.Model.Enum;

namespace SpaceWeb.Models.Maze
{
    public class MazeViewModel
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public CellType[,] Cells { get; set; }
    }
}
