






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
                var currentStep = steps[steps.Count - 1];
                MazePoint? nextStep;
                possibleSteps = GetPossibleSteps(maze);

                if (possibleSteps.Count == 0)
                {
                    BacktrackToPreviousPossibleSteps();
                    if (possibleSteps.Count == 0)
                    {
                        possibleSteps = GetPossibleSteps(maze);
                    }
                }

                if (possibleSteps.Any(s => s.IsExit == true))
                {
                    nextStep = possibleSteps.FirstOrDefault(s => s.IsExit == true);
                    steps.Add(nextStep);
                    break;
                }

                if (possibleSteps.Count == 0)
                {
                    foreach (var point in maze)
                    {
                        if (point.IsExit == true)
                        {
                            nextStep = point;
                        }
                    }
                    BacktrackToPreviousPossibleSteps();
                    possibleSteps = GetPossibleSteps(maze);
                    break;
                }

                
                nextStep = possibleSteps[random.Next(0, possibleSteps.Count)];
                possibleSteps.Remove(nextStep);
                steps.Add(nextStep);
                previousPossibleSteps.Remove(nextStep);
            }
        }

        private void BacktrackToPreviousPossibleSteps()
        {
            var targetSteps = new List<MazePoint>();

            for (int i = 0; i < previousPossibleSteps.Count; i++)
            {
                var targetStep = steps.Find(
                    s =>
                    (s.Y == previousPossibleSteps[i].Y - 1 && s.X == previousPossibleSteps[i].X && !previousPossibleSteps[i].Walls.Contains("Up")) ||
                    (s.Y == previousPossibleSteps[i].Y + 1 && s.X == previousPossibleSteps[i].X && !previousPossibleSteps[i].Walls.Contains("Down")) ||
                    (s.X == previousPossibleSteps[i].X - 1 && s.Y == previousPossibleSteps[i].Y && !previousPossibleSteps[i].Walls.Contains("Left")) ||
                    (s.X == previousPossibleSteps[i].X + 1 && s.Y == previousPossibleSteps[i].Y && !previousPossibleSteps[i].Walls.Contains("Right")));

                if (targetStep != null)
                {
                    targetSteps.Add(targetStep);
                }
            }

            for (int i = steps.Count - 1; i >= 0; i--)
            {
                if (i != steps.Count - 1)
                {
                    steps.Add(steps[i]);
                }

                var currentStep = steps[i];

                for (int j = 0; j < targetSteps.Count; j++)
                {
                    if (steps[i].Y == targetSteps[j].Y - 1 && steps[i].X == targetSteps[j].X && !targetSteps[j].Walls.Contains("Up")) // Step down
                    {
                        steps.Add(targetSteps[j]);
                        if (previousPossibleSteps.Count < 2)
                        {
                            possibleSteps.Insert(0, previousPossibleSteps[j]);
                        }
                        return;
                    }
                    if (steps[i].Y == targetSteps[j].Y + 1 && steps[i].X == targetSteps[j].X && !targetSteps[j].Walls.Contains("Down"))
                    {
                        steps.Add(targetSteps[j]);
                        if (previousPossibleSteps.Count < 2)
                        {
                            possibleSteps.Insert(0, previousPossibleSteps[j]);
                        }
                        return;
                    }
                    if (steps[i].X == targetSteps[j].X - 1 && steps[i].Y == targetSteps[j].Y && !targetSteps[j].Walls.Contains("Left"))
                    {
                        steps.Add(targetSteps[j]);
                        if (previousPossibleSteps.Count < 2)
                        {
                            possibleSteps.Insert(0, previousPossibleSteps[j]);
                        }
                        return;
                    }
                    if (steps[i].X == targetSteps[j].X + 1 && steps[i].Y == targetSteps[j].Y && !targetSteps[j].Walls.Contains("Right"))
                    {
                        steps.Add(targetSteps[j]);
                        if (previousPossibleSteps.Count < 2)
                        {
                            possibleSteps.Insert(0, previousPossibleSteps[j]);
                        }
                        return;
                    }
                    var upStep = steps.Find(s => (s.Y == steps[i].Y - 1 && s.X == steps[i].X && !s.Walls.Contains("Down")));
                    var downStep = steps.Find(s => (s.Y == steps[i].Y + 1 && s.X == steps[i].X && !s.Walls.Contains("Up")));
                    var leftStep = steps.Find(s => (s.X == steps[i].X - 1 && s.Y == steps[i].Y && !s.Walls.Contains("Right")));
                    var rightStep = steps.Find(s => (s.X == steps[i].X + 1 && s.Y == steps[i].Y && !s.Walls.Contains("Left")));

                    if (upStep != null && steps.IndexOf(upStep) < i && steps.IndexOf(targetSteps[j]) <= steps.IndexOf(upStep))
                    {
                        i = steps.IndexOf(upStep);
                        steps.Add(steps[i]);
                    }
                    if (downStep != null && steps.IndexOf(downStep) < i && steps.IndexOf(targetSteps[j]) <= steps.IndexOf(downStep))
                    {
                        i = steps.IndexOf(downStep);
                        steps.Add(steps[i]);
                    }
                    if (leftStep != null && steps.IndexOf(leftStep) < i && steps.IndexOf(targetSteps[j]) <= steps.IndexOf(leftStep))
                    {
                        i = steps.IndexOf(leftStep);
                        steps.Add(steps[i]);
                    }
                    if (rightStep != null && steps.IndexOf(rightStep) < i && steps.IndexOf(targetSteps[j]) <= steps.IndexOf(rightStep))
                    {
                        i = steps.IndexOf(rightStep);
                        steps.Add(steps[i]);
                    }
                    if (steps[i] == targetSteps.FirstOrDefault(t => t == steps[i]))
                    {
                        if (previousPossibleSteps.Count < 2)
                        {
                            possibleSteps.Insert(0, previousPossibleSteps[j]);
                        }
                        return;
                    }
                }
            }
        }

        private List<MazePoint> GetPossibleSteps(MazePoint[,] maze)
        {
            var possibleSteps = new List<MazePoint>();
            var currentStep = steps.ElementAt(steps.Count - 1);

            if (currentStep.Y - 1 >= 0)
            {
                var upStep = maze[currentStep.Y - 1, currentStep.X];
                if (!upStep.Walls.Contains("Down"))
                {
                    if (!steps.Contains(upStep))
                    {
                        possibleSteps.Add(upStep);

                        if (!previousPossibleSteps.Contains(upStep))
                        {
                            previousPossibleSteps.Insert(0, upStep);
                        }
                    }
                }
            }
            if (currentStep.Y + 1 < maze.GetLength(0))
            {
                var downStep = maze[currentStep.Y + 1, currentStep.X];
                if (!downStep.Walls.Contains("Up"))
                {
                    if (!steps.Contains(downStep))
                    {
                        possibleSteps.Add(downStep);

                        if (!previousPossibleSteps.Contains(downStep))
                        {
                            previousPossibleSteps.Insert(0, downStep);
                        }
                    }
                }
            }
            if (currentStep.X - 1 >= 0)
            {
                var leftStep = maze[currentStep.Y, currentStep.X - 1];
                if (!leftStep.Walls.Contains("Right"))
                {
                    if (!steps.Contains(leftStep))
                    {
                        possibleSteps.Add(leftStep);

                        if (!previousPossibleSteps.Contains(leftStep))
                        {
                            previousPossibleSteps.Insert(0, leftStep);
                        }
                    }
                }
            }
            if (currentStep.X + 1 < maze.GetLength(1))
            {
                var rightStep = maze[currentStep.Y, currentStep.X + 1];
                if (!rightStep.Walls.Contains("Left"))
                {
                    if (!steps.Contains(rightStep))
                    {
                        possibleSteps.Add(rightStep);

                        if (!previousPossibleSteps.Contains(rightStep))
                        {
                            previousPossibleSteps.Insert(0, rightStep);
                        }
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