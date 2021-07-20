using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Model
{
    public class MazeLevel : BaseModel
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public virtual List<Cell> Cells { get; set; }

        public virtual User User { get; set; }
    }
}
