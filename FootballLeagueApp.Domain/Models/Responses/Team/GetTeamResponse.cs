namespace FootballLeagueApp.Domain.Models.Responses.Team
{
    public class GetTeamResponse
    {
        public GetTeamResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

    }
}
