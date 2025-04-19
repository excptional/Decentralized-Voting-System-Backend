using Nethereum.ABI.FunctionEncoding.Attributes;

namespace DVotingBackendApp.dtos
{
    public class LocationDto
    {
        [Parameter("string", "latitude", 1)]
        public string Latitude { get; set; }

        [Parameter("string", "longitude", 2)]
        public string Longitude { get; set; }
    }
}
