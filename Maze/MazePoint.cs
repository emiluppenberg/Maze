namespace Maze
{
    public class MazePoint
    {
        public int Y { get; set; }
        public int X { get; set; }
        public List<string> Connections { get; set; } = new List<string>();
    }
}