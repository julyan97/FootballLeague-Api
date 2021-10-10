using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Repositories.Interfaces
{
    public interface IRepositoriesContext
    {
        IMatchRepository MatchRepository { get; }
        ITeamRepository TeamRepository { get; }
        Task SaveAsync();
    }
}
