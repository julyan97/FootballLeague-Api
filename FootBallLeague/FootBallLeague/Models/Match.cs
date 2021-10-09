using FootBallLeague.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Models
{
    public class Match : Base<Guid>
    {
        public ICollection<Team> PlayedTeams { get; set; } = new List<Team>();
        public string MatchScore { get; set; }

    }
}
