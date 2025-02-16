using System.Drawing;
using System.Reflection.PortableExecutable;

namespace Maze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ui = new MazeUserInterface();
            var builder = new MazeBuilder();
            var ai = new MazeAI();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;

                int xSize = ui.InputXSize();
                int ySize = ui.InputYSize();
                int speed = ui.InputSpeed();

                Console.Clear();

                var maze = builder.GenerateMaze(ySize, xSize);
                ai.SolveMaze(maze);
                ui.DisplayMazeAndAI(maze, ai, speed);
            }
        }
    }
}