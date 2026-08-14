namespace Maze
{
    public class MazeUserInterface
    {
        public int InputXSize()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Maze size X: ");

                if (int.TryParse(Console.ReadLine(), out int xSize))
                {
                    if (xSize < Console.BufferWidth / 2)
                    {
                        return xSize;
                    }

                    Console.WriteLine("Maze cannot be larger than window");
                }

                Console.WriteLine("Invalid input");
                System.Threading.Thread.Sleep(1000);
            }
        }

        public int InputYSize()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Maze size Y: ");

                if (int.TryParse(Console.ReadLine(), out int ySize))
                {
                    if (ySize < Console.BufferHeight / 2)
                    {
                        return ySize;
                    }

                    Console.WriteLine("Maze cannot be larger than window");
                }

                Console.WriteLine("Invalid input");
                System.Threading.Thread.Sleep(1000);
            }
        }

        public int InputSpeed()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("(Less is faster) Speed: ");

                if (int.TryParse(Console.ReadLine(), out int speed))
                {
                    if (speed > 0)
                    {
                        return speed;
                    }

                    Console.WriteLine("Speed must be greater than 0");
                }

                Console.WriteLine("Invalid input");
                System.Threading.Thread.Sleep(1000);
            }
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

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey(true);
        }
    }
}