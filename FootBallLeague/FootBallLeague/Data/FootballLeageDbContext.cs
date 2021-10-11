using FootBallLeague.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Data
{
    public class FootballLeageDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }

        public FootballLeageDbContext(DbContextOptions<FootballLeageDbContext> options)
    : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(x=>
            {
                x.HasMany(m => m.PlayedMatchesAsAwayTeam)
                .WithOne(al => al.AwayTeam)
                .OnDelete(DeleteBehavior.NoAction);

                x.HasMany(m => m.PlayedMatchesAsHomeTeam)
                .WithOne(ht => ht.HomeTeam)
                .OnDelete(DeleteBehavior.NoAction);
            
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
