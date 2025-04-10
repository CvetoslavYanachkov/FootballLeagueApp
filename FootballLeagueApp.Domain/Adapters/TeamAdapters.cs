using FootballLeagueApp.DataAccess.Data;
using FootballLeagueApp.Domain.Models.Requests.Team;
using FootballLeagueApp.Domain.Models.Responses.Team;
using System.Collections.Generic;

namespace FootballLeagueApp.Domain.Adapters
{
    public static class TeamAdapters
    {
        public static GetTeamResponse TransformToGetTeamResponse(this Team team)
        {
            return new GetTeamResponse
                (
                    team.Id,
                    team.Name
                );
        }

        public static Team TransformToCreateTeam(this CreateTeamRequest request)
        {
            return new Team
               (
                   request.Name
               );
        }

        public static CreateTeamResponse TransformToCreateTeamResponse(this Team team)
        {
            return new CreateTeamResponse
                (
                    team.Name
                );
        }

        public static Team TransformToUpdateTeam(this UpdateTeamRequest request)
        {
            return new Team
               (
                    request.Id,
                    request.Name
               );
        }

        public static List<GetTeamResponse> TransformToGetListOfTeamResponse(this List<Team> teams)
        {
            var listOfTeam = new List<GetTeamResponse>();

            foreach (var team in teams)
            {
               listOfTeam.Add(new GetTeamResponse
               (
                   team.Id,
                   team.Name
               ));
            }

            return listOfTeam;
        }
    }
}
