namespace Maze
{
    public class MazePoint
    {
        public int Y { get; set; }
        public int X { get; set; }
        public string[] Directions { get; set; } = ["", "", "", ""];
    }
}