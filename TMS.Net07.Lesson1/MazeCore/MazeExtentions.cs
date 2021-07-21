using MazeCore.Cells;
using MazeCore.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeCore
{
    public static class MazeExtentions
    {
        public static IEnumerable<BaseCell> NotCellType<T>(this IEnumerable<BaseCell> collection)
            where T : BaseCell
        {
            return collection.Where(x => !(x is T));
        }

        public static IEnumerable<BaseCell> NotWall(this IEnumerable<BaseCell> collection)
        {
            return collection.NotCellType<Wall>();
        }
    }
}
