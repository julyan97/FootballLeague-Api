using FootBallLeague.Data;
using FootBallLeague.Extenssions;
using FootBallLeague.Models;
using FootBallLeague.Models.DTOs;
using FootBallLeague.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FootBallLeague.Repositories
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        private readonly FootballLeageDbContext db;
        private readonly ITeamRepository teamRepository;

        public MatchRepository(FootballLeageDbContext db, ITeamRepository teamRepository)
        : base(db)
        {
            this.db = db;
            this.teamRepository = teamRepository;
        }

        public async Task CreateMatchFromDto(MatchDto match)
        {
            var team1 = (await teamRepository
                .FindByConditionAsync(x => x.TeamName == match.Team1))
                .FirstOrDefault();

            var team2 = (await teamRepository
                .FindByConditionAsync(x => x.TeamName == match.Team1))
                .FirstOrDefault();

            team1 ??= new Team() { TeamName = team1.TeamName };
            team2 ??= new Team() { TeamName = team2.TeamName };

            var scores = team1.CalculateGameOutcome(team2, match.MatchScore);
            team1.Ranking += (int)scores.team1;
            team2.Ranking += (int)scores.team2;
            var NewMatch = new Match();
            NewMatch.PlayedTeams.Add(team1);
            NewMatch.PlayedTeams.Add(team2);
            NewMatch.MatchScore = match.MatchScore;

            await this.CreateAsync(NewMatch);
        }
    }

}
