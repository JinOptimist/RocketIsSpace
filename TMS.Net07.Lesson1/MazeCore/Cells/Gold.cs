using MazeCore.Maze;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    public class Gold : BaseCell
    {
        public int GoldCount { get; private set; }

        public Gold(int x, int y, MazeLevel mazeLevel, int goldCount)
            : base(x, y, mazeLevel)
        {
            GoldCount = goldCount;
        }

        public Gold() : this(10, 10, null, 50)
        {
            GoldCount = GoldCount * 2;
        }
    }
}
