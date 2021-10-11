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
        /// <summary>
        /// Create new MatchEntity and Update the points of the teams based on the goals scored
        /// </summary>
        /// <param name="match">Dto to repesent the Match we want to create</param>
        /// <returns> returns the created match </returns>
        Task<Match> InsertMatchEntityAndUpdateTeamPointsAsync(MatchDto match);

        /// <summary>
        /// Updates the match entity and the team points of each team 
        /// </summary>
        /// <param name="matchId">the id of the match we want to update</param>
        /// <param name="match">Dto with the data we want to update</param>
        /// <returns> returns the match we have Updated</returns>
        Task<Match> UpdateMatchEntityAndUpdateTeamPointsAsync(Guid matchId, MatchDto match);

        /// <summary>
        /// Deletes the matchEntity and reverts all the points before the match has existed
        /// </summary>
        /// <param name="matchId">Id of the match we want to delete</param>
        Task DeleteEntityAndUpdatePointsAsync(Guid matchId);
    }
}
