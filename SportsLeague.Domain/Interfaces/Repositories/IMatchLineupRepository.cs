using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IMatchLineupRepository : IGenericRepository<MatchLineup>
    {
        Task<IEnumerable<MatchLineup>> GetByMatch(int matchId);
        Task<IEnumerable<MatchLineup>> GetByMatchAndTeam(int matchId, int teamId);
        Task<bool> ExistsByMatchAndPlayer(int matchId, int playerId);
        Task<MatchLineup?> GetByIdWithDetails(int id);
    }
}
