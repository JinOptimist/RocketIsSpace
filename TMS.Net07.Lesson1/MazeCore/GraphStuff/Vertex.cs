using MazeCore.Maze;
using System.Collections.Generic;

namespace MazeCore.GraphStuff
{
    public class Vertex
    {
        public List<Vertex> Neighbors { get; set; } = new List<Vertex>();

        public BaseCell BaseCell { get; set; }

        public int DistanceFromRoot { get; set; } = -1;

        public List<Vertex> PathFromRoot { get; set; } = new List<Vertex>();
    }
}