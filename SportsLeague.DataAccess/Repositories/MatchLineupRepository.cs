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
                .Include(ml => ml.Player)
                .ThenInclude(p => p.Team)
                .ToListAsync();
        }

        public async Task<IEnumerable<MatchLineup>> GetByMatchAndTeam(int matchId, int teamId)
        {
            return await _dbSet
                .Where(ml => ml.MatchId == matchId)
                .Where(ml => ml.Player.TeamId == teamId)
                .Include(ml => ml.Player)
                .ThenInclude(p => p.Team)
                .ToListAsync();
        }

        public async Task<bool> ExistsByMatchAndPlayer(int matchId, int playerId)
        {
            return await _dbSet
                .AnyAsync(ml => ml.MatchId == matchId && ml.PlayerId == playerId);
        }

        public async Task<MatchLineup?> GetByIdWithDetails(int id)
        {
            return await _dbSet
                .Include(ml => ml.Match)
                .Include(ml => ml.Player)
                .ThenInclude(p => p.Team)
                .FirstOrDefaultAsync(ml => ml.Id == id);
        }
    }
}
