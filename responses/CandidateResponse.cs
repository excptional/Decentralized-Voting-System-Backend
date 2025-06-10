namespace DVotingBackendApp.responses
{
    public class CandidateResponse
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Sex { get; set; }
        public string DOB { get; set; }
        public string UID { get; set; }
        public string Constituency { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string PoliticalAffiliation { get; set; }
        public string ImageUrl { get; set; }
        public int VoteCount { get; set; }
    }
}
