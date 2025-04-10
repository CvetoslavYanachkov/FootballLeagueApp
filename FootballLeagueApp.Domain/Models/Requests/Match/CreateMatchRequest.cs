﻿namespace FootballLeagueApp.Domain.Models.Requests.Match
{
    public class CreateMatchRequest
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
