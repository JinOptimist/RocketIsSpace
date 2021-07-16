using MazeCore.Maze;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    public class Wall : BaseCell
    {
        public Wall(int x, int y, MazeLevel mazeLevel) 
            : base(x, y, mazeLevel)
        {
        }
    }
}
