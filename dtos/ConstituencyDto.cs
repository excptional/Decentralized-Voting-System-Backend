using Nethereum.ABI.FunctionEncoding.Attributes;

namespace DVotingBackendApp.dtos
{
    [FunctionOutput]
    public class ConstituencyDto
    {
        [Parameter("string", "stateCode", 1)]
        public string StateCode { get; set; }

        [Parameter("string", "constituencyType", 2)]
        public string Type { get; set; }

        [Parameter("string", "constituencyNumber", 3)]
        public string Number { get; set; }

        [Parameter("string", "constituencyName", 4)]
        public string Name { get; set; }
    }
}
