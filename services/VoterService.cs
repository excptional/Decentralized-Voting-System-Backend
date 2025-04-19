using DVotingBackendApp.exceptions;
using DVotingBackendApp.models;
using DVotingBackendApp.repositories.interfaces;
using DVotingBackendApp.services.interfaces;

namespace DVotingBackendApp.services
{
    public class VoterService : IVoterService
    {
        private readonly IVoterRepository _voterRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IConstituencyRepository _constituencyRepository;

        public VoterService(IVoterRepository voterRepository, IConstituencyRepository constituencyRepository, ICandidateRepository candidateRepository)
        {
            _voterRepository = voterRepository;
            _constituencyRepository = constituencyRepository;
            _candidateRepository = candidateRepository;
        }

        public async Task<string> RegisterVoterAsync(Voter voter)
        {
            if (string.IsNullOrWhiteSpace(voter.Name) ||
                string.IsNullOrWhiteSpace(voter.UID) ||
                string.IsNullOrWhiteSpace(voter.Sex) ||
                string.IsNullOrWhiteSpace(voter.Constituency) ||
                string.IsNullOrWhiteSpace(voter.Location) ||
                string.IsNullOrWhiteSpace(voter.Phone))
                throw new InvalidCandidateException("Some required voter fields are missing. Please check all inputs and try again.");


            if (voter.DOB == default || voter.DOB > DateTime.Now)
                throw new InvalidVoterException("Invalid date of birth for voter.");

            if (await _voterRepository.ExistsVoterAsync(voter.UID))
                throw new InvalidVoterException("Voter is already exists.");

            if (!await _constituencyRepository.ExistsConstituencyAsync(voter.Constituency))
                throw new EntityNotFoundException("Constituency is not exist. Enter valid constituency.");

            return await _voterRepository.RegisterVoterAsync(voter);
        }

        public async Task<Voter> FetchVoterAsync(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                throw new InvalidVoterException("UID cannot be empty.");

            var voter = await _voterRepository.FetchVoterAsync(uid);

            if (voter == null)
                throw new EntityNotFoundException($"No voter found with UID: {uid}");

            return voter;
        }

        public async Task<bool> CheckVoterValidityAsync(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                throw new InvalidVoterException("UID cannot be null or empty.");

            return await _voterRepository.CheckVoterValidityAsync(uid);
        }

        public async Task<string> VoteAsync(Vote vote)
        {
            if (string.IsNullOrWhiteSpace(vote.VoterUid) ||
                string.IsNullOrWhiteSpace(vote.CandidateUid))
                throw new InvalidVoterException("Voter Uid and Candidate Uid must not be empty or null.");

            if (!await _voterRepository.ExistsVoterAsync(vote.VoterUid))
                throw new EntityNotFoundException("Voter with this Uid is not registered. Enter valid Voter Uid");

            if (!await _candidateRepository.ExistsCandidateAsync(vote.CandidateUid))
                throw new EntityNotFoundException("Candidate with this Uid is not registered. Enter valid Candidate Uid");

            if (!await _voterRepository.IsVoterEligibleAsync(vote.VoterUid))
                throw new InvalidVoterException("Voter with this Uid is already voted.");

            return await _voterRepository.VoteAsync(vote);
        }
    }
}
