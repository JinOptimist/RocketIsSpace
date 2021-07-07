using Maze.Drawer;
using MazeCore;
using System;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            var maze = new MazeBuilder().Build(seed: 50);

            new MazeDrawer().Draw(maze);


            Console.WriteLine("Hello World!");
        }
    }
}
