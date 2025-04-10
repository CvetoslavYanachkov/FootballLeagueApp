namespace FootballLeagueApp.Domain.Models.Responses.Match
{
    public class CreateMatchResponse
    {
        public CreateMatchResponse( string homeTeamName, string awayTeamName, int homeScore, int awayScore)
        {
            HomeTeamName = homeTeamName;
            AwayTeamName = awayTeamName;
            HomeScore = homeScore;
            AwayScore = awayScore;
        }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }
    }
}
