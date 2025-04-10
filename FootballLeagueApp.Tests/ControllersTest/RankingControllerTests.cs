using FootballLeagueApp.API.Controllers;
using FootballLeagueApp.Domain.Interfaces;
using FootballLeagueApp.Domain.Models.Responses.Ranking;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FootballLeagueApp.Tests.ControllersTest
{
    public class RankingControllerTests
    {
        private readonly Mock<IRankingService> _rankingServiceMock;
        private readonly RankingController _controller;

        public RankingControllerTests()
        {
            _rankingServiceMock = new Mock<IRankingService>();
            _controller = new RankingController(_rankingServiceMock.Object);
        }

        private static OkObjectResult AssertOk(IActionResult actionResult)
        {
            var ok = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, ok.StatusCode);
            return ok;
        }

        private static NotFoundObjectResult AssertNotFound(IActionResult actionResult)
        {
            var notFound = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal(404, notFound.StatusCode);
            return notFound;
        }

        [Fact]
        public async Task Get_Ranking_By_Id_Async_Should_Return_Ok_When_Ranking_Exists()
        {
            var teamId = 1;
            var expected = new GetRankingResponse("TeamName", 15, 5, 0, 3);

            _rankingServiceMock.Setup(s => s.GetRankingByTeamIdAsync(teamId))
                               .ReturnsAsync(expected);

            var result = await _controller.GetRankingByIdAsync(teamId);
            var ok = AssertOk(result.Result);

            Assert.Equal(expected, ok.Value);
        }

        [Fact]
        public async Task Get_Rankings_Async_Should_Return_Ok_When_Rankings_Exist()
        {
            var rankings = new List<GetRankingResponse>
            {
                new("Team A", 1, 1, 20, 3),
                new("Team B", 2, 2, 18, 3)
            };

            _rankingServiceMock.Setup(s => s.GetRankingsAsync())
                               .ReturnsAsync(rankings);

            var result = await _controller.GetRankingsAsync();
            var ok = AssertOk(result.Result);

            Assert.Equal(rankings, ok.Value);
        }

        [Fact]
        public async Task Get_Rankings_Async_Should_Return_Not_Found_When_No_Rankings_Exist()
        {
            _rankingServiceMock.Setup(s => s.GetRankingsAsync())
                               .ReturnsAsync(new List<GetRankingResponse>());

            var result = await _controller.GetRankingsAsync();
            var notFound = AssertNotFound(result.Result);

            Assert.Equal("No rankings available!", notFound.Value);
        }
    }
}
