using AutoMapper;
using DVotingBackendApp.dtos;
using DVotingBackendApp.models;
using DVotingBackendApp.requests;
using DVotingBackendApp.responses;

namespace IMBDWebApp.helpers
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<Voter, VoterRequest>().ReverseMap();
            CreateMap<VoterDto, VoterResponse>().ReverseMap();
            CreateMap<Candidate, CandidateRequest>().ReverseMap();
            CreateMap<CandidateDto, CandidateResponse>().ReverseMap();
            CreateMap<Constituency, ConstituencyRequest>().ReverseMap();
            CreateMap<ConstituencyDto, ConstituencyResponse>().ReverseMap();
          

        }
    }
}
