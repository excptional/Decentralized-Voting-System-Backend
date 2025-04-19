using DotNetEnv;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json.Linq;

namespace DVotingBackendApp.utils
{
    public class BlockchainService
    {
        public Web3 Web3 { get; }
        public Contract Contract { get; }

        public BlockchainService()
        {
            Env.Load();

            var privateKey = Env.GetString("PRIVATE_KEY");
            var apiUrl = Env.GetString("ALCHEMY_API_URL");
            var contractAddress = Env.GetString("CONTRACT_ADDRESS");

            var account = new Account(privateKey);
            Web3 = new Web3(account, apiUrl);

            var abiPath = Path.Combine("abi_files", "ES.json");
            var abi = JObject.Parse(File.ReadAllText(abiPath))["abi"]!.ToString();

            Contract = Web3.Eth.GetContract(abi, contractAddress);
        }
    }
}
