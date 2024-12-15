using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.DataAccess.Models;

namespace Maze.DataAccess.Data
{
    public class MazeDbContext : DbContext
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Maze;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public DbSet<MazeData> MazeData { get; set; } = null!;
        public DbSet<AIData> AIs { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MazeData>()
                .HasOne(md => md.MyAIData)
                .WithOne(ad => ad.MyMazeData)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
