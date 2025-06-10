using DVotingBackendApp.models;
using DVotingBackendApp.requests;
using DVotingBackendApp.responses;
using Microsoft.OpenApi.Any;

namespace DVotingBackendApp.services.interfaces
{
    public interface IVoterService
    {
        Task<string> RegisterVoterAsync(VoterRequest voterRequest);
        Task<VoterResponse> FetchVoterAsync(string uid);
        Task<IEnumerable<VoterResponse>> FetchVotersAsync(string constituency);
        Task<bool> CheckVoterValidityAsync(string uid);
        Task<string> VoteAsync(Vote vote);

    }
}
