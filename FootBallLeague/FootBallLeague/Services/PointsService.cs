using FootBallLeague.Models;
using FootBallLeague.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Services
{
    public class PointsService : IPointsService
    {
        public static readonly int PointsAwardedForWin = 3;
        public static readonly int PointsAwardedForLoss = 0;
        public static readonly int PointsAwardedForDraw = 1;
        public (int pointsHomeTeam, int pointsAwaysTeam) CalculatePoints(Match match)
        {
            if (match.GoalsScoredByHomeTeam == match.GoalsScoredByAwayTeam)
                return (PointsAwardedForDraw, PointsAwardedForDraw);
            else if (match.GoalsScoredByHomeTeam > match.GoalsScoredByAwayTeam)
                return (PointsAwardedForWin, PointsAwardedForLoss);
            else
                return (PointsAwardedForLoss, PointsAwardedForWin);
        }
    }
}
