using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Helpers;
using SportsLeague.Domain.Interfaces.Repositories;
using System.Text.RegularExpressions;

namespace SportsLeague.Domain.Services
{
    public class MatchLineupService : IMatchLineupRepository
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
        public async Task<MatchLineup> AddPlayerAsync(MatchLineup matchLineup)
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
                    "El estado del partido debe estar Scheduled");
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
                ExistsByMatchAndPlayer(matchLineup.MatchId,matchLineup.PlayerId);
            if (repeat == true)
            {
                throw new InvalidOperationException(
                    $"El jugador con ID {matchLineup.PlayerId} ya esta registrado en la alineacion");
            }

            // Validacion del limite de titulares por equipo por partido (max 11)
            var matchlineUps = await _matchLineupRepository.GetByMatchAndTeam(matchLineup.MatchId, player!.TeamId);
            int count = 0;
            foreach ( var matchLine in matchlineUps)
            {
                if (matchLine.IsStarter == true) 
                {
                    count++; 
                }
            }
            if (count >= 11)
            {
                throw new InvalidOperationException(
                    "El maximo de titulares por equipo por partido es 11");
            }

            // Modificar
            _logger.LogInformation(
                $"Registering LineUp for match: {matchLineup.MatchId}, " +
                $"Player: {matchLineup.Player.FirstName + "" + matchLineup.Player.LastName}");
            return await _matchLineupRepository.CreateAsync(matchLineup);
        }
    }
}
