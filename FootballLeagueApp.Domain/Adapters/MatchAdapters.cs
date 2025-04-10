using FootballLeagueApp.DataAccess.Data;
using FootballLeagueApp.Domain.Models.Requests.Match;
using FootballLeagueApp.Domain.Models.Responses;
using FootballLeagueApp.Domain.Models.Responses.Match;
using System.Collections.Generic;

namespace FootballLeagueApp.Domain.Adapters
{
    public static class MatchAdapters
    {
        public static GetMatchResponse TransformToGetMatchResponse(this Match match)
        {
            return new GetMatchResponse
                (
                    match.Id,
                    match.HomeTeam.Name,
                    match.AwayTeam.Name,
                    match.HomeScore,
                    match.AwayScore
                );
        }

        public static Match TransformToCreateMatch(this CreateMatchRequest request)
        {
            return new Match
               (
                   request.HomeTeamId,
                   request.AwayTeamId,
                   request.HomeScore,
                   request.AwayScore
               );

        }

        public static Match TransformToUpdateMatch(this UpdateMatchRequest request)
        {
            return new Match
               (
                   request.Id,
                   request.HomeTeamId,
                   request.AwayTeamId,
                   request.HomeScore,
                   request.AwayScore
               );
        }

        public static List<GetMatchResponse> TransformToGetListOfMatcResponse(this List<Match> matches)
        {
            var listOfMatches = new List<GetMatchResponse>();
            foreach (var match in matches)
            {
                listOfMatches.Add(match.TransformToGetMatchResponse());
            }
            return listOfMatches;
        }

        public static CreateMatchResponse TransformToCreateMatchResponse(this Match match)
        {
            return new CreateMatchResponse
                (
                    match.HomeTeam.Name,
                    match.AwayTeam.Name,
                    match.HomeScore,
                    match.AwayScore
                );
        }

    }
}
