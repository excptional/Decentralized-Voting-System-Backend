using DVotingBackendApp.dtos;
using DVotingBackendApp.models;

namespace DVotingBackendApp.repositories.interfaces
{
    public interface IConstituencyRepository
    {
        Task<string> RegisterConstituencyAsync(Constituency constituency);
        Task<IEnumerable<ConstituencyDto>> FetchConstituenciesAsync();
        Task<bool> ExistsConstituencyAsync(string id);
    }
}
