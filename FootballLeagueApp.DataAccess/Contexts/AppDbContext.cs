using FootballLeagueApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballLeagueApp.DataAccess.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Data.Match> Matches { get; set; }
        public DbSet<Ranking> Rankings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Team>()
              .HasKey(t => t.Id);

            modelBuilder.Entity<Team>()
               .Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(100);

            modelBuilder.Entity<Data.Match>()
               .HasOne(m => m.HomeTeam)
               .WithMany()
               .HasForeignKey(m => m.HomeTeamId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Data.Match>()
               .HasOne(m => m.AwayTeam)
               .WithMany()
               .HasForeignKey(m => m.AwayTeamId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Data.Match>()
             .HasKey(m => m.Id);

            modelBuilder.Entity<Ranking>()
                .HasKey(r => r.TeamId);

            modelBuilder.Entity<Ranking>()
                .HasOne(r => r.Team)
                .WithOne()
                .HasForeignKey<Ranking>(r => r.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
