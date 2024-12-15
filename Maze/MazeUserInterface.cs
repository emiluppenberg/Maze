using Maze.DataAccess.Data;
using Maze.DataAccess.Models;

namespace Maze
{
    public class MazeUserInterface
    {
        private MazeDataRepository mazeDataRepository;

        public MazeUserInterface(MazeDataRepository mazeDataRepository)
        {
            this.mazeDataRepository = mazeDataRepository;
        }

        public void Menu()
        {
            bool menu = true;

            while (menu)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(
                    "\nPress 1 - New maze\n" +
                    "Press 2 - View data\n" +
                    "Any other key - Exit program\n");

                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        menu = false;
                        break;
                    case '2':
                        ViewDataMenu();
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            }
        }
        public void ViewDataMenu()
        {
            var mazeSizes = mazeDataRepository.AllMazeSizes();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Maze size (X, Y)");
                for (int i = 0; i < mazeSizes.Count; i++)
                {
                    Console.Write($"Press {i + 1} - {mazeSizes[i].XLength}, {mazeSizes[i].YLength}\n");
                }

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input <= mazeSizes.Count && input > 0)
                    {
                        var mazeData = mazeDataRepository.GetAllByMazeSize
                            (mazeSizes[input - 1].YLength, mazeSizes[input - 1].XLength);
                        ViewMazeSize(mazeData);
                        break;
                    }
                }
            }
        }
        public void ViewMazeSize(List<MazeData> mazeData)
        {
            Console.Clear();
            foreach (var data in mazeData)
            {
                Console.Write($"MazeID: {data.MazeDataId} | AIID: {data.AIDataId} | AISteps: {data.MyAIData.Steps}\n");
            }
            Console.ReadKey(true);
        }
        public void DisplayMazeAndAI(MazePoint[,] maze, MazeAI ai, int speed)
        {
            int previousStepY = 0;
            int previousStepX = 0;

            for (int s = 0; s < ai.steps.Count; s++)
            {
                Console.CursorVisible = false;
                if (s > 0)
                {
                    Console.SetCursorPosition(previousStepX, previousStepY);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("*");
                }
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    int charBuffer = 0;

                    for (int j = 0; j < maze.GetLength(1); j++)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        if (maze[i, j].Walls.Contains("Up"))
                        {
                            Console.SetCursorPosition(maze[i, j].X + charBuffer, maze[i, j].Y + i);
                            Console.Write("·-·");
                        }
                        if (maze[i, j].Walls.Contains("Right"))
                        {
                            Console.SetCursorPosition(maze[i, j].X + charBuffer + 2, maze[i, j].Y + i);
                            Console.Write("·");
                            Console.SetCursorPosition(maze[i, j].X + charBuffer + 2, maze[i, j].Y + 1 + i);
                            Console.Write("|");
                            Console.SetCursorPosition(maze[i, j].X + charBuffer + 2, maze[i, j].Y + 2 + i);
                            Console.Write("·");
                        }
                        if (maze[i, j].Walls.Contains("Down"))
                        {
                            Console.SetCursorPosition(maze[i, j].X + charBuffer, maze[i, j].Y + 2 + i);
                            Console.Write("·-·");
                        }
                        if (maze[i, j].Walls.Contains("Left"))
                        {
                            Console.SetCursorPosition(maze[i, j].X + charBuffer, maze[i, j].Y + i);
                            Console.Write("·");
                            Console.SetCursorPosition(maze[i, j].X + charBuffer, maze[i, j].Y + 1 + i);
                            Console.Write("|");
                            Console.SetCursorPosition(maze[i, j].X + charBuffer, maze[i, j].Y + 2 + i);
                            Console.Write("·");
                        }
                        if (maze[i, j] == ai.steps[s])
                        {
                            Console.SetCursorPosition(ai.steps[s].X + charBuffer + 1, ai.steps[s].Y + 1 + i);
                            previousStepX = ai.steps[s].X + charBuffer + 1;
                            previousStepY = ai.steps[s].Y + 1 + i;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("*");
                        }
                        charBuffer += 1;
                    }
                }

                Thread.Sleep(speed);
            }
        }
        public void SaveData(int ySize, int xSize, int steps)
        {
            var aiData = new AIData()
            {
                Steps = steps
            };
            var mazeData = new MazeData()
            {
                XLength = xSize,
                YLength = ySize,
                MyAIData = aiData
            };

            mazeDataRepository.Add(mazeData);
            mazeDataRepository.SaveChanges();
        }
    }
}