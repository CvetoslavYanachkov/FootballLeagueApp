namespace FootballLeagueApp.Domain.Models.Responses.Ranking
{
    public class GetRankingResponse
    {
        public GetRankingResponse(string teamName, int points, int wins, int draws, int loosses)
        {
            TeamName = teamName;
            Points = points;
            Wins = wins;
            Draws = draws;
            Loosses = loosses;
        }

        public string TeamName { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loosses { get; set; }
    }
}
