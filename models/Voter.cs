using System.Numerics;

namespace DVotingBackendApp.models
{
    public class Voter
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateTime DOB { get; set; }
        public string UID { get; set; }
        public string Constituency { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public bool IsVoted { get; set; }
    }
}
