using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Helpers;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class MatchLineupService : IMatchLineupService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchLineupRepository _matchLineupRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly MatchValidationHelper _validationHelper;
        private readonly ILogger<MatchLineupService> _logger;

        public MatchLineupService(
            IMatchRepository matchRepository,
            IMatchLineupRepository matchLineupRepository,
            IPlayerRepository playerRepository,
            ITeamRepository teamRepository,
            MatchValidationHelper validationHelper,
            ILogger<MatchLineupService> logger)
        {
            _matchRepository = matchRepository;
            _matchLineupRepository = matchLineupRepository;
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
            _validationHelper = validationHelper;
            _logger = logger;
        }

        //Agregar jugador a la alineacion
        public async Task<MatchLineup> AddPlayerInMatchLineupAsync(MatchLineup matchLineup)
        {
            // Validacion de que el partido exista
            var match = await _matchRepository.GetByIdAsync(matchLineup.MatchId);
            if (match == null)
                throw new KeyNotFoundException(
                    $"No se encontró el partido con ID {matchLineup.MatchId}");

            // Validacion de que el partido este en estado Scheduled
            if (match.Status != 0)
            {
                throw new InvalidOperationException(
                    "El estado del partido debe estar en Scheduled");
            }

            // Validacion de que el jugador exista
            var player = await _playerRepository.GetByIdAsync(matchLineup.PlayerId);
            if (player == null)
                throw new KeyNotFoundException(
                    $"No se encontró el jugador con ID {matchLineup.PlayerId}");

            // Validacion de que el jugador pertenezca al HomeTeam o al AwayTeam
            await _validationHelper.ValidatePlayerInMatchAsync(matchLineup.PlayerId, match);

            // Validacion de que el jugador no se encuentre ya registrado en la alineacion
            bool repeat = await _matchLineupRepository.
                ExistsByMatchAndPlayer(matchLineup.MatchId, matchLineup.PlayerId);
            if (repeat == true)
            {
                throw new InvalidOperationException(
                    $"El jugador con ID {matchLineup.PlayerId} ya esta registrado en la alineacion");
            }

            // Validacion del limite de titulares por equipo por partido (max 11)
            var matchlineUps = await _matchLineupRepository.GetByMatchAndTeam(matchLineup.MatchId, player!.TeamId);
            int starterscount = matchlineUps.Count(ml => ml.IsStarter == true);
            if (starterscount >= 11 && matchLineup.IsStarter == true)
            {
                throw new InvalidOperationException(
                    "El máximo de titulares por equipo por partido es 11");
            }

            _logger.LogInformation(
                $"Registering LineUp for match: {matchLineup.MatchId}, " +
                $"Player: {matchLineup.Player.FirstName + "" + matchLineup.Player.LastName}");
            return await _matchLineupRepository.CreateAsync(matchLineup);
        }

        public async Task<IEnumerable<MatchLineup>> GetMatchLineupByMatchAsync(int matchId)
        {
            // Validacion de que el partido exista
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException(
                    $"No se encontró el partido con ID {matchId}");

            return await _matchLineupRepository.GetByMatch(matchId);
        }

        public async Task<IEnumerable<MatchLineup>> GetMatchLineupByMatchAndTeam(int matchId, int teamId)
        {
            // Validacion de que el partido exista
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException(
                    $"No se encontró el partido con ID {matchId}");

            // Validacion de que el equipo exista
            var team = await _teamRepository.GetByIdAsync(teamId);
            if (team == null)
                throw new KeyNotFoundException(
                    $"No se encontró el equipo con ID {teamId}");

            // Validacion de que el quipo este en el partido
            if (teamId != match.AwayTeamId && teamId != match.HomeTeamId)
            {
                throw new InvalidOperationException(
                    $"Este equipo no forma parte del partido");
            }

            return await _matchLineupRepository.GetByMatchAndTeam(matchId, teamId);
        }

        public async Task DeleteMatchLineupAsync(int matchId, int matchLineupId)
        {
            //Validacion de que el partido exista
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException(
                    $"No se encontró el partido con ID {matchId}");

            //Validacion de que la alineacion exista
            var exists = await _matchLineupRepository.ExistsAsync(matchLineupId);
            if (!exists)
            {
                throw new KeyNotFoundException($"No se encontro la alineacion con ID {matchLineupId}");
            }

            //Validacion de que el partido este en Scheduled
            if (match.Status != 0)
            {
                throw new InvalidOperationException(
                    "El partido debe de estar en estado Scheduled");
            }

            _logger.LogInformation(
                $"Deleting LineUp for match: {matchId}");
            await _matchLineupRepository.DeleteAsync(matchLineupId);
        }
    }
}
