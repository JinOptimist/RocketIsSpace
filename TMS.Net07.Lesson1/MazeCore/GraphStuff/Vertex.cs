using MazeCore.Maze;
using System.Collections.Generic;

namespace MazeCore.GraphStuff
{
    public class Vertex
    {
        public List<Vertex> Neighbors { get; set; } = new List<Vertex>();

        public BaseCell BaseCell { get; set; }

        public int DistanceFromRoot { get; set; }

        public List<int> Ways { get; set; } = new List<int>();
    }
}