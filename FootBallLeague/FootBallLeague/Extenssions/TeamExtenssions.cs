using FootBallLeague.Data;
using FootBallLeague.Enums;
using FootBallLeague.Models;
using FootBallLeague.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Extenssions
{
    public static class TeamExtenssions
    {
        public static (Score team1, Score team2) CalculateGameOutcome(this Team team1, Team team2, string score)
        {
            var scores = score
                .Split("-")
                .Select(x => int.Parse(x))
                .ToArray();
            var team1Score = scores[0];
            var Team2Score = scores[1];

            if (team1Score > Team2Score) return (Score.Won, Score.Loss);
            else if (team1Score == Team2Score) return (Score.Draw, Score.Draw);

            return (Score.Loss, Score.Won);
        }

        public static void UpdateGameOutcome(this Team team1, Team team2, string prevScore, string newScore)
        {
            var prevScores = CalculateGameOutcome(team1, team2, prevScore);
            var newScores = CalculateGameOutcome(team1, team2, newScore);

            var addToTeam1 = newScores.team1 - prevScores.team1;
            var addToTeam2 = newScores.team2 - prevScores.team2;

         //   team1.Ranking += addToTeam1;
         //  team2.Ranking += addToTeam2;
        }
    }
}
