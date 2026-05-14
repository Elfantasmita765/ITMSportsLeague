using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Helpers;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.Domain.Services
{
    public class MatchLineupService : IMatchLineupRepository
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ILogger<MatchLineupService> _logger;

        public MatchLineupService(
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository,
            ITeamRepository teamRepository,
            ILogger<MatchLineupService> logger)
        {
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
            _logger = logger;
        }
    }
}
