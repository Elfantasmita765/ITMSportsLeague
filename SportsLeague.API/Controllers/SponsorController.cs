using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorService _sponsorService;
        private readonly IMapper _mapper;
        private readonly ILogger<SponsorController> _logger;

        public SponsorController(
            ISponsorService sponsorService,
            IMapper mapper,
            ILogger<SponsorController> logger)
        {
            _sponsorService = sponsorService;
            _mapper = mapper;
            _logger = logger;
        }

        //LISTAR TODOS LOS SPONSORS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SponsorResponseDTO>>> GetAll()
        {
            var sponsors = await _sponsorService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<SponsorResponseDTO>>(sponsors));
        }

        //OBTENER SPONSOR POR ID
        [HttpGet("{id}")]
        public async Task<ActionResult<SponsorResponseDTO>> GetById(int id)
        {
            var sponsor = await _sponsorService.GetByIdAsync(id);

            if (sponsor == null)
                return NotFound(new { message = $"Sponsor con ID {id} no encontrado" });

            var sponsorDto = _mapper.Map<SponsorResponseDTO>(sponsor);
            return Ok(sponsorDto);
        }

        //OBTENER SPONSOR POR NOMBRE
        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<SponsorResponseDTO>> GetByName(string name)
        {
            var sponsor = await _sponsorService.GetByNameAsync(name);

            if (sponsor == null)
                return NotFound(new { message = $"Sponsor con Nombre {name} no encontrado" });

            var sponsorDto = _mapper.Map<SponsorResponseDTO>(sponsor);
            return Ok(sponsorDto);
        }

        //LISTAR SPONSORS POR CATEGORIA
        [HttpGet("by-category/{category}")]
        public async Task<ActionResult<IEnumerable<SponsorResponseDTO>>> GetByCategory(SponsorCategory category)
        {
            try
            {
                var sponsors = await _sponsorService.GetByCategoryAsync(category);
                return Ok(_mapper.Map<IEnumerable<SponsorResponseDTO>>(sponsors));
            }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        }

        //CREAR SPONSOR
        [HttpPost]
        public async Task<ActionResult<SponsorResponseDTO>> Create(SponsorRequestDTO dto)
        {
            try
            {
                var sponsor = _mapper.Map<Sponsor>(dto);
                var created = await _sponsorService.CreateAsync(sponsor);
                var responseDto = _mapper.Map<SponsorResponseDTO>(created);
                return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
            }
            catch (InvalidOperationException ex) { return BadRequest(new { message = ex.Message }); }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        }

        //ACTUALIZAR SPONSOR
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, SponsorRequestDTO dto)
        {
            try
            {
                var sponsor = _mapper.Map<Sponsor>(dto);
                await _sponsorService.UpdateAsync(id, sponsor);
                return NoContent();
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        }

        //ELIMINAR SPONSOR
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _sponsorService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        //OBTENER TORNEOS DEL SPONSOR
        [HttpGet("{id}/tournaments")]
        public async Task<ActionResult<IEnumerable<TournamentResponseDTO>>> GetTournaments(int id)
        {
            try
            {
                var tournaments = await _sponsorService.GetTournamentsBySponsorAsync(id);
                return Ok(_mapper.Map<IEnumerable<TournamentResponseDTO>>(tournaments));
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        //OBTENER SPONSORS DEL TORNEO
        [HttpGet("{id}/getsponsorsbytournament")]
        public async Task<ActionResult<IEnumerable<SponsorResponseDTO>>> GetSponsorsByTournament(int id)
        {
            try
            {
                var sponsors = await _sponsorService.GetSponsorsByTournamentAsync(id);
                return Ok(_mapper.Map<IEnumerable<SponsorResponseDTO>>(sponsors));
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        //VINCULAR SPONSOR A TORNEO
        [HttpPost("{id}/tournaments")]
        public async Task<ActionResult<TournamentSponsorResponseDTO>> LinkSponsor(TournamentSponsorRequestDTO dto, int id)
        {
            try
            {
                var tournamentSponsor = await _sponsorService.LinkSponsorAsync(dto.TournamentId, id, dto.ContractAmount);
                var responseDto = _mapper.Map<TournamentSponsorResponseDTO>(tournamentSponsor);
                return CreatedAtAction(nameof(GetTournaments), new {id}, responseDto);
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        }

        //DESVINCULAR SPONSOR DEL TORNEO
        [HttpDelete("{id}/tournaments/{tid}")]
        public async Task<ActionResult> UnlinkSponsor(int tid, int id)
        {
            try
            {
                await _sponsorService.UnlinkSponsorAsync(tid, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
        }
    }
}
