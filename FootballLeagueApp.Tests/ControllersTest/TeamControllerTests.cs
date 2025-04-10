using FluentValidation;
using FootballLeagueApp.API.Controllers;
using FootballLeagueApp.Domain.Interfaces;
using FootballLeagueApp.Domain.Models.Requests.Team;
using FootballLeagueApp.Domain.Models.Responses.Team;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FootballLeagueApp.Tests.ControllersTest
{
    public class TeamControllerTests
    {
        private readonly Mock<ITeamService> _teamServiceMock;
        private readonly TeamController _controller;

        public TeamControllerTests()
        {
            _teamServiceMock = new Mock<ITeamService>();
            _controller = new TeamController(_teamServiceMock.Object);
        }

        private static OkObjectResult AssertOkObjectResult(IActionResult actionResult)
        {
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            return okResult;
        }

        private static ObjectResult AssertCreatedResult(IActionResult actionResult)
        {
            var createdResult = actionResult as ObjectResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
            return createdResult;
        }


        [Fact]
        public async Task Get_Team_By_Id_Async_Should_Return_Ok_When_Team_Exists()
        {
            // Arrange
            var teamId = 1;
            var expectedTeam = new GetTeamResponse(teamId, "FC Awesome");
            _teamServiceMock.Setup(s => s.GetTeamByIdAsync(teamId)).ReturnsAsync(expectedTeam);

            // Act
            var result = await _controller.GetTeamByIdAsync(teamId);
            var okResult = AssertOkObjectResult(result.Result);

            // Assert
            Assert.Equal(expectedTeam, okResult.Value);
        }

        [Fact]
        public async Task Get_Teams_Async_Should_Return_List_Of_Teams()
        {
            var expectedTeams = new List<GetTeamResponse>
            {
                new GetTeamResponse(1, "Team A"),
                new GetTeamResponse(2, "Team B")
            };

            _teamServiceMock.Setup(s => s.GetTeamsAsync()).ReturnsAsync(expectedTeams);

            var result = await _controller.GetTeamsAsync();
            var okResult = AssertOkObjectResult(result.Result);

            var value = Assert.IsType<List<GetTeamResponse>>(okResult.Value);
            Assert.Equal(expectedTeams.Count, value.Count);
        }

        [Fact]
        public async Task Get_Teams_Async_Should_Return_Empty_List_When_No_Teams_Exist()
        {
            _teamServiceMock.Setup(s => s.GetTeamsAsync()).ReturnsAsync(new List<GetTeamResponse>());

            var result = await _controller.GetTeamsAsync();
            var okResult = AssertOkObjectResult(result.Result);

            var value = Assert.IsType<List<GetTeamResponse>>(okResult.Value);
            Assert.Empty(value);
        }

        [Fact]
        public async Task Create_Team_Async_Should_Return_Created_When_Valid_Request()
        {
            var request = new CreateTeamRequest { Name = "New Team" };
            var response = new CreateTeamResponse("TeamName");

            var validator = new Mock<IValidator<CreateTeamRequest>>();
            validator.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _teamServiceMock.Setup(s => s.CreateTeamAsync(request)).ReturnsAsync(response);

            var result = await _controller.CreateTeamAsync(request, validator.Object);
            var createdResult = AssertCreatedResult(result.Result);

            Assert.Equal(response, createdResult.Value);
        }

        [Fact]
        public async Task Update_Team_Async_Should_Return_Ok_When_Valid_Request()
        {
            var request = new UpdateTeamRequest { Id = 1, Name = "Updated Team" };

            var validator = new Mock<IValidator<UpdateTeamRequest>>();
            validator.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _teamServiceMock.Setup(s => s.UpdateTeamAsync(request)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateTeamAsync(request, validator.Object);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_Team_Async_Should_Return_Ok_When_Deleted()
        {
            var teamId = 1;
            _teamServiceMock.Setup(s => s.DeleteTeamAsync(teamId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteTeamAsync(teamId);
            var okResult = AssertOkObjectResult(result);

            Assert.Equal($"Team with Id {teamId} deleted.", okResult.Value);
        }

    }
}
