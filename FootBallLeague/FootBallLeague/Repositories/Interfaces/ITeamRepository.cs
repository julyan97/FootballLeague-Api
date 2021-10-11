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
        Task<Team> UpdateTeamAsync(Guid id, TeamDto team);
        Task DeleteTeamAsync(Guid teamId);
    }
}
