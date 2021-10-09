using FootBallLeague.Data;
using FootBallLeague.Models;
using FootBallLeague.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(FootballLeageDbContext db)
        : base(db)
        {
        }
    }
   
}
