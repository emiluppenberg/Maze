namespace Maze
{
    public class MazePoint
    {
        public int Y { get; set; }
        public int X { get; set; }
        public List<string> Walls { get; set; } = new List<string>() { "Up", "Right", "Down", "Left"};

        public void DisplayPoint()
        {
        }
    }
}