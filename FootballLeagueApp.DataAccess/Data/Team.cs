using System.Collections.Generic;

namespace FootballLeagueApp.DataAccess.Data
{
    public class Team
    {
        public Team(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Team(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
