using DVotingBackendApp.dtos;
using DVotingBackendApp.models;
using DVotingBackendApp.requests;
using DVotingBackendApp.responses;

namespace DVotingBackendApp.services.interfaces
{
    public interface IConstituencyService
    {
        Task<string> RegisterConstituencyAsync(ConstituencyRequest constituencyRequest);
        Task<IEnumerable<ConstituencyResponse>> FetchConstituenciesAsync();
    }
}
