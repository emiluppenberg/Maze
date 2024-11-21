






using System.Drawing.Text;

namespace Maze
{
    internal class MazeAI
    {
        private Random random = new Random();
        private List<MazePoint> possibleSteps = new List<MazePoint>();
        private List<MazePoint> previousPossibleSteps = new List<MazePoint>();
        public List<MazePoint> steps = new List<MazePoint>();
        public void SolveMaze(MazePoint[,] maze)
        {
            var startingPoint = FindStartingPoint(maze);
            previousPossibleSteps.Clear();
            steps.Clear();
            possibleSteps.Clear();
            steps.Add(startingPoint);

            while (true)
            {
                MazePoint? nextStep;
                possibleSteps = GetPossibleSteps(maze);
                var currentStep = steps[steps.Count - 1];

                if (possibleSteps.Count == 0)
                {
                    BacktrackToPreviousPossibleSteps();
                    possibleSteps = GetPossibleSteps(maze);
                }

                if (possibleSteps.Any(s => s.IsExit == true))
                {
                    nextStep = possibleSteps.FirstOrDefault(s => s.IsExit == true);
                    steps.Add(nextStep);
                    break;
                }

                if (possibleSteps.Count == 0)
                {
                    BacktrackToPreviousPossibleSteps();
                    possibleSteps = GetPossibleSteps(maze);
                }

                nextStep = possibleSteps[random.Next(0, possibleSteps.Count)];
                possibleSteps.Remove(nextStep);
                steps.Add(nextStep);
                previousPossibleSteps.Remove(nextStep);
            }
        }

        private List<MazePoint> FindClosestPossibleSteps(MazePoint[,] maze)
        {
            var possibleSteps = new List<MazePoint>();
            MazePoint? nextStep;
            var currentStepY = steps[steps.Count - 1].Y;
            var currentStepX = steps[steps.Count - 1].X;
            int closestY = previousPossibleSteps[previousPossibleSteps.Count - 1].Y;
            int closestX = previousPossibleSteps[previousPossibleSteps.Count - 1].X;

            for (int j = previousPossibleSteps.Count - 1; j >= 0; j--)
            {
                var differenceY = Math.Abs(currentStepY - previousPossibleSteps[j].Y);
                var differenceX = Math.Abs(currentStepX - previousPossibleSteps[j].X);

                if (differenceY <= closestY)
                {
                    if (differenceX <= closestX)
                    {
                        closestY = previousPossibleSteps[j].Y;
                        closestX = previousPossibleSteps[j].X;
                    }
                }
            }

            nextStep = previousPossibleSteps.FirstOrDefault(s => s.Y == closestY && s.X == closestX);
            possibleSteps.Add(nextStep);

            return possibleSteps;
            //    var possibleSteps = new List<MazePoint>();
            //    int closestY = steps[steps.Count - 1].Y;
            //    int closestX = steps[steps.Count - 1].X;

            //    for (int i = previousPossibleSteps.Count - 1; i >= 0; i--)
            //    {
            //        if (steps[steps.Count - 1].Y - previousPossibleSteps[i].Y < closestY)
            //        {
            //            closestY = previousPossibleSteps[i].Y;
            //        }
            //        if (steps[steps.Count - 1].X - previousPossibleSteps[i].X < closestX)
            //        {
            //            closestX = previousPossibleSteps[i].X;
            //        }
            //    }

            //    for (int i = 0; i < maze.GetLength(0); i++)
            //    {
            //        for (int j = 0; j < maze.GetLength(1); j++)
            //        {
            //            if (maze[i, j].Y == closestY && maze[i, j].X == closestX)
            //            {
            //                possibleSteps.Add(maze[i, j]);
            //            }
            //        }
            //    }

            //    return possibleSteps;
        }

