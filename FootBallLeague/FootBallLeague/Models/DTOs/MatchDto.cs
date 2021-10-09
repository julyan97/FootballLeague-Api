using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Models.DTOs
{
    public class MatchDto
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string MatchScore { get; set; }
    }
}
