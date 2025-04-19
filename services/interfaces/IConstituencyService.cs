using DVotingBackendApp.dtos;
using DVotingBackendApp.models;

namespace DVotingBackendApp.services.interfaces
{
    public interface IConstituencyService
    {
        Task<string> RegisterConstituencyAsync(Constituency constituency);
        Task<List<Constituency>> FetchConstituenciesAsync();
    }
}
