using MazeCore.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeCore.GraphStuff
{
    public class Graph
    {
        public Graph()
        {
            _startWayNumber = 1;
        }

        public List<Vertex> Vertices { get; set; } = new List<Vertex>();

        private int _startWayNumber;


        public void SetDistanceFromRoot(Vertex rootVertex)
        {
            Vertices.ForEach(x => x.DistanceFromRoot = -1);
            rootVertex.DistanceFromRoot = 0;
            SetDistance(rootVertex);
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

        public int GetRichestWay(Vertex rootVertex)
        {
            var graphs = GetAllWays(rootVertex);
            return 
                graphs
                .Select(
                    x => x.Vertices
                    .Select(x => x.BaseCell as Gold)
                    .Sum(x => x?.GoldCount))
                .Max(x => x.Value);
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
            if (!Current.Neighbors.Any(x => x.DistanceFromRoot > Current.DistanceFromRoot))
            {
                _startWayNumber++;
            }
        }

        private void SetChildrensWay(Vertex current)
        {
            if (!current.Ways.Contains(_startWayNumber))
            {
                current.Ways.Add(_startWayNumber);
            }
            foreach (var smallNeighbor in current.Neighbors.Where(x => x.DistanceFromRoot < current.DistanceFromRoot))
            {
                SetChildrensWay(smallNeighbor);
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
