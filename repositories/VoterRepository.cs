using System.Dynamic;
using DVotingBackendApp.dtos;
using DVotingBackendApp.models;
using DVotingBackendApp.repositories.interfaces;
using DVotingBackendApp.utils;
using Microsoft.OpenApi.Any;
using Nethereum.Contracts;
using Nethereum.Web3;

namespace DVotingBackendApp.repositories
{
    public class VoterRepository : IVoterRepository
    {
        private readonly Web3 _web3;
        private readonly Contract _contract;

        public VoterRepository(BlockchainService blockchainService) {
            _web3 = blockchainService.Web3;
            _contract = blockchainService.Contract;
        }

        public async Task<string> RegisterVoterAsync(Voter voter)
        {
            var function = _contract.GetFunction("registerVoter");
            var tx = await function.SendTransactionAndWaitForReceiptAsync(
                _web3.TransactionManager.Account.Address,
                new Nethereum.Hex.HexTypes.HexBigInteger(900000),
                null, null,
                voter.Name, voter.FatherName, voter.ImageUrl, voter.Sex, voter.DOB, voter.UID, voter.Constituency,
                voter.Location, voter.Phone);
            return tx.TransactionHash;
        }

        public async Task<List<string>> FetchVoterIdsAsync(string constituency)
        {
            var function = _contract.GetFunction("getVoterIds");
            var ids = await function.CallAsync<List<string>>(constituency);
            return ids;
        }

        public async Task<VoterDto> FetchVoterAsync(string uid)
        {
            var function = _contract.GetFunction("getVoter");

            var response = await function.CallDeserializingToObjectAsync<VoterOutputDto>(uid);

            return response.VoterDto;
            //return new Voter
            //{
            //    Name = result.VoterDto.Name,
            //    ImageUrl = result.VoterDto.ImageUrl,
            //    Sex = result.VoterDto.Sex,
            //    DOB = result.VoterDto.Dob,
            //    UID = result.VoterDto.Uid,
            //    Constituency = result.VoterDto.Constituency,
            //    Location = result.VoterDto.Location,
            //    Phone = result.VoterDto.PhoneNumber
            //};
        }

        public async Task<bool> CheckVoterValidityAsync(string uid)
        {
            var function = _contract.GetFunction("checkVoterValidity");
            return await function.CallAsync<bool>(uid);
        }

        public async Task<bool> ExistsVoterAsync(string uid)
        {
            var function = _contract.GetFunction("existsVoter");
            return await function.CallAsync<bool>(uid);
        }

        public async Task<string> VoteAsync(Vote vote)
        {
            var function = _contract.GetFunction("vote");
            var tx = await function.SendTransactionAndWaitForReceiptAsync(
                _web3.TransactionManager.Account.Address,
                new Nethereum.Hex.HexTypes.HexBigInteger(900000),
                null, null,
                vote.VoterUid, vote.CandidateUid);
            return tx.TransactionHash;
        }

        public async Task<bool> IsVoterEligibleAsync(string uid)
        {
            var function = _contract.GetFunction("isVoterEligible");
            return await function.CallAsync<bool>(uid);
        }


    }

}
