using DVotingBackendApp.models;
using DVotingBackendApp.requests;
using DVotingBackendApp.responses;

namespace DVotingBackendApp.services.interfaces
{
    public interface ICandidateService
    {
        Task<string> RegisterCandidateAsync(CandidateRequest candidateRequest);
        Task<IEnumerable<CandidateResponse>> FetchCandidatesAsync(string constituency);
        Task<CandidateResponse> FetchCandidateAsync(string uid);
    }
}
