using FootBallLeague.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FootBallLeague.Models
{
    public class Team : Base<Guid>
    {
        public string TeamName { get; set; }
        public double Ranking { get; set; }
        [JsonIgnore]
        public ICollection<Match> PlayedMatches { get; set; } = new List<Match>();

        public Team()
        {

        }
        public Team(Team toCopy)
        {
            this.Ranking = toCopy.Ranking;
            this.PlayedMatches = this.PlayedMatches;
        }

        public void Update(TeamDto toUpdate)
        {
            TeamName = toUpdate.TeamName;
            Ranking = toUpdate.Ranking;
        }

        public void Update(string name, double ranking)
        {
            TeamName = name;
            Ranking = ranking;
        }
    }
}
