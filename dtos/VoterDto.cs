using DVotingBackendApp.models;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace DVotingBackendApp.dtos
{
    public class VoterDto
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
        public string Dob { get; set; }

        [Parameter("string", "uid", 6)]
        public string Uid { get; set; }

        [Parameter("string", "constituency", 7)]
        public string Constituency { get; set; }

        [Parameter("string", "location", 8)]
        public string Location { get; set; }

        [Parameter("string", "phoneNumber", 9)]
        public string Phone { get; set; }

        [Parameter("bool", "isVoted", 10)]
        public bool IsVoted { get; set; }
    }

}
