






using System.Drawing.Text;

namespace Maze
{
    public class MazeAI
    {
        private Random random = new Random();
        public List<MazePoint> steps = new List<MazePoint>();
        public void SolveMaze(MazePoint[,] maze)
        {
            steps.Clear();
            var startingPoint = EnterMaze(maze);
            var previousPossibleSteps = new List<MazePoint>();
            steps.Add(startingPoint);

            while (true)
            {
                var currentStep = steps[steps.Count - 1];
                MazePoint? nextStep;
                var possibleSteps = GetPossibleSteps(maze, ref previousPossibleSteps);

                if (possibleSteps.Count == 0)
                {
                    var targetSteps = GetTargetSteps(previousPossibleSteps);
                    BacktrackToTargetStep(targetSteps);
                    possibleSteps = GetPossibleSteps(maze, ref previousPossibleSteps);
                }

                if (possibleSteps.Any(s => s.IsExit == true))
                {
                    nextStep = possibleSteps.FirstOrDefault(s => s.IsExit == true);
                    steps.Add(nextStep);
                    break;
                }

                nextStep = possibleSteps[random.Next(0, possibleSteps.Count)];
                steps.Add(nextStep);
                previousPossibleSteps.Remove(nextStep);
            }
        }

        private List<MazePoint> GetTargetSteps(List<MazePoint> previousPossibleSteps)
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

            return targetSteps;
        }

        private void BacktrackToTargetStep(List<MazePoint> targetSteps)
        {
            for (int i = steps.Count - 1; i >= 0; i--)
            {

                if (i != steps.Count - 1)
                {
                    steps.Add(steps[i]);

                    if (steps[i] == targetSteps.FirstOrDefault(t => t == steps[i]))
                    {
                        return;
                    }
                }

                var currentStep = steps[i];

                for (int j = 0; j < targetSteps.Count; j++)
                {
                    var upStep = steps.FirstOrDefault(s => (s.Y == steps[i].Y - 1 && s.X == steps[i].X && !s.Walls.Contains("Down")));
                    var downStep = steps.FirstOrDefault(s => (s.Y == steps[i].Y + 1 && s.X == steps[i].X && !s.Walls.Contains("Up")));
                    var leftStep = steps.FirstOrDefault(s => (s.X == steps[i].X - 1 && s.Y == steps[i].Y && !s.Walls.Contains("Right")));
                    var rightStep = steps.FirstOrDefault(s => (s.X == steps[i].X + 1 && s.Y == steps[i].Y && !s.Walls.Contains("Left")));

                    if (upStep != null && steps.IndexOf(upStep) < i && steps.IndexOf(targetSteps[j]) <= steps.IndexOf(upStep)) // Step up into shorter path
                    {
                        i = steps.IndexOf(upStep);

                        if (steps[i] == targetSteps.FirstOrDefault(t => t == steps[i]))
                        {
                            steps.Add(steps[i]);
                            return;
                        }
                    }
                    if (downStep != null && steps.IndexOf(downStep) < i && steps.IndexOf(targetSteps[j]) <= steps.IndexOf(downStep)) // Step down into shorter path
                    {
                        i = steps.IndexOf(downStep);

                        if (steps[i] == targetSteps.FirstOrDefault(t => t == steps[i]))
                        {
                            steps.Add(steps[i]);
                            return;
                        }
                    }
                    if (leftStep != null && steps.IndexOf(leftStep) < i && steps.IndexOf(targetSteps[j]) <= steps.IndexOf(leftStep)) // Step left into shorter path
                    {
                        i = steps.IndexOf(leftStep);

                        if (steps[i] == targetSteps.FirstOrDefault(t => t == steps[i]))
                        {
                            steps.Add(steps[i]);
                            return;
                        }
                    }
                    if (rightStep != null && steps.IndexOf(rightStep) < i && steps.IndexOf(targetSteps[j]) <= steps.IndexOf(rightStep)) // Step right into shorter path
                    {
                        i = steps.IndexOf(rightStep);

                        if (steps[i] == targetSteps.FirstOrDefault(t => t == steps[i]))
                        {
                            steps.Add(steps[i]);
                            return;
                        }
                    }

                    steps.Add(steps[i]);
                }
            }
        }

        private List<MazePoint> GetPossibleSteps(MazePoint[,] maze, ref List<MazePoint> previousPossibleSteps)
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

        public MazePoint EnterMaze(MazePoint[,] maze)
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