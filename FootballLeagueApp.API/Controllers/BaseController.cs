using Microsoft.AspNetCore.Mvc;

namespace FootballLeagueApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    { }
}
