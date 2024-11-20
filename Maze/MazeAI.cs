






namespace Maze
{
    internal class MazeAI
    {
        private Random random = new Random();
        public void SolveMaze(MazePoint[,] maze)
        {
            var startingPoint = FindStartingPoint(maze);
            var steps = new List<MazePoint>();
            steps.Add(startingPoint);

            while (true)
            {
                MazePoint? nextStep;
                var possibleSteps = GetPossibleSteps(steps, maze);

                if (possibleSteps.Count == 0)
                {
                    BacktrackToPossibleStep(steps, maze, ref possibleSteps);
                }

                if (possibleSteps.Any(s => s.IsExit == true))
                {
                    nextStep = possibleSteps.FirstOrDefault(s => s.IsExit == true);
                    steps.Add(nextStep);
                    break;
                }

                nextStep = possibleSteps[random.Next(0, possibleSteps.Count)];
                steps.Add(nextStep);
            }
        }

        private void BacktrackToPossibleStep(List<MazePoint> steps, MazePoint[,] maze, ref List<MazePoint> possibleSteps)
        {
            for (int i = steps.Count - 1; i >= 0; i--)
            {
                var currentStepY = steps[i - 1].Y;
                var currentStepX = steps[i - 1].X;
                steps.Add(steps[i - 1]);

                if (currentStepY - 1 >= 0)
                {
                    if (!maze[currentStepY - 1, currentStepX].Walls.Contains("Down"))
                    {
                        if (!steps.Contains(maze[currentStepY - 1, currentStepX]))
                        {
                            possibleSteps.Add(maze[currentStepY - 1, currentStepX]);
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
                        }
                    }
                }
                if (possibleSteps.Count > 0)
                {
                    return;
                }
            }
        }

        private List<MazePoint> GetPossibleSteps(List<MazePoint> steps, MazePoint[,] maze)
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