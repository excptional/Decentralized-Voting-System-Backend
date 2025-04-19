using DVotingBackendApp.models;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace DVotingBackendApp.dtos
{
    public class VoterDto
    {
        [Parameter("string", "name", 1)]
        public string Name { get; set; }

        [Parameter("string", "gender", 2)]
        public string Gender { get; set; }

        [Parameter("string", "dob", 3)]
        public string Dob { get; set; }

        [Parameter("string", "uid", 4)]
        public string Uid { get; set; }

        [Parameter("string", "constituency", 5)]
        public string Constituency { get; set; }

        [Parameter("string", "location", 6)]
        public string Location { get; set; }

        [Parameter("string", "phoneNumber", 7)]
        public string PhoneNumber { get; set; }

        [Parameter("bool", "isVoted", 8)]
        public bool IsVoted { get; set; }
    }

}
