using FootballLeagueApp.Domain.Interfaces;
using FootballLeagueApp.Domain.Models.Responses.Ranking;
using FootballLeagueApp.Domain.Models.Responses.Team;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FootballLeagueApp.API.Controllers
{
    /// <summary>
    /// API endpoints to retrieve team rankings.
    /// </summary>
    public class RankingController : BaseController
    {
        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            _rankingService = rankingService;
        }

        /// <summary>
        /// Get the ranking of a specific team by team ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Team ranking information</returns>
        [HttpGet("get-ranking")]
        [ProducesResponseType(typeof(GetRankingResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetRankingResponse>> GetRankingByIdAsync([FromQuery][Required] int id)
        {
            var response = await _rankingService.GetRankingByTeamIdAsync(id);

            return Ok(response);
        }

        /// <summary>
        /// Get the current ranking list of all teams.
        /// </summary>
        /// <returns>List of all teams with rankings</returns>
        [HttpGet("get-list-ranking")]
        [ProducesResponseType(typeof(List<GetRankingResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetTeamResponse>> GetRankingsAsync()
        {
            var response = await _rankingService.GetRankingsAsync();

            return response.Count > 0 ?
                Ok(response) :
                NotFound("No rankings available!");
        }
    }
}
