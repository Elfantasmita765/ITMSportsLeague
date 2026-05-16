using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IMatchLineupService
    {
        Task<MatchLineup> AddPlayerInMatchLineupAsync(int matchId, MatchLineup matchLineup);
        Task<IEnumerable<MatchLineup>> GetMatchLineupByMatchAsync(int matchId);
        Task<IEnumerable<MatchLineup>> GetMatchLineupByMatchAndTeamAsync(int matchId, int teamId);
        Task DeleteMatchLineupAsync(int matchId, int matchLineupId);
    }
}
