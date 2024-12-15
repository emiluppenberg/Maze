using System.Drawing;
using System.Reflection.PortableExecutable;
using Maze.DataAccess.Data;
using Maze.DataAccess.Models;

namespace Maze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new MazeDbContext();
            var mazeDataRepository = new MazeDataRepository(context);
            var ui = new MazeUserInterface(mazeDataRepository);
            var builder = new MazeBuilder();
            var ai = new MazeAI();

            while (true)
            {
                ui.Menu();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("Maze size X: ");
                int xSize = Convert.ToInt32(Console.ReadLine());

                Console.Write("Maze size Y: ");
                int ySize = Convert.ToInt32(Console.ReadLine());

                Console.Write("AI speed: ");
                int aiSpeed = Convert.ToInt32(Console.ReadLine());

                Console.Clear();

                var maze = builder.GenerateMaze(ySize, xSize);
                ai.SolveMaze(maze);
                ui.DisplayMazeAndAI(maze, ai, aiSpeed);

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nPress 1 - Save data\nPress any key - Continue");
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        ui.SaveData(ySize, xSize, ai.steps.Count);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void DisplayMaze(MazePoint[,] maze)
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                int charBuffer = 0;

                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j].Walls.Contains("Up"))
                    {
                        Console.SetCursorPosition(maze[i, j].X + charBuffer, maze[i, j].Y + i);
                        Console.Write("·-·");
                    }
                    if (maze[i, j].Walls.Contains("Right"))
                    {
                        Console.SetCursorPosition(maze[i, j].X + charBuffer + 2, maze[i, j].Y + i);
                        Console.Write("·");
                        Console.SetCursorPosition(maze[i, j].X + charBuffer + 2, maze[i, j].Y + 1 + i);
                        Console.Write("|");
                        Console.SetCursorPosition(maze[i, j].X + charBuffer + 2, maze[i, j].Y + 2 + i);
                        Console.Write("·");
                    }
                    if (maze[i, j].Walls.Contains("Down"))
                    {
                        Console.SetCursorPosition(maze[i, j].X + charBuffer, maze[i, j].Y + 2 + i);
                        Console.Write("·-·");
                    }
                    if (maze[i, j].Walls.Contains("Left"))
                    {
                        Console.SetCursorPosition(maze[i, j].X + charBuffer, maze[i, j].Y + i);
                        Console.Write("·");
                        Console.SetCursorPosition(maze[i, j].X + charBuffer, maze[i, j].Y + 1 + i);
                        Console.Write("|");
                        Console.SetCursorPosition(maze[i, j].X + charBuffer, maze[i, j].Y + 2 + i);
                        Console.Write("·");
                    }

                    charBuffer += 1;
                }
            }
        }
    }
}