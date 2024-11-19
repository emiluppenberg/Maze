namespace Maze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new MazeBuilder();

            var start = builder.GenerateStartPoint(10, 10);
            var maze = builder.GenerateMaze(start, 10, 10);
            DisplayMaze(maze);

            Console.ReadKey();
        }

        static void DisplayMaze(int[,] maze)
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    //if (maze[i, j].Contains('*')) // Current Position
                    //{
                    //    Console.ForegroundColor = ConsoleColor.Green;
                    //    Console.Write(maze[i, j]);
                    //}
                    //if (maze[i, j].Contains('+')) // Wall position
                    //{
                    //    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    //    Console.Write(maze[i, j]);
                    //}
                    //if (maze[i, j].Contains('X')) // Algorithm wall position
                    //{
                    //    Console.ForegroundColor = ConsoleColor.Red;
                    //    Console.Write(maze[i, j]);
                    //}
                    //if (maze[i, j].Contains('O')) // Path position
                    //{
                    //    Console.ForegroundColor = ConsoleColor.Blue;
                    //    Console.Write(maze[i, j]);
                    //}
                }
                Console.WriteLine();
            }
        }
    }
}