using System.ComponentModel.DataAnnotations;

namespace DVotingBackendApp.requests
{
    public class CandidateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        public string Sex { get; set; }

        [Required]
        public string DOB { get; set; }

        [Required]
        public string UID { get; set; }

        [Required]
        public string Constituency { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string PoliticalAffiliation { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public List<string> Votes { get; set; }
    }
}
