using FootballLeagueApp.DataAccess.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeagueApp.DataAccess.Interfaces
{
    public interface IMatchRepository
    {
        Task<List<Match>> GetMathesAsync();

        Task<Match> GetMatchByIdAsync(int id);

        Task<Match> CreateMatchAsync(Match match);

        Task UpdateMatchAsync(Match match);

        Task<List<int>> GetTeamIdsAsync();

        Task DeleteMatchAsync(int id);
    }
}
