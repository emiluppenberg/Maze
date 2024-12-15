using Maze.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Maze.DataAccess.Data
{
    public class MazeDataRepository : GenericRepository<MazeData>
    {
        public MazeDataRepository(MazeDbContext context) : base(context)
        {
        }

        public List<MazeSize> AllMazeSizes()
        {
            var groups = from md in context.MazeData
                         group md by new { md.YLength, md.XLength } into mdGroup
                         select new MazeSize
                         {
                             YLength = mdGroup.Key.YLength,
                             XLength = mdGroup.Key.XLength,
                         };

            return groups.ToList();
        }
        public List<MazeData> GetAllByMazeSize(int yLength, int xLength)
        {
            return context.MazeData.Where
                (md => md.YLength == yLength && md.XLength == xLength)
                .Include(md => md.MyAIData)
                .ToList();
        }
    }
}
