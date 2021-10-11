using FootBallLeague.Models;
using FootBallLeague.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Services
{
    public interface IPointsService
    {
        /// <summary>
        /// Calculates the points each team has to recive based on a match score
        /// </summary>
        /// <param name="match">MatchEntity which points have to be calculated</param>
        /// <returns> returns a tuple with points each team has to recive</returns>
        (int pointsHomeTeam, int pointsAwaysTeam) CalculatePoints(Match match);
    }
}
