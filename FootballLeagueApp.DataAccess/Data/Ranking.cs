using System.ComponentModel.DataAnnotations;

namespace FootballLeagueApp.DataAccess.Data
{
    public class Ranking
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loosses { get; set; }
    }
}
