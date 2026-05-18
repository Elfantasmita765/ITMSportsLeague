using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers
{
    [ApiController]
    [Route("api/match/{matchId}")]
    public class MatchLineupController : ControllerBase
    {
        private readonly IMatchLineupService _matchLineupService;
        private readonly IMapper _mapper;
        public MatchLineupController(
            IMatchLineupService matchLineupService, IMapper mapper)
        {
            _matchLineupService = matchLineupService;
            _mapper = mapper;
        }

        [HttpPost("lineup")]
        public async Task<ActionResult<MatchLineupDTO>> RegisterPlayerInLineup(
            int matchId, CreateMatchLineupDTO dto)
        {
            try
            {
                var lineup = _mapper.Map<MatchLineup>(dto);
                var created = await _matchLineupService.AddPlayerInMatchLineupAsync(matchId, lineup);
                return Ok(_mapper.Map<MatchLineupDTO>(created));
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
        }

        [HttpGet("lineup")]
        public async Task<ActionResult<IEnumerable<MatchLineupDTO>>> GetMatchLineupByMatch(int matchId)
        {
            try
            {
                var lineups = await _matchLineupService.GetMatchLineupByMatchAsync(matchId);
                return Ok(_mapper.Map<IEnumerable<MatchLineupDTO>>(lineups));
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        [HttpGet("lineup/team/{teamId}")]
        public async Task<ActionResult<IEnumerable<MatchLineupDTO>>> GetMatchLineupByMatchAndTeam(int matchId, int teamId)
        {
            try
            {
                var lineups = await _matchLineupService.GetMatchLineupByMatchAndTeamAsync(matchId, teamId);
                return Ok(_mapper.Map<IEnumerable<MatchLineupDTO>>(lineups));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("lineup/{id}")]
        public async Task<ActionResult> DeleteMatchLineupAsync(int matchId, int id)
        {
            try
            {
                await _matchLineupService.DeleteMatchLineupAsync(matchId, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
        }
    }
}
