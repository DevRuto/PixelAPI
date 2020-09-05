using Microsoft.EntityFrameworkCore;
using PixelAPI.Models;

namespace PixelAPI.Data
{
    public class PixelContext : DbContext
    {
        public DbSet<Map> Maps { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Replay> Replays { get; set; }
        public DbSet<Server> Servers { get; set; }

        public PixelContext(DbContextOptions<PixelContext> options) : base(options)
        {
        }
    }
}