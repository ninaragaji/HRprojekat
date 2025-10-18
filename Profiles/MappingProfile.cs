using AutoMapper;
using HRprojekat.DTO;
using HRprojekat.Models;

namespace HRprojekat.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Candidate, CandidateReadDto>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(s => s.Name)));

            CreateMap<CandidateCreateDto, Candidate>();

            CreateMap<CandidateUpdateDto, Candidate>();

            CreateMap<Skill, SkillReadDto>();

            CreateMap<SkillCreateDto, Skill>();
        }
    }
}
