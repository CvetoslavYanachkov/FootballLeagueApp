using FootballLeagueApp.DataAccess.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeagueApp.DataAccess.Interfaces
{
    public interface IRankingRepository
    {
        Task EnsureRankingExistsAsync(int teamId);
        Task<Ranking> GetByTeamIdAsync(int teamId);
        Task<List<Ranking>> GetListOfRankingAsync();
        Task SaveChangesAsync();
    }
}
