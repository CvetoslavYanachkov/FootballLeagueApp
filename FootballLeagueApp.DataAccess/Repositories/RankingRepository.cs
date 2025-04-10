using FootballLeagueApp.DataAccess.Contexts;
using FootballLeagueApp.DataAccess.Data;
using FootballLeagueApp.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueApp.DataAccess.Repositories
{
    public class RankingRepository : IRankingRepository
    {
        private readonly AppDbContext _appDbContext;
        public RankingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task EnsureRankingExistsAsync(int teamId)
        {
            try
            {
                var exists = await _appDbContext.Rankings.AnyAsync(r => r.TeamId == teamId);

                if (!exists)
                {
                    _appDbContext.Rankings.Add(new Ranking
                    {
                        TeamId = teamId,
                        Points = 0,
                        Wins = 0,
                        Draws = 0,
                        Loosses = 0
                    });

                    await _appDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //_logger.LogError(ex, $"Error in EnsureRankingExistsAsync for TeamId: {teamId}");
                throw;
            }

        }

        public async Task<Ranking> GetByTeamIdAsync(int teamId)
        {
            try
            {
                return await _appDbContext.Rankings
                    .Include(r => r.Team)
                    .FirstOrDefaultAsync(r => r.TeamId == teamId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //_logger.LogError(ex, $"Error in GetByTeamIdAsync for TeamId: {teamId}");
                throw;
            }
        }

        public async Task<List<Ranking>> GetListOfRankingAsync()
        {
            return await _appDbContext.Rankings
                .AsNoTracking()
                .Include(r => r.Team)
                .OrderByDescending(r => r.Points)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
