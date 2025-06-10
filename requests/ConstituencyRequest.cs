using System.ComponentModel.DataAnnotations;

namespace DVotingBackendApp.requests
{
    public class ConstituencyRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string StateCode { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
