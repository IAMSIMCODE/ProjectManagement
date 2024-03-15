using ProjectManagement.Domain.Models.DTO.Response;
using ProjectManagement.Domain.Models;
using AutoMapper;

namespace ProjectManagement.Domain.MappingProfile
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            CreateMap<Achievement, DeveloperAchievementResponse>();


            CreateMap<Developer, GetDeveloperResponse>()
                .ForMember(
                    dest => dest.DeveloperId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                );
        }
    }
}
