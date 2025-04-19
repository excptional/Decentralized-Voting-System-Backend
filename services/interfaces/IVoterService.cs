using DVotingBackendApp.models;
using Microsoft.OpenApi.Any;

namespace DVotingBackendApp.services.interfaces
{
    public interface IVoterService
    {
        Task<string> RegisterVoterAsync(Voter voter);
        Task<Voter> FetchVoterAsync(string uid);
        Task<bool> CheckVoterValidityAsync(string uid);
        Task<string> VoteAsync(Vote vote);

    }
}
