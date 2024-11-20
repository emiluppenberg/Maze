﻿
namespace Maze
{
    internal class MazeBuilder
    {
        private Random random = new Random();
        private List<MazePoint> previousPoints;
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

            return start;
        }
    }
}