using Nethereum.ABI.FunctionEncoding.Attributes;

namespace DVotingBackendApp.dtos
{
    [FunctionOutput]
    public class CandidateOutputListDto
    {
        [Parameter("tuple[]", "", 1)]
        public List<CandidateDto> Candidates { get; set; }
    }
}
