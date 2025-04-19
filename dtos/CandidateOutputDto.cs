using Nethereum.ABI.FunctionEncoding.Attributes;

namespace DVotingBackendApp.dtos
{
    [FunctionOutput]
    public class CandidateOutputDto
    {
        [Parameter("tuple", "", 1)]
        public CandidateDto CandidateDto { get; set; }
    }
}
