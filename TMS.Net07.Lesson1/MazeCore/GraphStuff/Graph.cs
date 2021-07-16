using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeCore.GraphStuff
{
    public class Graph
    {
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();

        public void SetDistanceFromRoot(Vertex rootVertext)
        {
            Vertices.ForEach(x => x.DistanceFromRoot = -1);
            SetDistance(rootVertext);
        }

        private void SetDistance(Vertex vertext)
        {
            foreach (var neighbor in vertext
                .Neighbors
                .Where(x => x.DistanceFromRoot < 0))
            {
                neighbor.DistanceFromRoot =
                    vertext.DistanceFromRoot + 1;
                SetDistance(neighbor);
            }
        }
    }
}
