using System.Drawing;

namespace Maze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new MazeBuilder();
            var ai = new MazeAI();

            while (true)
            {
                var maze = builder.GenerateMaze(10, 20);
                DisplayMaze(maze);
                ai.SolveMaze(maze);
                DisplayMazeAndAI(maze, ai);

                Console.ReadKey(true);
                Console.Clear();
            }
        }

        private static void DisplayMazeAndAI(MazePoint[,] maze, MazeAI ai)
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
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("*");
                        }
                        charBuffer += 1;
                    }
                }

                Thread.Sleep(300);
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