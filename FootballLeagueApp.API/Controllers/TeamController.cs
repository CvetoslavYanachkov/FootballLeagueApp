using FluentValidation;
using FootballLeagueApp.Domain.Interfaces;
using FootballLeagueApp.Domain.Models.Requests.Team;
using FootballLeagueApp.Domain.Models.Responses.Team;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FootballLeagueApp.API.Controllers
{
    /// <summary>
    /// Responsible for CRUD operations on teams.
    /// </summary>
    /// 
    public class TeamController : BaseController
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        /// <summary>
        /// Get team by Id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("get-team")]
        [ProducesResponseType(typeof(GetTeamResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetTeamResponse>> GetTeamByIdAsync([FromQuery][Required] int id)
        {
            var response = await _teamService.GetTeamByIdAsync(id);

            return Ok(response);
        }

        /// <summary>
        /// Get list of all teams.
        /// </summary>
        [HttpGet("get-teams")]
        [ProducesResponseType(typeof(List<GetTeamResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetTeamResponse>>> GetTeamsAsync()
        {
            var response = await _teamService.GetTeamsAsync();

            return Ok(response);
        }

        /// <summary>
        /// Create a new team.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="validator"></param>
        [HttpPost("create-team")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CreateTeamResponse>> CreateTeamAsync([FromBody] CreateTeamRequest request, [FromServices] IValidator<CreateTeamRequest> validator)
        {
            await validator.ValidateAndThrowAsync(request);
            var response = await _teamService.CreateTeamAsync(request);

            return StatusCode(StatusCodes.Status201Created, response);
        }

        /// <summary>
        /// Update a team.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="validator"></param>
        [HttpPut("update-team")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateTeamAsync([FromBody] UpdateTeamRequest request, [FromServices] IValidator<UpdateTeamRequest> validator)
        {
            await validator.ValidateAndThrowAsync(request);
            await _teamService.UpdateTeamAsync(request);

            return Ok();
        }

        /// <summary>
        /// Delete a team by Id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("delete-team")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteTeamAsync([FromQuery][Required] int id)
        {
            await _teamService.DeleteTeamAsync(id);

            return Ok($"Team with Id {id} deleted.");
        }
    }
}
