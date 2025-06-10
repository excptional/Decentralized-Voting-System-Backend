using AutoMapper;
using DVotingBackendApp.exceptions;
using DVotingBackendApp.models;
using DVotingBackendApp.repositories.interfaces;
using DVotingBackendApp.requests;
using DVotingBackendApp.responses;
using DVotingBackendApp.services.interfaces;

namespace DVotingBackendApp.services
{
    public class ConstituencyService : IConstituencyService
    {
        private readonly IConstituencyRepository _constituencyRepository;
        private readonly IMapper _mapper;

        public ConstituencyService(IConstituencyRepository constituencyRepository, IMapper mapper)
        {
            _constituencyRepository = constituencyRepository;
            _mapper = mapper;
        }

        public async Task<string> RegisterConstituencyAsync(ConstituencyRequest constituencyRequest)
        {
            var constituencyId = $"{constituencyRequest.StateCode}-{constituencyRequest.Type}-{constituencyRequest.Number}-{constituencyRequest.Name}";

            if (await _constituencyRepository.ExistsConstituencyAsync(constituencyId))
                throw new InvalidConstituencyException("A constituency with the same details already exists. Duplicate entries are not allowed.");

            var constituency = _mapper.Map<Constituency>(constituencyRequest);
            return await _constituencyRepository.RegisterConstituencyAsync(constituency);
        }

        public async Task<IEnumerable<ConstituencyResponse>> FetchConstituenciesAsync()
        {
            var response = await _constituencyRepository.FetchConstituenciesAsync();

            if (!response.Any())
                throw new EntityNotFoundException("No constituencies have been registered yet.");

            var constitunecies = response.Select(dto =>
                _mapper.Map<ConstituencyResponse>(dto));

            return constitunecies;
        }

    }
}
