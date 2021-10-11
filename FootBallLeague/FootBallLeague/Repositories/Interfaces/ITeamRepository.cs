using FootBallLeague.Models;
using FootBallLeague.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Repositories.Interfaces
{
    public interface ITeamRepository : IRepository<Team, Guid>
    {
        /// <summary>
        /// Updates the requested Team by the given id with the data from the Dto
        /// </summary>
        /// <param name="id">Id of the Team we want to Update</param>
        /// <param name="team">Dto with the data we want to update</param>
        /// <returns>returns the updated team</returns>
        Task<Team> UpdateTeamAsync(Guid id, TeamDto team);

        /// <summary>
        /// Deletes a Team by id
        /// </summary>
        /// <param name="teamId">id of the Team we want to delete</param>
        Task DeleteTeamAsync(Guid teamId);
    }
}
