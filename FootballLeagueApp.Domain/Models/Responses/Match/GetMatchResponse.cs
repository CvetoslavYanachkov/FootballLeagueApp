using System.Data;

namespace FootballLeagueApp.Domain.Models.Responses
{
    public class GetMatchResponse
    {
        public GetMatchResponse(int id, string homeTeamName, string awayTeamName, int homeScore, int awayScore)
        {
            Id = id;
            HomeTeamName = homeTeamName;
            AwayTeamName = awayTeamName;
            HomeScore = homeScore;
            AwayScore = awayScore;
        }

        public int Id { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

    }
}
