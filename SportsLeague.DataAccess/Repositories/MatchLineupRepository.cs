using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class MatchLineupRepository : GenericRepository<MatchLineup>, IMatchLineupRepository
    {
        public MatchLineupRepository(LeagueDbContext context) : base(context) { }

        public async Task<IEnumerable<MatchLineup>> GetByMatch(int matchId)
        {
            return await _dbSet
                .Where(ml => ml.MatchId == matchId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MatchLineup>> GetByMatchAndTeam(int matchId, int teamId)
        {
            return await _dbSet
                .Where(ml => ml.MatchId == matchId)
                .Where(ml => ml.Player.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<bool> ExistsByMatchAndPlayer(int matchId, int playerId)
        {
            return await _dbSet
                .AnyAsync(ml => ml.MatchId == matchId && ml.PlayerId == playerId);
        }
            
    }
}
