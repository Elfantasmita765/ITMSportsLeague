

using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ISponsorService
    {
        Task<IEnumerable<Sponsor>> GetAllAsync(); //LISTO
        Task<Sponsor?> GetByIdAsync(int id); //LISTO
        Task<Sponsor?> GetByNameAsync(string name); //opc //LISTO
        Task<Sponsor?> GetByEmailAsync(string Email); //opc //LISTO
        Task<IEnumerable<Sponsor>> GetByCategoryAsync(SponsorCategory category); //opc //LISTO
        Task<Sponsor> CreateAsync(Sponsor sponsor); //LISTO
        Task UpdateAsync(int id, Sponsor sponsor); //LISTO
        Task DeleteAsync(int id); //LISTO
        Task<IEnumerable<Team>> GetTournamentsBySponsorAsync(int sponsorId); //LISTO
        Task<IEnumerable<Team>> GetSponsorsByTournamentAsync(int tournamentId); //LISTO
        Task LinkSponsorAsync(int tournamentId, int sponsorId, decimal amount); //LISTO
        Task UnlinkSponsorAsync(int tournamentId, int sponsorId); //LISTO
    }
}
