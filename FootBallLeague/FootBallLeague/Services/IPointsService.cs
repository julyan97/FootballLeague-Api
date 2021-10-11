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
        (int pointsHomeTeam, int pointsAwaysTeam) CalculatePoints(Match match);
    }
}
