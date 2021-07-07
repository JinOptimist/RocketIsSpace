using MazeCore.Cells;
using MazeCore.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MazeCore
{
    public class MazeBuilder
    {
        private MazeLevel _mazeLevel;
        private Random _random;

        public MazeLevel Build(int width = 20, int height = 10, int? seed = null)
        {
            seed = seed ?? DateTime.Now.Millisecond;
            _random = new Random(seed.Value);
            _mazeLevel = new MazeLevel(width, height, seed.Value);

            GenerateWalls();

            GenerateGrounds();

            return _mazeLevel;
        }

        private void GenerateWalls()
        {
            for (int y = 0; y < _mazeLevel.Height; y++)
            {
                for (int x = 0; x < _mazeLevel.Width; x++)
                {
                    _mazeLevel.Cells.Add(new Wall(x, y, _mazeLevel));
                }
            }
        }

        private void GenerateGrounds()
        {
            var minerX = 2;
            var minerY = 2;
            var couldBreak = new List<BaseCell>();
            couldBreak.Add(_mazeLevel[minerX, minerY]);

            //var drawer = new MazeDrawer();
            while (couldBreak.Any())
            {
                //drawer.Draw(_mazeLevel);
                //Thread.Sleep(100);

                var wallToDemolish = GetRandom(couldBreak);
                couldBreak.Remove(wallToDemolish);
                minerX = wallToDemolish.X;
                minerY = wallToDemolish.Y;

                var ground = new Ground(minerX, minerY, _mazeLevel);
                _mazeLevel.ReplaceCell(ground);
                var nearestCells = GetNearCells<Wall>(ground);
                couldBreak.AddRange(nearestCells);
                couldBreak = couldBreak
                    .Where(x => GetNearCells<Ground>(x).Count() < 2)
                    .ToList();
            }
        }

        private IEnumerable<BaseCell> GetNearCells(BaseCell cell)
        {
            return GetNearCells<BaseCell>(cell);
        }

        private IEnumerable<BaseCell> GetNearCells<CellType>(BaseCell cell)
            where CellType : BaseCell
        {
            return _mazeLevel.Cells
                .OfType<CellType>()
                .Where(c =>
                   Math.Abs(c.X - cell.X) == 0 && Math.Abs(c.Y - cell.Y) == 1
                || Math.Abs(c.X - cell.X) == 1 && Math.Abs(c.Y - cell.Y) == 0
            );
        }

        private T GetRandom<T>(List<T> list)
        {
            var index = _random.Next(list.Count);
            return list[index];
        }
    }
}
