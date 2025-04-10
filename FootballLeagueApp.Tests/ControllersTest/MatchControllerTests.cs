using FluentValidation;
using FootballLeagueApp.API.Controllers;
using FootballLeagueApp.Domain.Interfaces;
using FootballLeagueApp.Domain.Models.Requests.Match;
using FootballLeagueApp.Domain.Models.Responses;
using FootballLeagueApp.Domain.Models.Responses.Match;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace FootballLeagueApp.Tests.ControllersTest
{
    public class MatchControllerTests
    {
        private readonly Mock<IMatchService> _matchServiceMock;
        private readonly MatchController _controller;

        public MatchControllerTests()
        {
            _matchServiceMock = new Mock<IMatchService>();
            _controller = new MatchController(_matchServiceMock.Object);
        }

        private static OkObjectResult AssertOk(IActionResult actionResult)
        {
            var ok = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, ok.StatusCode);
            return ok;
        }

        private static ObjectResult AssertCreated(IActionResult actionResult)
        {
            var created = Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(201, created.StatusCode);
            return created;
        }

        [Fact]
        public async Task Get_Match_By_Id_Async_Should_Return_Match_When_Exists()
        {
            var matchId = 1;
            var expected = new GetMatchResponse(matchId, "home", "away", 1, 3);
            _matchServiceMock.Setup(m => m.GetMatchByIdAsync(matchId)).ReturnsAsync(expected);

            var result = await _controller.GetMatchByIdAsync(matchId);
            var ok = AssertOk(result.Result);

            var response = Assert.IsType<GetMatchResponse>(ok.Value);
            Assert.Equal(matchId, response.Id);
        }

        [Fact]
        public async Task Get_Teams_Async_Should_Return_Match_List()
        {
            var expected = new List<GetMatchResponse>
            {
                new(1, "Home1", "Away1", 3, 1),
                new(2, "Home2", "Away2", 2, 2)
            };
            _matchServiceMock.Setup(s => s.GetMatchesAsync()).ReturnsAsync(expected);

            var result = await _controller.GetTeamsAsync();
            var ok = AssertOk(result.Result);

            Assert.Equal(expected, ok.Value);
        }

        [Fact]
        public async Task Get_Teams_Async_Should_Return_Empty_List_When_None_Exist()
        {
            _matchServiceMock.Setup(s => s.GetMatchesAsync()).ReturnsAsync(new List<GetMatchResponse>());

            var result = await _controller.GetTeamsAsync();
            var ok = AssertOk(result.Result);
            var matches = Assert.IsType<List<GetMatchResponse>>(ok.Value);

            Assert.Empty(matches);
        }

        [Fact]
        public async Task Create_Match_Async_Should_Return_Created_When_Valid()
        {
            var request = new CreateMatchRequest { HomeTeamId = 1, AwayTeamId = 2, HomeScore = 2, AwayScore = 1 };
            var response = new CreateMatchResponse("HomeTeam", "AwayTeam", 1, 2);

            var validator = new Mock<IValidator<CreateMatchRequest>>();
            validator.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _matchServiceMock.Setup(s => s.CreateMatchAsync(request)).ReturnsAsync(response);

            var result = await _controller.CreateMatchAsync(request, validator.Object);
            var created = AssertCreated(result.Result);

            Assert.Equal(response, created.Value);
        }

        [Fact]
        public async Task Update_Match_Async_Should_Return_Ok_When_Valid()
        {
            var request = new UpdateMatchRequest { Id = 1, HomeScore = 2, AwayScore = 2 };

            var validator = new Mock<IValidator<UpdateMatchRequest>>();
            validator.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _matchServiceMock.Setup(s => s.UpdateMatchAsync(request)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateMatchAsync(request, validator.Object);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_Match_Async_Should_Return_Ok_When_Deleted()
        {
            var matchId = 1;
            _matchServiceMock.Setup(s => s.DeleteMatchAsync(matchId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteMatchAsync(matchId);
            var ok = AssertOk(result);

            Assert.Equal($"Match with Id {matchId} deleted.", ok.Value);
        }

    }
}
