using DVotingBackendApp.dtos;
using DVotingBackendApp.models;
using DVotingBackendApp.repositories.interfaces;
using DVotingBackendApp.utils;
using Nethereum.Contracts;
using Nethereum.Web3;

namespace DVotingBackendApp.repositories
{
    public class ConstituencyRepository : IConstituencyRepository
    {
        private readonly Web3 _web3;
        private readonly Contract _contract;

        public ConstituencyRepository(BlockchainService blockchainService)
        {
            _web3 = blockchainService.Web3;
            _contract = blockchainService.Contract;
        }

        public async Task<string> RegisterConstituencyAsync(Constituency constituency)
        {
            var function = _contract.GetFunction("registerConstituency");
            var tx = await function.SendTransactionAndWaitForReceiptAsync(
                _web3.TransactionManager.Account.Address,
                new Nethereum.Hex.HexTypes.HexBigInteger(900000),
                null, null,
                constituency.StateCode, constituency.Type, constituency.Number, constituency.Name);
            return tx.TransactionHash;
        }

        public async Task<IEnumerable<ConstituencyDto>> FetchConstituenciesAsync()
        {
            var function = _contract.GetFunction("getConstituencies");
            var response = await function.CallDeserializingToObjectAsync<ConstituencyListDto>();

            return response.Constituencies;
            //return result.Constituencies.Select(dto => new Constituency
            //{
            //    StateCode = dto.StateCode,
            //    Type = dto.Type,
            //    Number = dto.Number,
            //    Name = dto.Name
            //}).ToList();
        }

        public async Task<bool> ExistsConstituencyAsync(string id)
        {
            var function = _contract.GetFunction("existsConstituency");
            return await function.CallAsync<bool>(id);
        }
    }

}
