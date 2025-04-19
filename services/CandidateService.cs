using DVotingBackendApp.exceptions;
using DVotingBackendApp.models;
using DVotingBackendApp.repositories;
using DVotingBackendApp.repositories.interfaces;
using DVotingBackendApp.services.interfaces;

namespace DVotingBackendApp.services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IConstituencyRepository _constituencyRepository;

        public CandidateService(ICandidateRepository candidateRepository, IConstituencyRepository constituencyRepository)
        {
            _candidateRepository = candidateRepository;
            _constituencyRepository = constituencyRepository;
        }

        public async Task<string> RegisterCandidateAsync(Candidate candidate)
        {
            if (string.IsNullOrWhiteSpace(candidate.Name) ||
                string.IsNullOrWhiteSpace(candidate.UID) ||
                string.IsNullOrWhiteSpace(candidate.Sex) ||
                string.IsNullOrWhiteSpace(candidate.Constituency) ||
                string.IsNullOrWhiteSpace(candidate.Location) ||
                string.IsNullOrWhiteSpace(candidate.Phone) ||
                string.IsNullOrWhiteSpace(candidate.PoliticalAffiliation))
                throw new InvalidCandidateException("Some required candidate fields are missing. Please check all inputs and try again.");

            if (candidate.DOB == default || candidate.DOB > DateTime.Now)
                throw new InvalidCandidateException("The provided date of birth is invalid. Please enter a valid DOB.");

            if (await _candidateRepository.ExistsCandidateAsync(candidate.UID))
                throw new InvalidCandidateException("This candidate is already registered. Duplicate registration is not allowed.");

            if (!await _constituencyRepository.ExistsConstituencyAsync(candidate.Constituency))
                throw new EntityNotFoundException("The specified constituency does not exist. Please enter a valid constituency name.");

            if (!await _candidateRepository.CheckPartyAvailabilityAsync(candidate.Constituency, candidate.PoliticalAffiliation))
                throw new InvalidCandidateException("A candidate from this party is already registered in the selected constituency.");

            return await _candidateRepository.RegisterCandidateAsync(candidate);
        }

        public async Task<List<Candidate>> FetchCandidatesAsync(string constituency)
        {
            if (string.IsNullOrWhiteSpace(constituency))
                throw new InvalidCandidateException("Constituency name must not be empty.");

            var result = await _candidateRepository.FetchCandidateIdsAsync(constituency);
            if (!result.Any())
                throw new EntityNotFoundException($"No candidates have been registered in the constituency '{constituency}'.");

            var candidates = new List<Candidate>();

            var candidateTasks = result.Select(id => _candidateRepository.FetchCandidateAsync(id));
            var fetchedCandidates = await Task.WhenAll(candidateTasks);
            candidates.AddRange(fetchedCandidates);

            return candidates;
        }

        public async Task<Candidate> FetchCandidateAsync(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                throw new InvalidCandidateException("Candidate UID must not be empty.");

            var candidate = await _candidateRepository.FetchCandidateAsync(uid);
            if (candidate == null)
                throw new EntityNotFoundException($"No candidate found with UID '{uid}'.");

            return candidate;
        }

    }
}
