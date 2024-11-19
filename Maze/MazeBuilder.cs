
namespace Maze
{
    internal class MazeBuilder
    {
        private Random random = new Random();
        private List<MazePoint> previousPoints;
        internal MazePoint[,] GenerateMaze(MazePoint start, int yLength, int xLength)
        {
            var maze = new MazePoint[yLength, xLength];

            previousPoints = new List<MazePoint>();
            var currentPoint = start;

            for (int i = 0; i < yLength; i++)
            {
                for (int j = 0; j < xLength; j++)
                {
                    maze[currentPoint.Y, currentPoint.X] = currentPoint;

                    var emptyNeighborPoints = GetEmptyNeighborPoints(currentPoint, maze);

                    MazePoint nextPoint = null;

                    if (emptyNeighborPoints.Count == 0)
                    {
                        BacktrackToEmptyNeighbor(ref emptyNeighborPoints, ref currentPoint, ref nextPoint, ref maze);
                    }

                    if (emptyNeighborPoints.Count > 0)
                    {
                        nextPoint = emptyNeighborPoints[random.Next(0, emptyNeighborPoints.Count)];
                    }

                    if (nextPoint != null)
                    {
                        ConnectPoints(ref currentPoint, ref nextPoint, ref maze);
                    }
                }
            }

            return maze;
        }

        private void BacktrackToEmptyNeighbor(ref List<MazePoint> emptyNeighborPoints, ref MazePoint currentPoint, ref MazePoint? nextPoint, ref MazePoint[,] maze)
        {
            for (int i = previousPoints.Count - 1; i >= 0; i--)
            {
                currentPoint = previousPoints[i];
                emptyNeighborPoints = GetEmptyNeighborPoints(currentPoint, maze);

                if (emptyNeighborPoints.Count > 0)
                {
                    return;
                }
            }
        }

        private void ConnectPoints(ref MazePoint currentPoint, ref MazePoint nextPoint, ref MazePoint[,] maze)
        {
            if (nextPoint.Connections.Contains("Up"))
            {
                maze[currentPoint.Y, currentPoint.X].Connections.Add("Down");
            }
            if (nextPoint.Connections.Contains("Right"))
            {
                maze[currentPoint.Y, currentPoint.X].Connections.Add("Left");
            }
            if (nextPoint.Connections.Contains("Down"))
            {
                maze[currentPoint.Y, currentPoint.X].Connections.Add("Up");
            }
            if (nextPoint.Connections.Contains("Left"))
            {
                maze[currentPoint.Y, currentPoint.X].Connections.Add("Right");
            }

            previousPoints.Add(currentPoint);
            currentPoint = nextPoint;
        }

        private List<MazePoint> GetEmptyNeighborPoints(MazePoint currentPoint, MazePoint[,] maze)
        {
            var emptyNeighborPoints = new List<MazePoint>();

            if (currentPoint.Y - 1 >= 0)
            {
                if (maze[currentPoint.Y - 1, currentPoint.X] == null)
                {
                    var emptyPoint = new MazePoint();
                    emptyPoint.Y = currentPoint.Y - 1;
                    emptyPoint.X = currentPoint.X;
                    emptyPoint.Connections.Add("Down");
                    emptyNeighborPoints.Add(emptyPoint);
                }
            }
            if (currentPoint.Y + 1 < maze.GetLength(0))
            {
                if (maze[currentPoint.Y + 1, currentPoint.X] == null)
                {
                    var emptyPoint = new MazePoint();
                    emptyPoint.Y = currentPoint.Y + 1;
                    emptyPoint.X = currentPoint.X;
                    emptyPoint.Connections.Add("Up");
                    emptyNeighborPoints.Add(emptyPoint);
                }
            }
            if (currentPoint.X - 1 >= 0)
            {
                if (maze[currentPoint.Y, currentPoint.X - 1] == null)
                {
                    var emptyPoint = new MazePoint();
                    emptyPoint.Y = currentPoint.Y;
                    emptyPoint.X = currentPoint.X - 1;
                    emptyPoint.Connections.Add("Right");
                    emptyNeighborPoints.Add(emptyPoint);
                }
            }
            if (currentPoint.X + 1 < maze.GetLength(1))
            {
                if (maze[currentPoint.Y, currentPoint.X + 1] == null)
                {
                    var emptyPoint = new MazePoint();
                    emptyPoint.Y = currentPoint.Y;
                    emptyPoint.X = currentPoint.X + 1;
                    emptyPoint.Connections.Add("Left");
                    emptyNeighborPoints.Add(emptyPoint);
                }
            }

            return emptyNeighborPoints;
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
                    start.X = xLength - 1;
                    start.Connections.Add("Right");
                }
                else
                {
                    start.Connections.Add("Left");
                }
            }
            else
            {
                start.X = random.Next(0, xLength);
                if (start.Y > 0)
                {
                    start.Connections.Add("Down");
                }
                if (start.Y == 0)
                {
                    start.Connections.Add("Up");
                }
            }

            return start;
        }
    }
}