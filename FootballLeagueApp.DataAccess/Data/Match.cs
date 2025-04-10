namespace FootballLeagueApp.DataAccess.Data
{
    public class Match
    {
        public Match(int id, int homeTeamId, int awayTeamId, int homeScore, int awayScore)
        {
            Id = id;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            HomeScore = homeScore;
            AwayScore = awayScore;
        }

        public Match(Team homeTeam, Team awayTeam, int homeScore, int awayScore)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeScore = homeScore;
            AwayScore = awayScore;
        }

        public Match(int homeTeamId, int awayTeamId, int homeScore, int awayScore)
        {
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            HomeScore = homeScore;
            AwayScore = awayScore;
        }

        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
