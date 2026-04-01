

using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ISponsorService
    {
        Task<IEnumerable<Sponsor>> GetAllAsync(); 
        Task<Sponsor?> GetByIdAsync(int id); 
        Task<Sponsor?> GetByNameAsync(string name);
        Task<IEnumerable<Sponsor>> GetByCategoryAsync(SponsorCategory category);
        Task<Sponsor> CreateAsync(Sponsor sponsor);
        Task UpdateAsync(int id, Sponsor sponsor); 
        Task DeleteAsync(int id); 
        Task<IEnumerable<Tournament>> GetTournamentsBySponsorAsync(int sponsorId); 
        Task<IEnumerable<Sponsor>> GetSponsorsByTournamentAsync(int tournamentId); 
        Task<TournamentSponsor> LinkSponsorAsync(int tournamentId, int sponsorId, decimal amount); 
        Task UnlinkSponsorAsync(int tournamentId, int sponsorId); 
    }
}
