using Nethereum.ABI.FunctionEncoding.Attributes;

namespace DVotingBackendApp.dtos
{
    [FunctionOutput]
    public class VoterOutputDto
    {
        [Parameter("tuple", "", 1)]
        public VoterDto VoterDto { get; set; }
    }
}
