using FluentValidation;
using FootballLeagueApp.Domain.Interfaces;
using FootballLeagueApp.Domain.Models.Requests.Match;
using FootballLeagueApp.Domain.Models.Responses;
using FootballLeagueApp.Domain.Models.Responses.Match;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FootballLeagueApp.API.Controllers
{
    /// <summary>
    /// Responsible for CRUD operations on matches.
    /// </summary>
    public class MatchController : BaseController
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        /// <summary>
        /// Retrieves a match by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Match details if found.</returns>
        [HttpGet("get-match")]
        [ProducesResponseType(typeof(GetMatchResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetMatchResponse>> GetMatchByIdAsync([FromQuery][Required] int id)
        {
            var response = await _matchService.GetMatchByIdAsync(id);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a list of all played matches.
        /// </summary>
        /// <returns>List of matches with home and away team details.</returns>
        [HttpGet("get-matches")]
        [ProducesResponseType(typeof(List<GetMatchResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetMatchResponse>>> GetTeamsAsync()
        {
            var response = await _matchService.GetMatchesAsync();

            return Ok(response);
        }

        /// <summary>
        /// Creates a new match.
        /// </summary>
        /// <param name="request">The match creation request.</param>
        /// <param name="validator"></param>
        [HttpPost("create-match")]
        [ProducesResponseType(StatusCodes.Status201Created)]
   

        public async Task<ActionResult<CreateMatchResponse>> CreateMatchAsync(CreateMatchRequest request, [FromServices] IValidator<CreateMatchRequest> validator)
        {
            await validator.ValidateAndThrowAsync(request);
            var response = await _matchService.CreateMatchAsync(request);

            return StatusCode(StatusCodes.Status201Created, response);
        }

        /// <summary>
        /// Updates an existing match.
        /// </summary>
        /// <param name="request">The match update request.</param>
        /// <param name="validator"></param>
        [HttpPut("update-match")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateMatchAsync(UpdateMatchRequest request, [FromServices] IValidator<UpdateMatchRequest> validator)
        {
            await validator.ValidateAndThrowAsync(request);
            await _matchService.UpdateMatchAsync(request);

            return Ok();
        }

        /// <summary>
        /// Deletes a match by its Id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("delete-match")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteMatchAsync([FromQuery][Required] int id)
        {
            await _matchService.DeleteMatchAsync(id);

            return Ok($"Match with Id {id} deleted.");
        }
    }
}
