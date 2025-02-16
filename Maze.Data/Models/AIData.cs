using System.ComponentModel.DataAnnotations.Schema;

namespace Maze.DataAccess.Models
{
    public class AIData
    {
        public int AIDataId { get; set; }
        public int Steps { get; set; }
        public MazeData MyMazeData { get; set; } = null!;
        [ForeignKey("MyMazeData")]
        public int MazeDataId { get; set; }
    }
}