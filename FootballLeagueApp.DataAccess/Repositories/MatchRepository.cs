using FootballLeagueApp.Common.Exceptions.Models;
using FootballLeagueApp.DataAccess.Contexts;
using FootballLeagueApp.DataAccess.Data;
using FootballLeagueApp.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FootballLeagueApp.DataAccess.DbServices
{
    public class MatchRepository : IMatchRepository
    {
        private readonly AppDbContext _appDbContext;

        public MatchRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Match> CreateMatchAsync(Match match)
        {
            try
            {
                await _appDbContext.Matches.AddAsync(match);
                await _appDbContext.SaveChangesAsync();

                return match;
            }
            catch (Exception ex)
            {
                throw new GeneralException
                    (nameof(GetMatchByIdAsync),
                    HttpStatusCode.BadRequest,
                    "400",
                    ex.Message,
                    ExceptionType.ERROR);
            }
        }

        public async Task DeleteMatchAsync(int id)
        {
            var match = await _appDbContext.Matches.FindAsync(id);
            if (match != null)
            {
                _appDbContext.Matches.Remove(match);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Match> GetMatchByIdAsync(int id)
        {
            var match = await _appDbContext.Matches
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .FirstOrDefaultAsync(m => m.Id == id);

            return match;
        }

        public async Task<List<Match>> GetMathesAsync()
        {
            return await _appDbContext.Matches
                .AsNoTracking()
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToListAsync();
        }

        public async Task<List<int>> GetTeamIdsAsync()
        {
            var homeTeamIds = _appDbContext.Matches.Select(m => m.HomeTeamId);
            var awayTeamIds = _appDbContext.Matches.Select(m => m.AwayTeamId);

            return await homeTeamIds
                .Union(awayTeamIds)
                .Distinct()
                .ToListAsync();
        }

        public async Task UpdateMatchAsync(Match match)
        {
            var existingMatch = await _appDbContext.Matches
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .FirstOrDefaultAsync(m => m.Id == match.Id);

            if (existingMatch != null)
            {
                existingMatch.Id = match.Id;
                existingMatch.HomeTeamId = match.HomeTeamId;
                existingMatch.AwayTeamId = match.AwayTeamId;
                existingMatch.HomeScore = match.HomeScore;
                existingMatch.AwayScore = match.AwayScore;

                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
