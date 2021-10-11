using FootBallLeague.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Models
{
    public class Match : BaseEntity<Guid>
    {
        public Guid HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public Guid AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public int GoalsScoredByHomeTeam { get; set; }
        public int GoalsScoredByAwayTeam { get; set; }

    }
}
