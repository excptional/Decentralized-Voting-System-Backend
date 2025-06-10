using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace DVotingBackendApp.dtos
{
    public class CandidateDto : IFunctionOutputDTO
    {
        [Parameter("string", "name", 1)]
        public string Name { get; set; }

        [Parameter("string", "fatherName", 2)]
        public string FatherName { get; set; }

        [Parameter("string", "imageUrl", 3)]
        public string ImageUrl { get; set; }

        [Parameter("string", "gender", 4)]
        public string Sex { get; set; }

        [Parameter("string", "dob", 5)]
        public string DOB { get; set; }

        [Parameter("string", "uid", 6)]
        public string UID { get; set; }

        [Parameter("string", "constituency", 7)]
        public string Constituency { get; set; }

        [Parameter("string", "location", 8)]
        public string Location { get; set; }

        [Parameter("string", "politicalAffiliation", 9)]
        public string PoliticalAffiliation { get; set; }

        [Parameter("string", "phoneNumber", 10)]
        public string Phone { get; set; }

        [Parameter("string[]", "votes", 11)]
        public List<string> Votes { get; set; }
    }
}
