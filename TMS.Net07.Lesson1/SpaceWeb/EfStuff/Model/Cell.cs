using SpaceWeb.EfStuff.Model.Enum;

namespace SpaceWeb.EfStuff.Model
{
    public class Cell : BaseModel
    {
        public int X { get; set; }

        public int Y { get; set; }

        public virtual MazeLevel MazeLevel { get; set; }

        public CellType CellType { get; set; }
    }
}
