using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IMatchLineupService
    {
        Task<MatchLineup> AddPlayerInMatchLineupAsync(MatchLineup matchLineup);
        Task<IEnumerable<MatchLineup>> GetMatchLineupByMatchAsync(int matchId);
        Task<IEnumerable<MatchLineup>> GetMatchLineupByMatchAndTeam(int matchId, int teamId);
        Task DeleteMatchLineupAsync(int matchId, int matchLineupId);
    }
}
