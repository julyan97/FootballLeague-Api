using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Models.DTOs
{
    public class TeamDto
    {
        public string TeamName { get; set; }
        public double Ranking { get; set; }

        public Team CreateTeam()
        {
            return new Team() { TeamName = TeamName, Ranking = Ranking };
        }
    }
}
