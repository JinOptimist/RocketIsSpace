using MazeCore.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeCore.GraphStuff
{
    public class Graph
    {
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();

        public int StartWayNumber { get; set; }


        public void SetDistanceFromRoot(Vertex rootVertex)
        {
            Vertices.ForEach(x => x.DistanceFromRoot = -1);
            rootVertex.DistanceFromRoot = 0;
            SetDistance(rootVertex);
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

        public Graph GetRichestWay(Vertex rootVertex)
        {
            var graphs = GetAllWays(rootVertex);
            //graphs.Single(x => x.Vertices.Select(x => x.BaseCell);
            return null;
        }

        public List<Graph> GetAllWays(Vertex rootVertex)
        {
            SetDistanceFromRoot(rootVertex);
            GetWays(rootVertex);
            var ways = rootVertex.Ways.Select(x => x).Distinct().ToList();
            List<Graph> result = new List<Graph>();
            foreach (var way in ways)
            {
                result.Add(new Graph() { Vertices = Vertices.Where(x => x.Ways.Contains(way)).ToList() });
            }
            return result;
        }

        private void GetWays(Vertex Current)
        {
            SetChildrensWay(Current);
            foreach (var neighbor in Current.Neighbors.Where(x => x.DistanceFromRoot > Current.DistanceFromRoot))
            {
                GetWays(neighbor);
            }
            if (Current.Neighbors.Where(x => x.DistanceFromRoot > Current.DistanceFromRoot).Count() == 0)
            {
                StartWayNumber++;
            }
        }

        private void SetChildrensWay(Vertex Current)
        {
            if (!Current.Ways.Contains(StartWayNumber))
            {
                Current.Ways.Add(StartWayNumber);
            }
            foreach (var smallNeighbor in Current.Neighbors.Where(x => x.DistanceFromRoot < Current.DistanceFromRoot))
            {
                SetChildrensWay(smallNeighbor);
            }
        }
    }
}
