namespace DVotingBackendApp.responses
{
    public class VoterResponse
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Sex { get; set; }
        public string DOB { get; set; }
        public string UID { get; set; }
        public string Constituency { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public bool IsVoted { get; set; }
    }
}
