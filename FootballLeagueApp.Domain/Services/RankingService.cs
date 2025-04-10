using FootballLeagueApp.Common.Exceptions.Models;
using FootballLeagueApp.DataAccess.Data;
using FootballLeagueApp.DataAccess.Interfaces;
using FootballLeagueApp.Domain.Adapters;
using FootballLeagueApp.Domain.Interfaces;
using FootballLeagueApp.Domain.Models.Responses.Ranking;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FootballLeagueApp.Domain.Services
{
    public class RankingService : IRankingService
    {
        private readonly IRankingRepository _rankingRepository;
        public RankingService(IRankingRepository rankingRepository)
        {
            _rankingRepository = rankingRepository;
        }

        public async Task<GetRankingResponse> GetRankingByTeamIdAsync(int teamId)
        {
            var ranking = await _rankingRepository.GetByTeamIdAsync(teamId);

            if (ranking is null)
            {
                throw new GeneralException
                    (nameof(GetRankingByTeamIdAsync),
                    HttpStatusCode.NotFound,
                    "404",
                    $"Team with team id {teamId} not found.",
                    ExceptionType.WARNING);
            }

            return ranking.TransformToGetRankingResponse();
        }

        public async Task<List<GetRankingResponse>> GetRankingsAsync()
        {
            var listOfRanking = await _rankingRepository.GetListOfRankingAsync();

            return listOfRanking.TransformToGetListOfRankingResponse();
        }

        public async Task UpdateRankingsAsync(Match match)
        {
            await _rankingRepository.EnsureRankingExistsAsync(match.HomeTeamId);
            await _rankingRepository.EnsureRankingExistsAsync(match.AwayTeamId);

            var homeRanking = await _rankingRepository.GetByTeamIdAsync(match.HomeTeamId);
            var awayRanking = await _rankingRepository.GetByTeamIdAsync(match.AwayTeamId);

            if (match.HomeScore > match.AwayScore)
            {
                homeRanking.Points += 3;
                homeRanking.Wins++;
                awayRanking.Loosses++;
            }
            else if (match.HomeScore < match.AwayScore)
            {
                awayRanking.Points += 3;
                awayRanking.Wins++;
                homeRanking.Loosses++;
            }
            else
            {
                homeRanking.Points += 1;
                awayRanking.Points += 1;
                homeRanking.Draws++;
                awayRanking.Draws++;
            }

            await _rankingRepository.SaveChangesAsync();
        }
    }
}
