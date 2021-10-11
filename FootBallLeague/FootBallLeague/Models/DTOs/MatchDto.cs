using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Models.DTOs
{
    public class MatchDto
    {
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public int GoalsScoredByHomeTeam { get; set; }
        public int GoalsScoredByAwayTeam { get; set; }
    }
}
