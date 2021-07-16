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

        public void SetPathFromRoot(Vertex rootVertext)
        {
            SetPath(rootVertext);
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

        private void SetPath(Vertex vertext)
        {
            vertext.PathFromRoot.Add(vertext);
            foreach (var neighbor in vertext
                .Neighbors
                .Where(x => !x.PathFromRoot.Any()))
            {
                neighbor.PathFromRoot = vertext.PathFromRoot.ToList();
                SetPath(neighbor);
            }
        }
    }
}
