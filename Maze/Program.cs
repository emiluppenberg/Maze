namespace Maze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new MazeBuilder();

            var start = builder.GenerateStartPoint(10, 10);
            var maze = builder.GenerateMaze(start, 10, 10);

            Console.ReadKey();
        }

        static void DisplayMaze(MazePoint[,] maze)
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j].Connections.Contains("Up"))
                    {

                    }

                }
                Console.WriteLine();
            }
        }
    }
}