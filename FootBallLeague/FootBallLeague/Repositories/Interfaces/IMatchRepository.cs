using FootBallLeague.Models;
using FootBallLeague.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Repositories.Interfaces
{
    public interface IMatchRepository : IRepository<Match, Guid>
    {
        Task<Match> InsertMatchEntityAndUpdateTeamPointsAsync(MatchDto match);
        Task<Match> UpdateMatchEntityAndUpdateTeamPointsAsync(Guid matchId, MatchDto match);
        Task DeleteEntityAndUpdatePointsAsync(Guid matchId);
    }
}
