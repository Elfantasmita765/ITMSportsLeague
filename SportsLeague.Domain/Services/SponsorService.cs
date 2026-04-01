using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using System.Net.Mail; //Para validar el formato del correo

namespace SportsLeague.Domain.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly ITournamentSponsorRepository _tournamentSponsorRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ILogger<SponsorService> _logger;

        public SponsorService(
            ISponsorRepository sponsorRepository,
            ITournamentSponsorRepository tournamentSponsorRepository,
            ITournamentRepository tournamentRepository,
            ILogger<SponsorService> logger)
        {
            _sponsorRepository = sponsorRepository;
            _tournamentSponsorRepository = tournamentSponsorRepository;
            _tournamentRepository = tournamentRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Sponsor>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all sponsors");
            return await _sponsorRepository.GetAllAsync();
        }

        public async Task<Sponsor?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving sponsor with ID: {SponsorId}", id);
            var sponsor = await _sponsorRepository.GetByIdWithTournamentsAsync(id);
            if (sponsor == null)
                _logger.LogWarning("sponsor with ID {SponsorId} not found", id);
            return sponsor;
        }

        public async Task<Sponsor?> GetByNameAsync(string name)
        {
            _logger.LogInformation("Retrieving sponsor with Name: {SponsorName}", name);
            var sponsor = await _sponsorRepository.GetByNameAsync(name);
            if (sponsor == null)
                _logger.LogWarning("sponsor with Name {SponsorName} not found", name);
            return sponsor;
        }

        public async Task<Sponsor?> GetByEmailAsync(string email)
        {
            _logger.LogInformation("Retrieving sponsor with Email: {SponsorEmail}", email);
            var sponsor = await _sponsorRepository.GetByEmailAsync(email);
            if (sponsor == null)
                _logger.LogWarning("sponsor with Email {SponsorEmail} not found", email);
            return sponsor;
        }

        public async Task<IEnumerable<Sponsor>> GetByCategoryAsync(SponsorCategory category)
        {
            //Validar que la categoria si exista dentro del Enum (SponsorCategory)
            if (!Enum.IsDefined(typeof(SponsorCategory), category))
                throw new ArgumentException("Categoria invalida");

            _logger.LogInformation("Retrieving sponsors with Category: {SponsorCategory}", category);
            var sponsors = await _sponsorRepository.GetByCategoryAsync(category);
            return sponsors;
        }

        public async Task<Sponsor> CreateAsync(Sponsor sponsor)
        {
            // Validar que el nombre no este duplicado
            bool existsSponsorName = await _sponsorRepository.ExistsByNameAsync(sponsor.Name);
            if (existsSponsorName == true)
            {
                throw new InvalidOperationException("El nombre del sponsor ya esta en uso");
            }

            // Validar que el email este en un formato valido
            string email = sponsor.ContactEmail;
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email es obligatorio", nameof(email));
            try
            {
                var address = new MailAddress(email);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Formato de email es invalido", nameof(email));
            }

            //Validar que la categoria sea valida
            if (!Enum.IsDefined(typeof(SponsorCategory), sponsor.Category))
                throw new ArgumentException("Categoria invalida");

            _logger.LogInformation("Creating sponsor: {SponsorName}", sponsor.Name);
            return await _sponsorRepository.CreateAsync(sponsor);
        }

        public async Task UpdateAsync(int id, Sponsor sponsor)
        {
            var existing = await _sponsorRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró el sponsor con ID {id}");

            // Validar que el nuevo nombre no este duplicado
            var existingSponsor = await _sponsorRepository.GetByNameAsync(sponsor.Name);
            if (existingSponsor != null && existingSponsor.Id != id)
            {
                throw new InvalidOperationException("El nombre del sponsor ya esta en uso");
            }

            // Validar que el email este en un formato valido
            string email = sponsor.ContactEmail;
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email es obligatorio", nameof(email));
            try
            {
                var address = new MailAddress(email);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Formato de email es invalido", nameof(email));
            }

            //Validar que la categoria sea valida
            if (!Enum.IsDefined(typeof(SponsorCategory), sponsor.Category))
                throw new ArgumentException("Categoria invalida");

            existing.Name = sponsor.Name;
            existing.ContactEmail = sponsor.ContactEmail;
            existing.Phone = sponsor.Phone;
            existing.WebsiteUrl = sponsor.WebsiteUrl;
            existing.Category = sponsor.Category;

            _logger.LogInformation("Updating sponsor with ID: {SponsorId}", id);
            await _sponsorRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _sponsorRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró el sponsor con ID {id}");

            _logger.LogInformation("Deleting sponsor with ID: {SponsorId}", id);
            await _sponsorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Tournament>> GetTournamentsBySponsorAsync(int sponsorId)
        {
            var sponsor = await _sponsorRepository.GetByIdAsync(sponsorId);
            if (sponsor == null)
                throw new KeyNotFoundException(
                    $"No se encontró el sponsor con ID {sponsorId}");

            var tournamentSponsors = await _tournamentSponsorRepository
                .GetBySponsorIdAsync(sponsorId);

            return tournamentSponsors.Select(ts => ts.Tournament);
        }

        public async Task<IEnumerable<Sponsor>> GetSponsorsByTournamentAsync(int tournamentId)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
            if (tournament == null)
                throw new KeyNotFoundException(
                    $"No se encontró el torneo con ID {tournamentId}");

            var tournamentSponsors = await _tournamentSponsorRepository
                .GetByTournamentIdAsync(tournamentId);

            return tournamentSponsors.Select(ts => ts.Sponsor);
        }

        public async Task<TournamentSponsor> LinkSponsorAsync(int tournamentId, int sponsorId, decimal amount)
        {
            //Validar que el monto sea mayor a 0
            if (amount <= 0)
            {
                throw new ArgumentException("El monto del contrato debe ser mayor a 0");
            }

            // Validar que el torneo existe
            var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
            if (tournament == null)
                throw new KeyNotFoundException(
                    $"No se encontró el torneo con ID {tournamentId}");

            // Validar que el sponsor existe
            var sponsor = await _sponsorRepository.ExistsAsync(sponsorId);
            if (!sponsor)
                throw new KeyNotFoundException(
                    $"No se encontró el sponsor con ID {sponsorId}");

            // Validar que no esté ya vinculado
            var existing = await _tournamentSponsorRepository
                .GetByTournamentAndSponsorIdAsync(tournamentId, sponsorId);
            if (existing != null)
            {
                throw new InvalidOperationException(
                    "Este sponsor ya está vinculado al torneo");
            }


            var tournamentSponsor = new TournamentSponsor
            {
                TournamentId = tournamentId,
                SponsorId = sponsorId,
                ContractAmount = amount,
                JoinedAt = DateTime.UtcNow
            };

            _logger.LogInformation(
                "Linking sponsor {SponsorId} in tournament {TournamentId}",
                sponsorId, tournamentId);
            await _tournamentSponsorRepository.CreateAsync(tournamentSponsor);

            var result = await _tournamentSponsorRepository.GetByTournamentAndSponsorIdAsync(tournamentId, sponsorId);

            return result!;
        }

        public async Task UnlinkSponsorAsync(int tournamentId, int sponsorId)
        {
            // Validar que el torneo existe
            var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
            if (tournament == null)
                throw new KeyNotFoundException(
                    $"No se encontró el torneo con ID {tournamentId}");

            // Validar que el sponsor existe
            var sponsor = await _sponsorRepository.ExistsAsync(sponsorId);
            if (!sponsor)
                throw new KeyNotFoundException(
                    $"No se encontró el sponsor con ID {sponsorId}");

            // Validar que exista la vinculacion
            var existing = await _tournamentSponsorRepository
                .GetByTournamentAndSponsorIdAsync(tournamentId, sponsorId);
            if (existing == null)
            {
                throw new InvalidOperationException(
                    "Este sponsor no estaba vinculado al torneo");
            }

            _logger.LogInformation("Unlinking sponsor with ID: {SponsorId} , from the tournament with ID: {TournamentId}", sponsorId, tournamentId);
            await _tournamentSponsorRepository.DeleteAsync(existing.Id);
        }
    }
}
