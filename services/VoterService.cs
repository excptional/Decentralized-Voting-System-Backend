using AutoMapper;
using DVotingBackendApp.exceptions;
using DVotingBackendApp.models;
using DVotingBackendApp.repositories.interfaces;
using DVotingBackendApp.requests;
using DVotingBackendApp.responses;
using DVotingBackendApp.services.interfaces;

namespace DVotingBackendApp.services
{
    public class VoterService : IVoterService
    {
        private readonly IVoterRepository _voterRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IConstituencyRepository _constituencyRepository;
        private readonly IMapper _mapper;

        public VoterService(IVoterRepository voterRepository, IConstituencyRepository constituencyRepository, ICandidateRepository candidateRepository, IMapper mapper)
        {
            _voterRepository = voterRepository;
            _constituencyRepository = constituencyRepository;
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }

        public async Task<string> RegisterVoterAsync(VoterRequest voterRequest)
        {
            if (await _voterRepository.ExistsVoterAsync(voterRequest.UID))
                throw new InvalidVoterException("Voter is already exists.");

            if (!await _constituencyRepository.ExistsConstituencyAsync(voterRequest.Constituency))
                throw new EntityNotFoundException("Constituency is not exist. Enter valid constituency.");

            var voter = _mapper.Map<Voter>(voterRequest);

            return await _voterRepository.RegisterVoterAsync(voter);
        }

        public async Task<IEnumerable<VoterResponse>> FetchVotersAsync(string constituency)
        {
            if (string.IsNullOrWhiteSpace(constituency))
                throw new InvalidVoterException("Constituency name must not be empty.");

            var response = await _voterRepository.FetchVoterIdsAsync(constituency);
            if (!response.Any())
                throw new EntityNotFoundException($"No voters have been registered in the constituency '{constituency}'.");

            //var voters = new List<VoterResponse>();

            var voterTasks = response.Select(async id =>
            {
                var voterDto = await _voterRepository.FetchVoterAsync(id);

                return _mapper.Map<VoterResponse>(voterDto);
            });
            var voters = await Task.WhenAll(voterTasks);

            //voters.AddRange(fetchedVoters);

            return voters;
        }

        public async Task<VoterResponse> FetchVoterAsync(string uid)
        {
            try
            {
                var response = await _voterRepository.FetchVoterAsync(uid);

                return _mapper.Map<VoterResponse>(response);

            } catch (Exception ex)
            {
                throw new EntityNotFoundException($"No voter found with UID: {uid}");

            }
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
