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

                Console.ReadKey(true);
                Console.Clear();
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