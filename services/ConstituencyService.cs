using DVotingBackendApp.exceptions;
using DVotingBackendApp.models;
using DVotingBackendApp.repositories.interfaces;
using DVotingBackendApp.services.interfaces;

namespace DVotingBackendApp.services
{
    public class ConstituencyService : IConstituencyService
    {
        private readonly IConstituencyRepository _constituencyRepository;

        public ConstituencyService(IConstituencyRepository constituencyRepository)
        {
            _constituencyRepository = constituencyRepository;
        }

        public async Task<string> RegisterConstituencyAsync(Constituency constituency)
        {
            if (string.IsNullOrWhiteSpace(constituency.StateCode) ||
                string.IsNullOrWhiteSpace(constituency.Name))
                throw new InvalidConstituencyException("Both state code and constituency name are required. Please provide valid details.");

            var constituencyId = $"{constituency.StateCode}-{constituency.Type}-{constituency.Number}-{constituency.Name}";

            if (await _constituencyRepository.ExistsConstituencyAsync(constituencyId))
                throw new InvalidConstituencyException("A constituency with the same details already exists. Duplicate entries are not allowed.");

            return await _constituencyRepository.RegisterConstituencyAsync(constituency);
        }

        public async Task<List<Constituency>> FetchConstituenciesAsync()
        {
            var result = await _constituencyRepository.FetchConstituenciesAsync();

            if (!result.Any())
                throw new EntityNotFoundException("No constituencies have been registered yet.");

            return result;
        }

    }
}