        private void BacktrackToPreviousPossibleSteps()
        {
            for (int i = previousPossibleSteps.Count - 1; i >= 0; i--)
            {
                var targetStep = steps.Find(
                    s =>
                    (s.Y == previousPossibleSteps[i].Y - 1 && s.X == previousPossibleSteps[i].X && !previousPossibleSteps[i].Walls.Contains("Up")) ||
                    (s.Y == previousPossibleSteps[i].Y + 1 && s.X == previousPossibleSteps[i].X && !previousPossibleSteps[i].Walls.Contains("Down")) ||
                    (s.X == previousPossibleSteps[i].X - 1 && s.Y == previousPossibleSteps[i].Y && !previousPossibleSteps[i].Walls.Contains("Left")) ||
                    (s.X == previousPossibleSteps[i].X + 1 && s.Y == previousPossibleSteps[i].Y && !previousPossibleSteps[i].Walls.Contains("Right")));

                if (targetStep != null)
                {
                    for (int j = steps.Count - 1; j >= 0; j--)
                    {
                        steps.Add(steps[j]);

                        if (steps[j] == targetStep)
                        {
                            previousPossibleSteps.Remove(previousPossibleSteps[i]);
                            return;
                        }
                        if (steps[j].Y == targetStep.Y - 1 && steps[j].X == targetStep.X && !targetStep.Walls.Contains("Up"))
                        {
                            steps.Add(targetStep);
                            previousPossibleSteps.Remove(previousPossibleSteps[i]);
                            return;
                        }
                        if (steps[j].Y == targetStep.Y + 1 && steps[j].X == targetStep.X && !targetStep.Walls.Contains("Down"))
                        {
                            steps.Add(targetStep);
                            previousPossibleSteps.Remove(previousPossibleSteps[i]);
                            return;
                        }
                        if (steps[j].X == targetStep.X - 1 && steps[j].Y == targetStep.Y && !targetStep.Walls.Contains("Left"))
                        {
                            steps.Add(targetStep);
                            previousPossibleSteps.Remove(previousPossibleSteps[i]);
                            return;
                        }
                        if (steps[j].X == targetStep.X + 1 && steps[j].Y == targetStep.Y && !targetStep.Walls.Contains("Right"))
                        {
                            steps.Add(targetStep);
                            previousPossibleSteps.Remove(previousPossibleSteps[i]);
                            return;
                        }
                    }
                }
            }
            //if (currentStepY - 1 >= 0)
            //{
            //    if (!maze[currentStepY - 1, currentStepX].Walls.Contains("Down"))
            //    {
            //        if (previousPossibleSteps.Contains(maze[currentStepY - 1, currentStepX]))
            //        {
            //            possibleSteps.Add(maze[currentStepY - 1, currentStepX]);
            //        }
            //        if (steps.Contains(maze[currentStepY - 1, currentStepX]))
            //        {
            //            if (currentStepY - 2 >= 0)
            //            {
            //                if (!maze[currentStepY - 2, currentStepX].Walls.Contains("Down"))
            //                {
            //                    if (!steps.Contains(maze[currentStepY - 2, currentStepX]))
            //                    {
            //                        possibleSteps.Add(maze[currentStepY - 1, currentStepX]);
            //                    }
            //                }
            //            }
            //            if (currentStepX - 1 >= 0)
            //            {
            //                if (!maze[currentStepY - 1, currentStepX - 1].Walls.Contains("Right"))
            //                {
            //                    if (!steps.Contains(maze[currentStepY - 1, currentStepX - 1]))
            //                    {
            //                        possibleSteps.Add(maze[currentStepY - 1, currentStepX]);
            //                    }
            //                }
            //            }
            //            if (currentStepX + 1 < maze.GetLength(1))
            //            {
            //                if (!maze[currentStepY - 1, currentStepX + 1].Walls.Contains("Left"))
            //                {
            //                    if (!steps.Contains(maze[currentStepY - 1, currentStepX + 1]))
            //                    {
            //                        possibleSteps.Add(maze[currentStepY - 1, currentStepX]);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //if (currentStepY + 1 < maze.GetLength(0))
            //{
            //    if (!maze[currentStepY + 1, currentStepX].Walls.Contains("Up"))
            //    {
            //        if (!steps.Contains(maze[currentStepY + 1, currentStepX]))
            //        {
            //            possibleSteps.Add(maze[currentStepY + 1, currentStepX]);
            //        }
            //    }
            //    if (steps.Contains(maze[currentStepY + 1, currentStepX]))
            //    {
            //        if (currentStepY + 2 < maze.GetLength(0))
            //        {
            //            if (!maze[currentStepY + 2, currentStepX].Walls.Contains("Up"))
            //            {
            //                if (!steps.Contains(maze[currentStepY + 2, currentStepX]))
            //                {
            //                    possibleSteps.Add(maze[currentStepY + 1, currentStepX]);
            //                }
            //            }
            //        }
            //        if (currentStepX - 1 >= 0)
            //        {
            //            if (!maze[currentStepY + 1, currentStepX - 1].Walls.Contains("Right"))
            //            {
            //                if (!steps.Contains(maze[currentStepY + 1, currentStepX - 1]))
            //                {
            //                    possibleSteps.Add(maze[currentStepY + 1, currentStepX]);
            //                }
            //            }
            //        }
            //        if (currentStepX + 1 < maze.GetLength(1))
            //        {
            //            if (!maze[currentStepY + 1, currentStepX + 1].Walls.Contains("Left"))
            //            {
            //                if (!steps.Contains(maze[currentStepY + 1, currentStepX + 1]))
            //                {
            //                    possibleSteps.Add(maze[currentStepY + 1, currentStepX]);
            //                }
            //            }
            //        }
            //    }
            //}
            //if (currentStepX - 1 >= 0)
            //{
            //    if (!maze[currentStepY, currentStepX - 1].Walls.Contains("Right"))
            //    {
            //        if (!steps.Contains(maze[currentStepY, currentStepX - 1]))
            //        {
            //            possibleSteps.Add(maze[currentStepY, currentStepX - 1]);
            //        }
            //    }
            //    if (steps.Contains(maze[currentStepY, currentStepX - 1]))
            //    {
            //        if (currentStepX - 2 >= 0)
            //        {
            //            if (!maze[currentStepY, currentStepX - 2].Walls.Contains("Right"))
            //            {
            //                if (!steps.Contains(maze[currentStepY, currentStepX - 2]))
            //                {
            //                    possibleSteps.Add(maze[currentStepY, currentStepX - 1]);
            //                }
            //            }
            //        }
            //        if (currentStepY - 1 >= 0)
            //        {
            //            if (!maze[currentStepY - 1, currentStepX - 1].Walls.Contains("Down"))
            //            {
            //                if (!steps.Contains(maze[currentStepY - 1, currentStepX - 1]))
            //                {
            //                    possibleSteps.Add(maze[currentStepY, currentStepX - 1]);
            //                }
            //            }
            //        }
            //        if (currentStepY + 1 < maze.GetLength(0))
            //        {
            //            if (!maze[currentStepY + 1, currentStepX - 1].Walls.Contains("Up"))
            //            {
            //                if (!steps.Contains(maze[currentStepY + 1, currentStepX - 1]))
            //                {
            //                    possibleSteps.Add(maze[currentStepY, currentStepX - 1]);
            //                }
            //            }
            //        }
            //    }
            //}
            //if (currentStepX + 1 < maze.GetLength(1))
            //{
            //    if (!maze[currentStepY, currentStepX + 1].Walls.Contains("Left"))
            //    {
            //        if (!steps.Contains(maze[currentStepY, currentStepX + 1]))
            //        {
            //            possibleSteps.Add(maze[currentStepY, currentStepX + 1]);
            //        }
            //        if (steps.Contains(maze[currentStepY, currentStepX + 1]))
            //        {
            //            if (currentStepX + 2 < maze.GetLength(1))
            //            {
            //                if (!maze[currentStepY, currentStepX + 2].Walls.Contains("Left"))
            //                {
            //                    if (!steps.Contains(maze[currentStepY, currentStepX + 2]))
            //                    {
            //                        possibleSteps.Add(maze[currentStepY, currentStepX + 1]);
            //                    }
            //                }
            //            }
            //            if (currentStepY - 1 >= 0)
            //            {
            //                if (!maze[currentStepY - 1, currentStepX + 1].Walls.Contains("Down"))
            //                {
            //                    if (!steps.Contains(maze[currentStepY - 1, currentStepX + 1]))
            //                    {
            //                        possibleSteps.Add(maze[currentStepY, currentStepX + 1]);
            //                    }
            //                }
            //            }
            //            if (currentStepY + 1 < maze.GetLength(0))
            //            {
            //                if (!maze[currentStepY + 1, currentStepX + 1].Walls.Contains("Up"))
            //                {
            //                    if (!steps.Contains(maze[currentStepY + 1, currentStepX + 1]))
            //                    {
            //                        possibleSteps.Add(maze[currentStepY, currentStepX + 1]);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //if (possibleSteps.Count > 0)
            //{
            //    return;
            //}
        }

