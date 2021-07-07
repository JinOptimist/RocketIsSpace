using MazeCore;
using MazeCore.Cells;
using MazeCore.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze.Drawer
{
    public class MazeDrawer
    {
        public const string WallSymbol = "#";
        public const string GroundSymbol = "#";

        private List<BaseCell> prevCells;

        public void Draw(MazeLevel mazeLevel)
        {
            if (prevCells == null)
            {
                FirstDraw(mazeLevel);
            }
            else
            {
                ReDraw(mazeLevel);
            }

            prevCells = mazeLevel.Cells.ToList();
        }

        private void ReDraw(MazeLevel mazeLevel)
        {

            foreach (var cell in mazeLevel.Cells)
            {
                var prevCell = prevCells.Single(x => x.IsSameCoordinate(cell));
                if (prevCell.GetType() != cell.GetType())
                {
                    Console.SetCursorPosition(cell.X, cell.Y);

                    DrawCell(cell);
                }
            }
        }

        private void FirstDraw(MazeLevel mazeLevel)
        {
            Console.Clear();
            for (int y = 0; y < mazeLevel.Height; y++)
            {
                for (int x = 0; x < mazeLevel.Width; x++)
                {
                    var cell = mazeLevel.Cells.Single(cell => cell.X == x
                        && cell.Y == y);

                    DrawCell(cell);
                }

                Console.WriteLine();
            }
        }
   
        private void DrawCell(BaseCell cell)
        {
            if (cell is Wall)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(WallSymbol);
            }
            if (cell is Ground)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write(GroundSymbol);
            }
        }
    }
}
