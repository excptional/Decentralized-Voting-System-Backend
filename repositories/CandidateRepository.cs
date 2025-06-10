using DVotingBackendApp.dtos;
using DVotingBackendApp.models;
using DVotingBackendApp.repositories.interfaces;
using DVotingBackendApp.utils;
using Nethereum.Contracts;
using Nethereum.Web3;

namespace DVotingBackendApp.repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly Web3 _web3;
        private readonly Contract _contract;

        public CandidateRepository(BlockchainService blockchainService)
        {
            _web3 = blockchainService.Web3;
            _contract = blockchainService.Contract;
        }

        public async Task<string> RegisterCandidateAsync(Candidate candidate)
        {
            var function = _contract.GetFunction("registerCandidate");
            var tx = await function.SendTransactionAndWaitForReceiptAsync(
                _web3.TransactionManager.Account.Address,
                new Nethereum.Hex.HexTypes.HexBigInteger(900000),
                null, null,
                candidate.Name, candidate.FatherName, candidate.ImageUrl, candidate.Sex, candidate.DOB, candidate.UID, candidate.Constituency,
                candidate.Location, candidate.PoliticalAffiliation, candidate.Phone);
            return tx.TransactionHash;
        }

        public async Task<List<string>> FetchCandidateIdsAsync(string constituency)
        {
            var function = _contract.GetFunction("getCandidateIds");
            var ids = await function.CallAsync<List<string>>(constituency);
            return ids;
        }

        public async Task<CandidateDto> FetchCandidateAsync(string uid)
        {
            var function = _contract.GetFunction("getCandidate");
            var response = await function.CallDeserializingToObjectAsync<CandidateOutputDto>(uid);

            return response.CandidateDto;

            //return new Candidate
            //{
            //    Name = result.CandidateDto.Name,
            //    ImageUrl = result.CandidateDto.ImageUrl,
            //    Sex = result.CandidateDto.Sex,
            //    DOB = result.CandidateDto.DOB,
            //    UID = result.CandidateDto.UID,
            //    Constituency = result.CandidateDto.Constituency,
            //    Location = result.CandidateDto.Location,
            //    PoliticalAffiliation = result.CandidateDto.PoliticalAffiliation,
            //    Phone = result.CandidateDto.Phone,
            //    Votes = result.CandidateDto.Votes?.ToList() ?? new List<string>()
            //};
        }

        public async Task<bool> ExistsCandidateAsync(string uid)
        {
            var function = _contract.GetFunction("existsCandidate");
            return await function.CallAsync<bool>(uid);
        }

        public async Task<bool> CheckPartyAvailabilityAsync(string constituency, string politicalAffiliation)
        {
            var function = _contract.GetFunction("checkParty");
            return await function.CallAsync<bool>(constituency, politicalAffiliation);
        }
    }

}
