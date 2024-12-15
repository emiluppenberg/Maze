using System.Drawing;
using Maze.DataAccess.Data;
using Maze.DataAccess.Models;

namespace Maze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new MazeDbContext();
            var builder = new MazeBuilder();
            var ai = new MazeAI();

            while (true)
            {
                bool loop = true;

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
                DisplayMazeAndAI(maze, ai, aiSpeed);

                while (loop)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(
                        "\nPress 1 - Save data\n" +
                        "Press 2 - View data\n" +
                        "Any other key - New maze\n");

                    switch (Console.ReadKey(true).KeyChar)
                    {
                        case '1':
                            Console.Write("AI speed: ");
                            aiSpeed = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            DisplayMazeAndAI(maze, ai, aiSpeed);
                            break;
                        case '0':
                            Environment.Exit(0);
                            break;
                        default:
                            Console.Clear();
                            loop = false;
                            break;
                    }
                }
            }
        }

        private static void DisplayMazeAndAI(MazePoint[,] maze, MazeAI ai, int speed)
        {
            int previousStepY = 0;
            int previousStepX = 0;

            for (int s = 0; s < ai.steps.Count; s++)
            {
                Console.CursorVisible = false;
                if (s > 0)
                {
                    Console.SetCursorPosition(previousStepX, previousStepY);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("*");
                }
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    int charBuffer = 0;

                    for (int j = 0; j < maze.GetLength(1); j++)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
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
                        if (maze[i, j] == ai.steps[s])
                        {
                            Console.SetCursorPosition(ai.steps[s].X + charBuffer + 1, ai.steps[s].Y + 1 + i);
                            previousStepX = ai.steps[s].X + charBuffer + 1;
                            previousStepY = ai.steps[s].Y + 1 + i;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("*");
                        }
                        charBuffer += 1;
                    }
                }

                Thread.Sleep(speed);
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