﻿using MazeCore.Cells;
using MazeCore.GraphStuff;
using MazeCore.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MazeCore
{
    public class MazeBuilder
    {
        private MazeLevel _mazeLevel;
        private Random _random;

        public Graph BuildGraph(MazeLevel maze)
        {
            _mazeLevel = maze;

            var graph = new Graph();
            var notWall = maze.Cells.NotWall();
            foreach (var cell in notWall)
            {
                var vertex = new Vertex();
                vertex.BaseCell = cell;
                graph.Vertices.Add(vertex);
            }

            foreach (var cell in notWall)
            {
                var currentVertext = graph.Vertices.Single(x => x.BaseCell == cell);
                var nears = GetNearCells(cell).NotWall();
                foreach (var near in nears)
                {
                    var neighbor = graph.Vertices.Single(x => x.BaseCell == near);
                    currentVertext.Neighbors.Add(neighbor);
                }
            }

            return graph;
        }

        public MazeLevel Build(int width = 20, int height = 10, int? seed = null)
        {
            seed = seed ?? DateTime.Now.Millisecond;
            _random = new Random(seed.Value);
            _mazeLevel = new MazeLevel(width, height, seed.Value);

            GenerateWalls();

            GenerateGrounds();

            GenerateGold();

            return _mazeLevel;
        }

        private void GenerateGold(int countGoldHeap = 5, int goldCountMax = 10)
        {
            var grounds = _mazeLevel.Cells.OfType<Ground>().ToList();

            for (int i = 0; i < countGoldHeap; i++)
            {
                var goldCount = _random.Next(1, goldCountMax);
                var ground = GetRandom(grounds);
                var gold = new Gold(ground.X, ground.Y, _mazeLevel, goldCount);
                _mazeLevel.ReplaceCell(gold);
                grounds.Remove(ground);
            }
        }

        private void GenerateWalls()
        {
            for (int y = 0; y < _mazeLevel.Height; y++)
            {
                for (int x = 0; x < _mazeLevel.Width; x++)
                {
                    _mazeLevel.Cells.Add(new Wall(x, y, _mazeLevel));
                }
            }
        }

        private void GenerateGrounds()
        {
            var minerX = 2;
            var minerY = 2;
            var couldBreak = new List<BaseCell>();
            couldBreak.Add(_mazeLevel[minerX, minerY]);

            //var drawer = new MazeDrawer();
            while (couldBreak.Any())
            {
                //drawer.Draw(_mazeLevel);
                //Thread.Sleep(100);

                var wallToDemolish = GetRandom(couldBreak);
                couldBreak.Remove(wallToDemolish);
                minerX = wallToDemolish.X;
                minerY = wallToDemolish.Y;

                var ground = new Ground(minerX, minerY, _mazeLevel);
                _mazeLevel.ReplaceCell(ground);
                var nearestCells = GetNearCells<Wall>(ground);
                couldBreak.AddRange(nearestCells);
                couldBreak = couldBreak
                    .Where(x => GetNearCells<Ground>(x).Count() < 2)
                    .ToList();
            }
        }

        private IEnumerable<BaseCell> GetNearCells(BaseCell cell)
        {
            return GetNearCells<BaseCell>(cell);
        }

        private IEnumerable<BaseCell> GetNearCells<CellType>(BaseCell cell)
            where CellType : BaseCell
        {
            return _mazeLevel.Cells
                .OfType<CellType>()
                .Where(c =>
                   Math.Abs(c.X - cell.X) == 0 && Math.Abs(c.Y - cell.Y) == 1
                || Math.Abs(c.X - cell.X) == 1 && Math.Abs(c.Y - cell.Y) == 0
            );
        }

        private T GetRandom<T>(List<T> list)
        {
            var index = _random.Next(list.Count);
            return list[index];
        }
    }
}
