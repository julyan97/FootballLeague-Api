using FootBallLeague.Models;
using FootBallLeague.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Repositories.Interfaces
{
    public interface IMatchRepository : IRepository<Match>
    {
        Task CreateMatchFromDto(MatchDto match);
    }
}
