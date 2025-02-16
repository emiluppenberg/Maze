using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.DataAccess.Models
{
    public class MazeData
    {
        public int MazeDataId { get; set; }
        public int YLength { get; set; }
        public int XLength { get; set; }
        public AIData MyAIData { get; set; } = null!;
        [ForeignKey("MyAIData")]
        public int AIDataId { get; set; }
    }
}
