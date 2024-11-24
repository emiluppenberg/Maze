
namespace Maze
{
    internal class MazeBuilder
    {
        private Random random = new Random();
        private List<MazePoint> previousPoints = new List<MazePoint>();
        internal MazePoint[,] GenerateMaze(int yLength, int xLength)
        {
            var maze = new MazePoint[yLength, xLength];

            previousPoints = new List<MazePoint>();
            var currentPoint = GenerateStartPoint(yLength, xLength);

            for (int i = 0; i < yLength; i++)
            {
                for (int j = 0; j < xLength; j++)
                {
                    maze[currentPoint.Y, currentPoint.X] = currentPoint;

                    var emptyNeighborPoints = GetEmptyNeighborPoints(currentPoint, maze);

                    MazePoint? nextPoint = null;

                    if (i == yLength - 1 && j == xLength - 1)
                    {
                        while (true)
                        {
                            GenerateExitPoint(ref emptyNeighborPoints, ref currentPoint, ref maze);
                            if (currentPoint.IsStart != true)
                            {
                                maze[currentPoint.Y, currentPoint.X] = currentPoint;
                                break;
                            }
                        }
                    }

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

        private void GenerateExitPoint(ref List<MazePoint> emptyNeighborPoints, ref MazePoint currentPoint, ref MazePoint[,] maze)
        {
            for (int i = previousPoints.Count - 1; i >= 0; i--)
            {
                currentPoint = previousPoints[random.Next(0, previousPoints.Count)];
                emptyNeighborPoints = GetEmptyNeighborPoints(currentPoint, maze);

                if (currentPoint.X == 0 && currentPoint.Walls.Count > 1)
                {
                    currentPoint.Walls.Remove("Left");
                    currentPoint.IsExit = true;
                    return;
                }
                if (currentPoint.X == maze.GetLength(1) - 1 && currentPoint.Walls.Count > 1)
                {
                    currentPoint.Walls.Remove("Right");
                    currentPoint.IsExit = true;
                    return;
                }
                if (currentPoint.Y == 0 && currentPoint.Walls.Count > 1)
                {
                    currentPoint.Walls.Remove("Up");
                    currentPoint.IsExit = true;
                    return;
                }
                if (currentPoint.Y == maze.GetLength(0) - 1 && currentPoint.Walls.Count > 1)
                {
                    currentPoint.Walls.Remove("Down");
                    currentPoint.IsExit = true;
                    return;
                }
            }
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
            if (!nextPoint.Walls.Contains("Up"))
            {
                maze[currentPoint.Y, currentPoint.X].Walls.Remove("Down");
            }
            if (!nextPoint.Walls.Contains("Right"))
            {
                maze[currentPoint.Y, currentPoint.X].Walls.Remove("Left");
            }
            if (!nextPoint.Walls.Contains("Down"))
            {
                maze[currentPoint.Y, currentPoint.X].Walls.Remove("Up");
            }
            if (!nextPoint.Walls.Contains("Left"))
            {
                maze[currentPoint.Y, currentPoint.X].Walls.Remove("Right");
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
                    emptyPoint.Walls.Remove("Down");
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
                    emptyPoint.Walls.Remove("Up");
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
                    emptyPoint.Walls.Remove("Right");
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
                    emptyPoint.Walls.Remove("Left");
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
                    start.Walls.Remove("Right");
                }
                else
                {
                    start.Walls.Remove("Left");
                }
            }
            else
            {
                start.X = random.Next(0, xLength);
                if (start.Y > 0)
                {
                    start.Walls.Remove("Down");
                }
                if (start.Y == 0)
                {
                    start.Walls.Remove("Up");
                }
            }
            start.IsStart = true;
            return start;
        }
    }
}