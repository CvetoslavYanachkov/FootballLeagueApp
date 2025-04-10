using FootballLeagueApp.DataAccess.Data;
using FootballLeagueApp.Domain.Models.Responses.Ranking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeagueApp.Domain.Interfaces
{
    public interface IRankingService
    {
        Task<GetRankingResponse> GetRankingByTeamIdAsync(int teamId);
        Task<List<GetRankingResponse>> GetRankingsAsync();
        Task UpdateRankingsAsync(Match match);
    }
}
