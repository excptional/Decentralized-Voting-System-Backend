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
                voter.Name, voter.Sex, DateTimeUtils.ToDateString(voter.DOB), voter.UID, voter.Constituency,
                voter.Location, voter.Phone);
            return tx.TransactionHash;
        }

        public async Task<Voter> FetchVoterAsync(string uid)
        {
            var function = _contract.GetFunction("getVoter");

            var result = await function.CallDeserializingToObjectAsync<VoterOutputDto>(uid);


            return new Voter
            {
                Name = result.VoterDto.Name,
                Sex = result.VoterDto.Gender,
                DOB = DateTimeUtils.FromDateString(result.VoterDto.Dob),
                UID = result.VoterDto.Uid,
                Constituency = result.VoterDto.Constituency,
                Location = result.VoterDto.Location,
                Phone = result.VoterDto.PhoneNumber
            };
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
