using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace DVotingBackendApp.dtos
{
    public class CandidateDto : IFunctionOutputDTO
    {
        [Parameter("string", "name", 1)]
        public string Name { get; set; }

        [Parameter("string", "gender", 2)]
        public string Sex { get; set; }

        [Parameter("string", "dob", 3)]
        public string DOB { get; set; }

        [Parameter("string", "uid", 4)]
        public string UID { get; set; }

        [Parameter("string", "constituency", 5)]
        public string Constituency { get; set; }

        [Parameter("string", "location", 6)]
        public string Location { get; set; }

        [Parameter("string", "politicalAffiliation", 7)]
        public string PoliticalAffiliation { get; set; }

        [Parameter("string", "phoneNumber", 8)]
        public string Phone { get; set; }

        [Parameter("string[]", "votes", 9)]
        public List<string> Votes { get; set; }
    }
}
