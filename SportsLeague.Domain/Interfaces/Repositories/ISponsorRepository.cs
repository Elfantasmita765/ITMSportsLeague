using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ISponsorRepository : IGenericRepository<Sponsor>
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<Sponsor?> GetByNameAsync(string name);
        Task<Sponsor?> GetByEmailAsync(string email);
        Task<IEnumerable<Sponsor>> GetByCategoryAsync(SponsorCategory category);
        Task<Sponsor?> GetByIdWithTournamentsAsync(int id);
    }
}
