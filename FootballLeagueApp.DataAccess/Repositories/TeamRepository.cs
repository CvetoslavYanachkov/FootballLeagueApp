using FootballLeagueApp.Common.Exceptions.Models;
using FootballLeagueApp.DataAccess.Contexts;
using FootballLeagueApp.DataAccess.Data;
using FootballLeagueApp.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FootballLeagueApp.DataAccess.DbServices
{
    public class teamRepository : ITeamRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMatchRepository _matchRepository;

        public teamRepository(AppDbContext appDbContext, IMatchRepository matchRepository)
        {
            _appDbContext = appDbContext;
            _matchRepository = matchRepository;
        }

        public async Task<Team> CreateTeamAsync(Team team)
        {
            try
            {
                await _appDbContext.Teams.AddAsync(team);
                await _appDbContext.SaveChangesAsync();

                return team;
            }
            catch (Exception ex)
            {
                throw new GeneralException
                    (nameof(CreateTeamAsync),
                    HttpStatusCode.BadRequest,
                    "400",
                    ex.Message,
                    ExceptionType.ERROR);
            }
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _appDbContext.Teams.FindAsync(id);
            var matches = await _matchRepository.GetTeamIdsAsync();
            if (team != null)
            {
                if (matches.Contains(id))
                {
                    throw new GeneralException
                    (nameof(CreateTeamAsync),
                    HttpStatusCode.BadRequest,
                    "400",
                    $"Cannot delete team with Id {id} because it's used in one or more matches.",
                    ExceptionType.ERROR);
                }

                _appDbContext.Teams.Remove(team);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            var team = await _appDbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);

            return team;
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            return await _appDbContext.Teams.AsNoTracking().ToListAsync();
        }

        public async Task UpdateTeamAsync(Team team)
        {
            var oldTeam = await _appDbContext.Teams.FirstOrDefaultAsync(t => t.Id == team.Id);

            if (team != null)
            {
                oldTeam.Name = team.Name;
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
