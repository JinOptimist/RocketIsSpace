using MazeCore.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeCore
{
    public class MazeLevel
    {
        public List<BaseCell> Cells { get; set; } = new List<BaseCell>();

        public readonly int Width;

        public readonly int Height;

        public MazeLevel(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void ReplaceCell(BaseCell newCell)
        {
            var oldCell = Cells.Single(x => newCell.IsSameCoordinate(x));
            Cells.Remove(oldCell);
            Cells.Add(newCell);
        }

        public BaseCell this[int x, int y]
        {
            get
            {
                return Cells.Single(cell => cell.X == x && cell.Y == y);
            }
        }
    }
}
