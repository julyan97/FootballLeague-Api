using FootBallLeague.Data;
using FootBallLeague.Models;
using FootBallLeague.Models.DTOs;
using FootBallLeague.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Repositories
{
    public class TeamRepository : Repository<Team, Guid>, ITeamRepository
    {
        public TeamRepository(FootballLeageDbContext db)
        : base(db)
        {
        }

        public async Task<Team> UpdateTeamAsync(Guid teamId, TeamDto team)
        {
            var existingTeamEntity = await TryFindEntityByIdAsync(teamId,true);

            var newTeamEntity = new Team
            {
                Id = teamId,
                Name = team.TeamName,
                Points = team.Points
            };

            await UpdateAsync(newTeamEntity);

            return newTeamEntity;
        }

        public async Task DeleteTeamAsync(Guid teamId)
        {
            var existingTeamEntity = await TryFindEntityByIdAsync(teamId,true);
            await DeleteAsync(existingTeamEntity);
        }


    }

}
