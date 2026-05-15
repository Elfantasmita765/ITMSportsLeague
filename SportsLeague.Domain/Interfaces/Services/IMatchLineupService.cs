using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IMatchLineupService
    {
        Task<MatchLineup> AddPlayerAsync(MatchLineup matchLineup);
        Task<IEnumerable<MatchLineup>> GetMatchLineupByMatchAsync(int matchId);
        Task<MatchLineup> GetMatchLineupByMatchAndTeam(int matchId, int teamId);
        Task DeleteMatchLineupAsync(int matchId);
    }
}
