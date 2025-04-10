using FootballLeagueApp.DataAccess.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeagueApp.DataAccess.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetTeamsAsync();

        Task<Team> GetTeamByIdAsync(int id);

        Task<Team> CreateTeamAsync(Team team);

        Task UpdateTeamAsync(Team team);

        Task DeleteTeamAsync(int id);
    }
}
