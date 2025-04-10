using FootballLeagueApp.DataAccess.Data;
using FootballLeagueApp.Domain.Models.Responses.Ranking;
using System.Collections.Generic;

namespace FootballLeagueApp.Domain.Adapters
{
    public static class RankingAdapter
    {
        public static GetRankingResponse TransformToGetRankingResponse(this Ranking ranking)
        {
            return new GetRankingResponse
                (
                    ranking.Team.Name,
                    ranking.Points,
                    ranking.Wins,
                    ranking.Draws,
                    ranking.Loosses
                );
        }

        public static List<GetRankingResponse> TransformToGetListOfRankingResponse(this List<Ranking> listOfRanking)
        {
            var listOfTeam = new List<GetRankingResponse>();

            foreach (var ranking in listOfRanking)
            {
                listOfTeam.Add(new GetRankingResponse
                (
                    ranking.Team.Name,
                    ranking.Points,
                    ranking.Wins,
                    ranking.Draws,
                    ranking.Loosses
                ));
            }

            return listOfTeam;
        }
    }
}
