using FootballLeagueApp.Common.Exceptions.Models;
using FootballLeagueApp.DataAccess.Interfaces;
using FootballLeagueApp.Domain.Adapters;
using FootballLeagueApp.Domain.Interfaces;
using FootballLeagueApp.Domain.Models.Requests.Team;
using FootballLeagueApp.Domain.Models.Responses.Team;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FootballLeagueApp.Domain.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<CreateTeamResponse> CreateTeamAsync(CreateTeamRequest request)
        {
            var team = request.TransformToCreateTeam();
            var createTeam = await _teamRepository.CreateTeamAsync(team);

            return createTeam.TransformToCreateTeamResponse();
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);

            if (team is null)
            {
                throw new GeneralException
                    (nameof(DeleteTeamAsync),
                    HttpStatusCode.NotFound,
                    "404",
                    $"Team with Id {id} not found.",
                    ExceptionType.WARNING);
            }

            await _teamRepository.DeleteTeamAsync(team.Id);
        }


        public async Task<GetTeamResponse> GetTeamByIdAsync(int id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);

            if (team is null)
            {
                throw new GeneralException
                    (nameof(GetTeamByIdAsync),
                    HttpStatusCode.NotFound,
                    "404",
                    $"Team with Id {id} not found.",
                    ExceptionType.WARNING);
            }

            return team.TransformToGetTeamResponse();
        }

        public async Task<List<GetTeamResponse>> GetTeamsAsync()
        {
            var teams = await _teamRepository.GetTeamsAsync();

            return teams.TransformToGetListOfTeamResponse();
        }

        public async Task UpdateTeamAsync(UpdateTeamRequest teamRequest)
        {
            var team = await _teamRepository.GetTeamByIdAsync(teamRequest.Id);

            if (team is null)
            {
                throw new GeneralException
                    (nameof(UpdateTeamAsync),
                    HttpStatusCode.NotFound,
                    "404",
                    $"Team with Id {teamRequest.Id} not found.",
                    ExceptionType.WARNING);
            }

            var updateTeam = teamRequest.TransformToUpdateTeam();
            await _teamRepository.UpdateTeamAsync(updateTeam);
        }
    }
}
