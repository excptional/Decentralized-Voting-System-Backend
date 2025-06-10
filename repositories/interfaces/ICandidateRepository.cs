using DVotingBackendApp.dtos;
using DVotingBackendApp.models;

namespace DVotingBackendApp.repositories.interfaces
{
    public interface ICandidateRepository
    {
        Task<string> RegisterCandidateAsync(Candidate candidate);
        Task<List<string>> FetchCandidateIdsAsync(string constituency);
        Task<CandidateDto> FetchCandidateAsync(string uid);
        Task<bool> ExistsCandidateAsync(string uid);
        Task<bool> CheckPartyAvailabilityAsync(string constituency, string politicalAffiliation);

    }
}
