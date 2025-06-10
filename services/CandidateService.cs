using AutoMapper;
using DVotingBackendApp.exceptions;
using DVotingBackendApp.models;
using DVotingBackendApp.repositories;
using DVotingBackendApp.repositories.interfaces;
using DVotingBackendApp.requests;
using DVotingBackendApp.responses;
using DVotingBackendApp.services.interfaces;

namespace DVotingBackendApp.services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IConstituencyRepository _constituencyRepository;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository candidateRepository, IConstituencyRepository constituencyRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _constituencyRepository = constituencyRepository;
            _mapper = mapper;
        }

        public async Task<string> RegisterCandidateAsync(CandidateRequest candidateRequest)
        {

            if (await _candidateRepository.ExistsCandidateAsync(candidateRequest.UID))
                throw new InvalidCandidateException("This candidate is already registered. Duplicate registration is not allowed.");

            if (!await _constituencyRepository.ExistsConstituencyAsync(candidateRequest.Constituency))
                throw new EntityNotFoundException("The specified constituency does not exist. Please enter a valid constituency name.");

            if (!await _candidateRepository.CheckPartyAvailabilityAsync(candidateRequest.Constituency, candidateRequest.PoliticalAffiliation))
                throw new InvalidCandidateException("A candidate from this party is already registered in the selected constituency.");

            var candiadate = _mapper.Map<Candidate>(candidateRequest);

            return await _candidateRepository.RegisterCandidateAsync(candiadate);
        }

        public async Task<IEnumerable<CandidateResponse>> FetchCandidatesAsync(string constituency)
        {
            if (string.IsNullOrWhiteSpace(constituency))
                throw new InvalidCandidateException("Constituency name must not be empty.");

            var result = await _candidateRepository.FetchCandidateIdsAsync(constituency);
            if (!result.Any())
                throw new EntityNotFoundException($"No candidates have been registered in the constituency '{constituency}'.");

            //var candidates = new List<Candidate>();

            var candidateTasks = result.Select(async id =>
            {
                var candidateDto = await _candidateRepository.FetchCandidateAsync(id);

                return _mapper.Map<CandidateResponse>(candidateDto);
            });
            var candidates = await Task.WhenAll(candidateTasks);

            //candidates.AddRange(fetchedCandidates);

            return candidates;
        }

        public async Task<CandidateResponse> FetchCandidateAsync(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                throw new InvalidCandidateException("Candidate UID must not be empty.");

            try
            {
                var response = await _candidateRepository.FetchCandidateAsync(uid);
                var candidate = _mapper.Map<CandidateResponse>(response);

                candidate.VoteCount = response.Votes.Count;

                return candidate;

            } catch (Exception)
            {
                throw new EntityNotFoundException($"No candidate found with UID '{uid}'.");
            }
        }

    }
}
