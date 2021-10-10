using FootBallLeague.Data;
using FootBallLeague.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Repositories
{
    public class RepositoriesContext : IRepositoriesContext
    {
        private readonly FootballLeageDbContext db;

        public RepositoriesContext(
            FootballLeageDbContext db,
            IMatchRepository matchRepository,
            ITeamRepository teamRepository)
        {
            this.db = db;
            MatchRepository = matchRepository;
            TeamRepository = teamRepository;
        }
        public IMatchRepository MatchRepository { get; private set; }

        public ITeamRepository TeamRepository { get; private set; }

        public async Task SaveAsync()
        {
            
            await db.SaveChangesAsync();
        }
    }
}
