using IPL.DataAccessLayer;
using IPL.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPL.Controllers
{
    [Route("api/[controller]")]
    public class IplController : ControllerBase
    {
        private readonly IIplDAO iplDAO;

        public IplController(IIplDAO iplDAO)
        {
            this.iplDAO = iplDAO;
        }

        //[HttpGet("player/", Name = "GetAllPlayers")]
        //public async Task<List<Player>> GetAllPlayers() {
        //    List<Player> players = await iplDAO.GetAllPlayers();
        //    return players;
        //}

        //[HttpGet("player/{playerId}", Name = "GetPlayerById")]
        //public async Task<IActionResult> GetPlayerById(int playerId) {
        //    Player player = await iplDAO.GetPlayerById(playerId);
        //    if(player != null) {
        //        return Ok(player);
        //    } else {
        //        return NotFound();
        //    }
        //}

        [HttpPost("player", Name = "AddPlayer")]
        public async Task<IActionResult> AddPlayer([FromBody] Player player)
        {
            int result = await iplDAO.AddPlayer(player);
            if (result > 0)
            {
                return Ok("Player added successfully");

            }
            else
            {
                return BadRequest("Player data is required");
            }
        }

        [HttpGet("match/fan-engagement", Name = "GetMatchDetails")]
        public async Task<IActionResult> GetMatchDetails()
        {
            List<Match> matches = await iplDAO.GetMatchDetails();

            return Ok(matches);
        }

        [HttpGet("match/top-players", Name = "TopPlayersFanEngagement")]
        public async Task<IActionResult> TopPlayersFanEngagement()
        {
            List<Player> players = await iplDAO.TopPlayersFanEngagement();
            if (players != null)
            {
                return Ok(players);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("match/match-details", Name = "GetMatchesBetweenDates")]
        public async Task<IActionResult> GetMatchesBetweenDates([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            Console.WriteLine(startDate + " " + endDate);
            List<Match> matches = await iplDAO.GetMatches(startDate, endDate);
            if (matches != null)
            {
                return Ok(matches);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
