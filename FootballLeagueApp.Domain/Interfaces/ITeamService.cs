using FootballLeagueApp.Domain.Models.Requests.Team;
using FootballLeagueApp.Domain.Models.Responses.Team;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeagueApp.Domain.Interfaces
{
    public interface ITeamService
    {
        Task<List<GetTeamResponse>> GetTeamsAsync();

        Task<GetTeamResponse> GetTeamByIdAsync(int id);

        Task<CreateTeamResponse> CreateTeamAsync(CreateTeamRequest team);

        Task UpdateTeamAsync(UpdateTeamRequest team);

        Task DeleteTeamAsync(int id);
    }
}
