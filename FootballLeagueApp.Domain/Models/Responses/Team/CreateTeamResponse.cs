namespace FootballLeagueApp.Domain.Models.Responses.Team
{
    public class CreateTeamResponse
    {
        public CreateTeamResponse(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
