using DVotingBackendApp.models;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace DVotingBackendApp.dtos
{
    [FunctionOutput]
    public class ConstituencyListDto
    {
        [Parameter("tuple[]", "", 1)]
        public List<ConstituencyDto> Constituencies { get; set; }
    }
}
