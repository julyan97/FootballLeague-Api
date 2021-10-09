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
        public MatchRepository(FootballLeageDbContext db)
        : base(db)
        {
        }

        public void CreateMatchFromDto(MatchDto match, Team[] teams)
        {
            try
            {
                Team team1 = null;
                Team team2 = null;

                if (teams.Count() == 1)
                {
                    if (teams.First().TeamName == match.Team1)
                    {
                        team1 = teams.First();
                        team2 = new Team() { TeamName = match.Team2 };

                    }
                    else
                    {
                        team2 = teams.First();
                        team1 = new Team() { TeamName = match.Team1 };
                    }
                }
                else if (teams.Count() == 0)
                {
                    team1 = new Team() { TeamName = match.Team1 };
                    team2 = new Team() { TeamName = match.Team2 };
                }
                else
                {
                    team1 = teams[0];
                    team2 = teams[1];

                }
                var scores = team1.CalculateGameOutcome(team2, match.MatchScore);
                team1.Ranking += (int)scores.team1;
                team2.Ranking += (int)scores.team2;
                var NewMatch = new Match();
                NewMatch.PlayedTeams.Add(team1);
                NewMatch.PlayedTeams.Add(team2);
                NewMatch.MatchScore = match.MatchScore;

                this.Create(NewMatch);
                
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }

}
