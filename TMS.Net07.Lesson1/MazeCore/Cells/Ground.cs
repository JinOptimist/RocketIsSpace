using MazeCore.Maze;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    public class Ground : BaseCell
    {
        public Ground(int x, int y, MazeLevel mazeLevel) 
            : base(x, y, mazeLevel)
        {
        }
    }
}
