using FootBallLeague.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FootBallLeague.Models
{
    public class Team : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public int Points { get; set; }
        [JsonIgnore]
        public ICollection<Match> PlayedMatchesAsHomeTeam { get; set; }
        [JsonIgnore]
        public ICollection<Match> PlayedMatchesAsAwayTeam { get; set; }
    }
}
