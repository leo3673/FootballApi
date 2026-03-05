using FootballApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Club> clubs { get; set; }
        public DbSet<Coach> coaches { get; set; }
        public DbSet<Facility> facilities { get; set; }
        public DbSet<League> leagues{ get; set; }
        public DbSet<Match> matches{ get; set; }
        public DbSet<Player> players{ get; set; }
        public DbSet<PlayerSeasonStats> playerSeasonStats { get; set; }
    }
}
