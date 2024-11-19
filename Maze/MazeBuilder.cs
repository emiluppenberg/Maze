
namespace Maze
{
    internal class MazeBuilder
    {
        private Random random = new Random();
        internal MazePoint[,] GenerateMaze(MazePoint start, int yLength, int xLength)
        {
            var maze = new MazePoint[yLength, xLength];

            var previousPoint = new List<MazePoint>();
            var currentPoint = start;

            for (int i = 0; i < yLength; i++)
            {
                for (int j = 0; j < xLength; j++)
                {
                    maze[currentPoint.Y, currentPoint.X] = currentPoint;

                    var nextPoint = GetNextPoint(currentPoint, maze);

                    if (nextPoint != null)
                    {
                        if (nextPoint.Directions.Contains("Up"))
                        {
                            maze[currentPoint.Y, currentPoint.X].Directions[0] = "Down";
                        }
                        if (nextPoint.Directions.Contains("Right"))
                        {
                            maze[currentPoint.Y, currentPoint.X].Directions[1] = "Left";
                        }
                        if (nextPoint.Directions.Contains("Down"))
                        {
                            maze[currentPoint.Y, currentPoint.X].Directions[2] = "Up";
                        }
                        if (nextPoint.Directions.Contains("Left"))
                        {
                            maze[currentPoint.Y, currentPoint.X].Directions[3] = "Right";
                        }

                        currentPoint = nextPoint;
                    }
                }
            }

            return maze;
        }

        private MazePoint GetNextPoint(MazePoint currentPoint, MazePoint[,] maze)
        {
            
        }

        public MazePoint GenerateStartPoint(int yLength, int xLength)
        {
            var start = new MazePoint();
            start.Y = random.Next(0, yLength);
            if (start.Y > 0 && start.Y < yLength)
            {
                start.X = random.Next(0, 2);
                if (start.X > 0)
                {
                    start.X = xLength;
                }
            }
            else
            {
                start.X = random.Next(0, xLength);
            }

            return start;
        }
    }
}