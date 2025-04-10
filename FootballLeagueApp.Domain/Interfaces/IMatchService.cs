using FootballLeagueApp.Domain.Models.Requests.Match;
using FootballLeagueApp.Domain.Models.Responses;
using FootballLeagueApp.Domain.Models.Responses.Match;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeagueApp.Domain.Interfaces
{
    public interface IMatchService
    {
        Task<List<GetMatchResponse>> GetMatchesAsync();

        Task<GetMatchResponse> GetMatchByIdAsync(int id);

        Task<CreateMatchResponse> CreateMatchAsync(CreateMatchRequest match);

        Task UpdateMatchAsync(UpdateMatchRequest match);

        Task DeleteMatchAsync(int id);
    }
}
