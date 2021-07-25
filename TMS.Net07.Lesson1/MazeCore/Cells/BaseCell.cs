namespace MazeCore.Maze
{
    public abstract class BaseCell
    {
        protected BaseCell(int x, int y, MazeLevel mazeLevel)
        {
            X = x;
            Y = y;
            MazeLevel = mazeLevel;
        }

        public MazeLevel MazeLevel { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public bool IsSameCoordinate(BaseCell cell)
        {
            return cell.X == X && cell.Y == Y;
        }
    }
}
