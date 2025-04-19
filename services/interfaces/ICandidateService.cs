using DVotingBackendApp.models;

namespace DVotingBackendApp.services.interfaces
{
    public interface ICandidateService
    {
        Task<string> RegisterCandidateAsync(Candidate candidate);
        Task<List<Candidate>> FetchCandidatesAsync(string constituency);
        Task<Candidate> FetchCandidateAsync(string uid);
    }
}