        private List<MazePoint> GetPossibleSteps(MazePoint[,] maze)
        {
            var possibleSteps = new List<MazePoint>();
            var currentStepY = steps.ElementAt(steps.Count - 1).Y;
            var currentStepX = steps.ElementAt(steps.Count - 1).X;

            if (currentStepY - 1 >= 0)
            {
                if (!maze[currentStepY - 1, currentStepX].Walls.Contains("Down"))
                {
                    if (!steps.Contains(maze[currentStepY - 1, currentStepX]))
                    {
                        possibleSteps.Add(maze[currentStepY - 1, currentStepX]);
                        previousPossibleSteps.Add(maze[currentStepY - 1, currentStepX]);
                    }
                }
            }
            if (currentStepY + 1 < maze.GetLength(0))
            {
                if (!maze[currentStepY + 1, currentStepX].Walls.Contains("Up"))
                {
                    if (!steps.Contains(maze[currentStepY + 1, currentStepX]))
                    {
                        possibleSteps.Add(maze[currentStepY + 1, currentStepX]);
                        previousPossibleSteps.Add(maze[currentStepY + 1, currentStepX]);
                    }
                }
            }
            if (currentStepX - 1 >= 0)
            {
                if (!maze[currentStepY, currentStepX - 1].Walls.Contains("Right"))
                {
                    if (!steps.Contains(maze[currentStepY, currentStepX - 1]))
                    {
                        possibleSteps.Add(maze[currentStepY, currentStepX - 1]);
                        previousPossibleSteps.Add(maze[currentStepY, currentStepX - 1]);
                    }
                }
            }
            if (currentStepX + 1 < maze.GetLength(1))
            {
                if (!maze[currentStepY, currentStepX + 1].Walls.Contains("Left"))
                {
                    if (!steps.Contains(maze[currentStepY, currentStepX + 1]))
                    {
                        possibleSteps.Add(maze[currentStepY, currentStepX + 1]);
                        previousPossibleSteps.Add(maze[currentStepY, currentStepX + 1]);
                    }
                }
            }

            return possibleSteps;
        }

        public MazePoint FindStartingPoint(MazePoint[,] maze)
        {
            var startingPoint = new MazePoint();

            foreach (var point in maze)
            {
                if (point.IsStart == true)
                {
                    startingPoint = point;
                }
            }

            return startingPoint;
        }
    }
}