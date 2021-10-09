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
    }
}
