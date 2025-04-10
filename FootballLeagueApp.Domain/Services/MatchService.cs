using FootballLeagueApp.Common.Exceptions.Models;
using FootballLeagueApp.DataAccess.Interfaces;
using FootballLeagueApp.Domain.Adapters;
using FootballLeagueApp.Domain.Interfaces;
using FootballLeagueApp.Domain.Models.Requests.Match;
using FootballLeagueApp.Domain.Models.Responses;
using FootballLeagueApp.Domain.Models.Responses.Match;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FootballLeagueApp.Domain.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IRankingService _rankingService;
        public MatchService(IMatchRepository matchRepository, ITeamRepository teamRepository, IRankingService rankingService)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _rankingService = rankingService;
        }

        public async Task<CreateMatchResponse> CreateMatchAsync(CreateMatchRequest request)
        {
            if (request.HomeTeamId == request.AwayTeamId)
            {
                throw new GeneralException
                   (nameof(CreateMatchAsync),
                   HttpStatusCode.NotFound,
                   "404",
                   "Home and Away teams must be different.",
                   ExceptionType.WARNING);
            }

            var homeTeam = await _teamRepository.GetTeamByIdAsync(request.HomeTeamId);
            var awayTeam = await _teamRepository.GetTeamByIdAsync(request.AwayTeamId);

            if (homeTeam == null)
            {
                throw new GeneralException
                   (nameof(UpdateMatchAsync),
                   HttpStatusCode.NotFound,
                   "404",
                   "HomeTeam is not found.",
                   ExceptionType.WARNING);
            }
            else if (awayTeam == null)
            {
                throw new GeneralException
                   (nameof(UpdateMatchAsync),
                   HttpStatusCode.NotFound,
                   "404",
                   "AwayTeam not found.",
                   ExceptionType.WARNING);
            }

            var match = request.TransformToCreateMatch();
            await _rankingService.UpdateRankingsAsync(match);
            var createMatch = await _matchRepository.CreateMatchAsync(match);

            return createMatch.TransformToCreateMatchResponse();
        }

        public async Task DeleteMatchAsync(int id)
        {
            var match = await _matchRepository.GetMatchByIdAsync(id);

            if (match == null)
            {
                throw new GeneralException
                    (nameof(DeleteMatchAsync),
                    HttpStatusCode.NotFound,
                    "404",
                    $"Match with Id {id} not found.",
                    ExceptionType.WARNING);
            }

            await _matchRepository.DeleteMatchAsync(match.Id);
        }

        public async Task<GetMatchResponse> GetMatchByIdAsync(int id)
        {
            var match = await _matchRepository.GetMatchByIdAsync(id);

            if (match is null)
            {
                throw new GeneralException
                    (nameof(GetMatchByIdAsync),
                    HttpStatusCode.NotFound,
                    "404",
                    $"Match with Id {id} not found.",
                    ExceptionType.WARNING);
            }

            return match.TransformToGetMatchResponse();
        }

        public async Task<List<GetMatchResponse>> GetMatchesAsync()
        {
            var matches = await _matchRepository.GetMathesAsync();

            return matches.TransformToGetListOfMatcResponse();
        }

        public async Task UpdateMatchAsync(UpdateMatchRequest request)
        {
            if (request.HomeTeamId == request.AwayTeamId)
            {
                throw new GeneralException
                  (nameof(UpdateMatchAsync),
                  HttpStatusCode.NotFound,
                  "404",
                  "Home and Away teams must be different.",
                  ExceptionType.WARNING);
            }

            var homeTeam = await _teamRepository.GetTeamByIdAsync(request.HomeTeamId);
            var awayTeam = await _teamRepository.GetTeamByIdAsync(request.AwayTeamId);

            if (homeTeam == null)
            {
                throw new GeneralException
                   (nameof(UpdateMatchAsync),
                   HttpStatusCode.NotFound,
                   "404",
                   "HomeTeam is not found.",
                   ExceptionType.WARNING);
            }
            else if (awayTeam == null)
            {
                throw new GeneralException
                   (nameof(UpdateMatchAsync),
                   HttpStatusCode.NotFound,
                   "404",
                   "AwayTeam not found.",
                   ExceptionType.WARNING);
            }

            var match = request.TransformToUpdateMatch();
            await _rankingService.UpdateRankingsAsync(match);
            await _matchRepository.UpdateMatchAsync(match);
        }
    }
}
