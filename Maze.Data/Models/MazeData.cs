using System;
using System.Collections.Generic;
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
        public int AIId { get; set; }
        public AI MyAI { get; set; }
    }
}
