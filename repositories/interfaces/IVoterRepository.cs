using DVotingBackendApp.models;
using Microsoft.OpenApi.Any;

namespace DVotingBackendApp.repositories.interfaces
{
    public interface IVoterRepository
    {
        Task<string> RegisterVoterAsync(Voter voter);
        Task<Voter> FetchVoterAsync(string uid);
        Task<bool> CheckVoterValidityAsync(string uid);
        Task<bool> ExistsVoterAsync(string uid);
        Task<string> VoteAsync(Vote vote);
        Task<bool> IsVoterEligibleAsync(string uid);
    }
}
